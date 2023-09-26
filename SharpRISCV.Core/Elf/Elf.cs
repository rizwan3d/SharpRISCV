using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.Elf
{
    public class EFI
    {

        static uint _start = 0x2AB30000;
        public static void Build()
        {

            byte[] codeBytes = { 0xB0, 0x2A, 0x31, 0xC0, 0xFE, 0xC0, 0x83, 0xF8, 0xC0, 0xCD, 0x80, 0x66, 0x31, 0xC0, 0x83, 0xC0, 0x01, 0x3D, 0x00, 0x00, 0x00, 0x00, 0x75, 0x04, 0x48, 0xC7, 0xC0, 0x02, 0xAB, 0x00, 0x00, 0x48, 0xC7, 0xC0, 0x02, 0xAB, 0x00, 0x00, 0x90, 0xC3 };
            uint _end = (uint)(_start + codeBytes.Length);


            using (FileStream fileStream = new FileStream("output.elf", FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {

                // Program header
                //byte[] programHeader = {
                //    0x01, 0x00, 0x00, 0x00,  // Segment type: Load
                //    0x00, 0x00, 0x00, 0x00,  // Flags (not used)
                //    0x00, 0x00, 0x00, 0x00,  // Offset in file
                //    (byte)_start, // Virtual address in memory
                //    (byte)_start, // Physical address in memory
                //    (byte)(_end - _start),      // Size of segment in file
                //    (byte)(_end - _start),      // Size of segment in memory
                //    7,1
                //};

                //ushort e_phentsize = (ushort)(8* programHeader.Length) ;

                //byte[] elfHeader = { 
                //    // Line 1
                //    0x7f,  // 7F
                //    0x45,  // E
                //    0x4c,  // L
                //    0x46,  // F
                //    2,     // 64Bit (1 for 32 bit)
                //    1,     // little (Don't Know)
                //    1,     // Version (Always 1)
                //    0, 0, 0, 0, 0, 0, 0, 0, 0,  // Empty 8 segments
                //    // Line 2
                //    2,0,        // Type ( 2 = EXE)
                //    0xF3,0,     // Machine RISC-V
                //    1,0,0,0,    // Version (Always 1)
                //    0,0,0,0,0,0,0,0, // e_entry Proggrmer heder offset
                //    // Line 3
                //    0x40, 0,0,0,0,0,0,0,  // e_phoff
                //    0,    0,0,0,0,0,0,0,  // e_shoff
                //    1,0,0,0,    // e_flags
                //    0x64,0,  //Contains the size of this header, normally 64 Bytes for 64-bit and 52 Bytes for 32-bit format.
                //    (byte)e_phentsize,1,0,0,0,0,0,0,0
                //};



                byte[] elfHeader =
                {
                    0x7f,0x45,0x4c,0x46,0x02,0x01,0x01,0x00, //000000   e_ident
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00, //000008   
                    0x02,0x00,0xf3,0x00,0x01,0x00,0x00,0x00, //000010   e_type*2    e_machine*2    e_version*4
                    0xe4,0x01,0x01,0x00,0x00,0x00,0x00,0x00, //000018   e_entry*8
                    0x40,0x00,0x00,0x00,0x00,0x00,0x00,0x00, //000020   e_phoff
                    0xf8,0x36,0x00,0x00,0x00,0x00,0x00,0x00, //000028   e_shoff
                    0x04,0x00,0x00,0x00,0x40,0x00,0x38,0x00, //000030   e_shoff*4   e_ehsize*2     e_phentsize*2  
                    0x02,0x00,0x40,0x00,0x0b,0x00,0x0a,0x00, //000038   e_phnum*2   e_shentsize*2  e_shentsize*2  e_shentsize*2
                };

                byte[] programHeader = {
                    0x01,0x00,0x00,0x00,0x05,0x00,0x00,0x00, //000000   p_type*4    p_flags*4
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00, //000008   p_offset*8
                    0x00,0x00,0x01,0x00,0x00,0x00,0x00,0x00, //000010   p_vaddr*8
                    0x00,0x00,0x01,0x00,0x00,0x00,0x00,0x00, //000018   p_paddr*8
                    0x3c,0x30,0x00,0x00,0x00,0x00,0x00,0x00, //000020   p_filesz*8
                    0x3c,0x30,0x00,0x00,0x00,0x00,0x00,0x00, //000028   p_memsz*8
                    0x00,0x10,0x00,0x00,0x00,0x00,0x00,0x00  //000030   p_align*8
                };




                //byte[] machineCode = {
                //    // Data section
                //    0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x2C, 0x20, 0x57,
                //    0x6F, 0x72, 0x6C, 0x64, 0x21, 0x0A, 0x00, 0x00,

                //    // Text section
                //    0x37, 0x03, 0x00, 0x00, 0x6F, 0x03, 0x00, 0x00,
                //    0x67, 0x03, 0x00, 0x00, 0x23, 0x03, 0x00, 0x00,
                //    0x13, 0x00, 0x00, 0x00, 0x93, 0x01, 0x00, 0x00,
                //    0x63, 0x00, 0x00, 0x00, 0x6F, 0x03, 0x00, 0x00,
                //    0x93, 0x01, 0x00, 0x00, 0x6F, 0x73, 0x00, 0x00
                //};

                // Combine the ELF header, program header, and machine code
                //byte[] elfFile = new byte[elfHeader.Length + programHeader.Length];//+ codeBytes.Length];
                // Array.Copy(elfHeader, elfFile, elfHeader.Length);
                // Array.Copy(programHeader, 0, elfFile, elfHeader.Length, programHeader.Length);
                //Array.Copy(codeBytes, 0, elfFile, elfHeader.Length + programHeader.Length, codeBytes.Length);

                //writer.Write(elfHeader);
                //writer.Write(programHeader);

                // Define ELF section header
                //long sectionHeaderOffset = writer.BaseStream.Position;
                //writer.Write((ushort)0);    // e_shstrndx
                //writer.Write((ushort)0);    // e_flags
                //writer.Write((ushort)64);   // e_ehsize (ELF header size)
                //writer.Write((ushort)64);   // e_phentsize (Program header entry size)
                //writer.Write((ushort)0);    // e_phnum (Number of program header entries)
                //writer.Write((ushort)64);   // e_shentsize (Section header entry size)
                //writer.Write((ushort)1);    // e_shnum (Number of section header entries)
                //writer.Write(sectionHeaderOffset); // e_shoff (Section header table offset)

                // Define a section for the machine code
                writer.Write((uint)1);       // sh_name (Index in the string table)
                writer.Write((uint)3);       // sh_type (SHT_PROGBITS, program information)
                writer.Write((ulong)0);      // sh_flags (No flags)
                writer.Write((ulong)0);      // sh_addr (Virtual address, not used)
                writer.Write((ulong)0);      // sh_offset (Offset in file)
                writer.Write((ulong)0);      // sh_size (Size of section)
                writer.Write((uint)0);       // sh_link (Section index for section header string table)
                writer.Write((uint)0);       // sh_info (Extra information)
                writer.Write((ulong)1);      // sh_addralign (Alignment)
                writer.Write((ulong)0);      // sh_entsize (Entry size, if applicable)

                byte[] machineCode = new byte[] { 0x00, 0x25, 0x01, 0x03, 0x00, 0x00 };
                // Write machine code to the section
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);
                writer.Write(machineCode);

                // Update the section header size
                //long sectionHeaderSize = writer.BaseStream.Position - sectionHeaderOffset;
                //writer.Seek((int)sectionHeaderOffset + 32, SeekOrigin.Begin);
                //writer.Write((ulong)sectionHeaderSize);

            }
        }
    }
}

