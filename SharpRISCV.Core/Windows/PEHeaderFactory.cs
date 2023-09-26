using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpRISCV.Core.Windows
{
    public static class PEHeaderFactory
    {
        public static List<byte> dataSectBytes { get; set; }

        public static uint latestDataSectAddr;
        public const uint dataSectAddr = 0x00403000;
        // ^ when fixing this
        // when a child parser is done compiling, give the index of
        // opcodes that need to be changed to the actual data sect
        // address. Update the index relative to the main parser,
        // and update all opcode indexes to be set to the valid data sect addr
        // plus the old original value.

        unsafe static public PEHeader newHdr(List<byte> opcodes, List<byte> importOpcodes, uint endMemAddress, int offset, uint importOpcodesVirtualSize, Machine machine, bool gui = false)
        {

            PEHeader hdr = default;

            List<byte> mockOpcodes = new List<byte>(opcodes);
            while (mockOpcodes.Count % 512 != 0)
                mockOpcodes.Add(0x00);

            ushort sections = (ushort)(importOpcodes != null ? 2 : 1);
            if (dataSectBytes.Count() != 0) ++sections; // (Data Section)

            #region MZ header

            hdr.e_magic = BitConverter.ToUInt16(Encoding.ASCII.GetBytes("MZ"), 0);
            hdr.e_cblp = (ushort)(opcodes.Count % 512);
            hdr.e_cp = (ushort)Math.Ceiling(opcodes.Count / 512M);
            const ushort mzHeaderSize = 64 / 16;
            hdr.e_cparhdr = mzHeaderSize;
            hdr.e_minalloc = 0x0010;
            hdr.e_maxalloc = ushort.MaxValue;
            hdr.e_sp = 0x0140;
            hdr.e_lsarlc = 0x0040;
            const byte MZheader_ByteSize = 64, DOSStub_ByteSize = 64;
            hdr.e_lfanew = MZheader_ByteSize + DOSStub_ByteSize;

            #endregion

            #region DOS stub

            Marshal.Copy(new byte[] { 0x0E, 0x1F, 0xBA, 0x0E, 0, 0xB4, 9, 0xCD, 0x21, 0xB8, 1, 0x4C, 0xCD, 0x21 }, 0, new IntPtr(hdr.unknown), 14);
            const string msg = "This program cannot be run in DOS mode";
            Marshal.Copy(msg.Select(x => (byte)x).ToArray(), 0, new IntPtr(hdr.msg), msg.Length);
            Marshal.Copy(new byte[] { 0x2E, 0x0D, 0x0D, 0x0A, 0x24 }, 0, new IntPtr(hdr.unknown_0), 5);

            #endregion

            #region PE header

            hdr.signature = BitConverter.ToUInt16(Encoding.ASCII.GetBytes("PE"), 0);
            hdr.machine = machine;
            hdr.numberOfSections = sections;
            hdr.timeDateStamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            hdr.sizeOfOptionalHeader = 0x00E0;
            hdr.characteristics = 0x010F;

            #endregion

            #region PE Optional Header

            hdr.magic = 0x010B;//IMAGE_NT_OPTIONAL_HDR32_MAGIC
            hdr.majorLinkerVersion = 1;
            hdr.minorLinkerVersion = 0x49;
            hdr.sizeOfCode = (uint)mockOpcodes.Count;
            hdr.sizeOfInitializedData = (uint)(mockOpcodes.Count + (importOpcodes != null ? importOpcodes.Count : 0));
            const uint alignment = 0x00001000, imgBase = 0x00400000;
            hdr.addressOfEntryPoint = alignment;
            hdr.baseOfCode = alignment;
            hdr.baseOfData = alignment;
            hdr.imageBase = imgBase;
            hdr.sectionAlignment = alignment;
            hdr.fileAlignment = 512;
            hdr.majorOperatingSystemVersion = 1;
            hdr.minorSubsystemVersion = 0x000A;
            hdr.majorSubsystemVersion = 3;
            uint opcodesSectSize = (uint)(opcodes.Count - opcodes.Count % alignment + alignment), importOpcodesSectSize = 0, dataOpcodesSectSize = 0;
            hdr.sizeOfImage = opcodesSectSize + alignment;//+aligment at end to account for header
            if (importOpcodes != null)
            {
                importOpcodesSectSize = (uint)(importOpcodes.Count - importOpcodes.Count % alignment + alignment);
                hdr.sizeOfImage += importOpcodesSectSize;
            }
            hdr.sizeOfHeaders = (uint)Marshal.SizeOf(typeof(PEHeader));
            hdr.subSystem = (ushort)(gui ? 2 : 3);//IMAGE_SUBSYSTEM_WINDOWS_GUI,IMAGE_SUBSYSTEM_WINDOWS_CUI
            hdr.sizeOfStackCommit = alignment;
            hdr.sizeOfStackReserve = alignment;
            hdr.sizeOfHeapReserve = alignment * 16;
            hdr.numberOfRvaAndSizes = 16;

            #endregion

            #region PE Code Section

            Marshal.Copy(".text".toCodeSectNameBytes(), 0, new IntPtr(hdr.name), 8);
            hdr.virtualSize = (uint)opcodes.Count;
            hdr.virtualAddress = alignment;
            hdr.sizeOfRawData = (uint)mockOpcodes.Count;
            hdr.pointerToRawData = 512;
            hdr.characteristics_0 = 0xE0000060;

            #endregion

            #region Sections

            offset = 0;

            #region Import section

            if (importOpcodes != null)
            {

                uint addr = opcodesSectSize + alignment;

                hdr.dir[2] = addr;
                hdr.dir[3] = importOpcodesVirtualSize;
                hdr.tableBytes[0] = BitConverter.ToUInt64(Encoding.ASCII.GetBytes(".idata").Concat(new byte[] { 0, 0 }).ToArray(), 0);
                hdr.tableBytes[1] = BitConverter.ToUInt64(BitConverter.GetBytes(importOpcodesVirtualSize).Concat(BitConverter.GetBytes(addr)).ToArray(), 0);
                //importTableBytes[2] - First UInt32: Size of Import Section Raw Data, Second UInt32: Pointer to Import Section Raw Data (The amount of bytes until the import section)
                hdr.tableBytes[2] = BitConverter.ToUInt64(BitConverter.GetBytes((uint)importOpcodes.Count).Concat(BitConverter.GetBytes((uint)Marshal.SizeOf(typeof(PEHeader)) + mockOpcodes.Count)).ToArray(), 0);
                hdr.tableBytes[4] = BitConverter.ToUInt64(new byte[] { 0, 0, 0, 0, 0x40, 0, 0, 0xC0 }, 0); // Characteristics == 0C0000040h

                offset = 5;

            }

            #endregion

            #region Data section

            if (dataSectBytes.Count() != 0)
            {

                dataOpcodesSectSize = (uint)(dataSectBytes.Count() - dataSectBytes.Count() % alignment + alignment);
                hdr.sizeOfImage += dataOpcodesSectSize;

                uint addr = importOpcodesSectSize + opcodesSectSize + alignment;
                latestDataSectAddr = addr + imgBase;

                // (The data section in Sunset is solely used for storing static instances)
                hdr.tableBytes[0 + offset] = BitConverter.ToUInt64(Encoding.ASCII.GetBytes(".data").Concat(new byte[] { 0, 0, 0 }).ToArray(), 0);
                hdr.tableBytes[1 + offset] = BitConverter.ToUInt64(BitConverter.GetBytes(dataSectBytes.Count()).Concat(BitConverter.GetBytes(addr)).ToArray(), 0);
                while (dataSectBytes.Count() % 512 != 0)
                    dataSectBytes.Add(0);
                hdr.tableBytes[2 + offset] = BitConverter.ToUInt64(BitConverter.GetBytes((uint)dataSectBytes.Count()).Concat(BitConverter.GetBytes((uint)Marshal.SizeOf(typeof(PEHeader)) + mockOpcodes.Count + (importOpcodes == null ? 0 : importOpcodes.Count))).ToArray(), 0);
                hdr.tableBytes[4 + offset] = BitConverter.ToUInt64(new byte[] { 0, 0, 0, 0, 0x40, 0, 0, 0xC0 }, 0); // Characteristics == 0C0000040h

            }

            #endregion

            #endregion

            return hdr;

        }

        static public byte[] toBytes(this PEHeader hdr)
        {

            const ushort hdrSize = 512;

            byte[] arr = new byte[hdrSize];
            IntPtr ptr = Marshal.AllocHGlobal(hdrSize);

            Marshal.StructureToPtr(hdr, ptr, true);
            Marshal.Copy(ptr, arr, 0, hdrSize);
            Marshal.FreeHGlobal(ptr);

            return arr;

        }

        /// <returns>8 bytes always</returns>
        static private byte[] toCodeSectNameBytes(this string str)
        {

            const byte maxLength = 8;

            if (str.Length > maxLength) throw new Exception("Name too long");
            else if (!str.StartsWith(".")) throw new Exception("Invalid name");

            List<byte> bytes = str.Select(x => (byte)x).ToList();
            bytes.AddRange(new byte[maxLength - str.Length]);
            return bytes.ToArray();

        }

    }
}
