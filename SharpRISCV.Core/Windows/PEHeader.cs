using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;

namespace SharpRISCV.Core.Windows
{
    //bad use of struct sorta but w.e microsoft does the same
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct PEHeader
    {

        #region MZ header

        public ushort

            e_magic, e_cblp, e_cp, e_crlc, e_cparhdr, e_minalloc, e_maxalloc,
            e_ss, e_sp, e_csum, e_ip, e_cs, e_lsarlc, e_ovno, e_res, e_res_0,
            e_res_1, e_res_2, e_oemid, e_oeminfo, e_res2, e_res2_0, e_res2_1,
            e_res2_3, e_res2_2, e_res2_4, e_res2_5, e_res2_6, e_res2_7, e_res2_8;

        public uint e_lfanew;

        #endregion

        #region DOS stub

        public fixed byte unknown[14],
                          msg[38], // ASCII str
                          unknown_0[5],
                          unknown_empty[7];

        #endregion

        #region PE header

        public uint signature;

        public Machine machine;
        public ushort numberOfSections;

        public uint

            timeDateStamp,
            pointerToSymbolTable,
            numberOfSymbols;

        public ushort

            sizeOfOptionalHeader,
            characteristics;//First characteristics (Pe header one)

        #endregion

        #region PE optional header

        public ushort magic;

        public byte

            majorLinkerVersion,
            minorLinkerVersion;

        public uint

            sizeOfCode,
            sizeOfInitializedData,
            sizeOfUninitializedData,
            addressOfEntryPoint,
            baseOfCode,
            baseOfData,
            imageBase,
            sectionAlignment,
            fileAlignment;

        public ushort

            majorOperatingSystemVersion,
            minorOperatingSystemVersion,
            majorImageVersion,
            minorImageVersion,
            majorSubsystemVersion,
            minorSubsystemVersion;

        public uint

            win32VersionValue,
            sizeOfImage,
            sizeOfHeaders,
            checkSum;

        public ushort

            subSystem,
            dllCharacteristics;

        public uint

            sizeOfStackReserve,
            sizeOfStackCommit,
            sizeOfHeapReserve,
            sizeOfHeapCommit,
            loaderFlags,
            numberOfRvaAndSizes;

        #endregion

        #region Data directories

        public fixed uint dir[32];

        #endregion

        #region PE code section

        public fixed byte name[8];

        public uint

            virtualSize,
            virtualAddress,
            sizeOfRawData,
            pointerToRawData,
            pointerToRelocations,
            pointerToLinenumbers;

        public ushort

            numberOfRelocations,
            numberOfLinenumbers;

        public uint characteristics_0; //Second characteristics (PE code section one)

        #endregion

        #region Import/data table

        public fixed ulong tableBytes[12];

        #endregion

    }
}
