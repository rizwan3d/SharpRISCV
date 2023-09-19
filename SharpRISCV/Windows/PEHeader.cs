using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace SharpRISCV.Windows
{
    //bad use of struct sorta but w.e microsoft does the same
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct PEHeader
    {

        #region MZ header

        public UInt16

            e_magic, e_cblp, e_cp, e_crlc, e_cparhdr, e_minalloc, e_maxalloc,
            e_ss, e_sp, e_csum, e_ip, e_cs, e_lsarlc, e_ovno, e_res, e_res_0,
            e_res_1, e_res_2, e_oemid, e_oeminfo, e_res2, e_res2_0, e_res2_1,
            e_res2_3, e_res2_2, e_res2_4, e_res2_5, e_res2_6, e_res2_7, e_res2_8;

        public UInt32 e_lfanew;

        #endregion

        #region DOS stub

        public fixed Byte unknown[14],
                          msg[38], // ASCII str
                          unknown_0[5],
                          unknown_empty[7];

        #endregion

        #region PE header

        public UInt32 signature;

        public Machine machine;
        public UInt16 numberOfSections;

        public UInt32

            timeDateStamp,
            pointerToSymbolTable,
            numberOfSymbols;

        public UInt16

            sizeOfOptionalHeader,
            characteristics;//First characteristics (Pe header one)

        #endregion

        #region PE optional header

        public UInt16 magic;

        public Byte

            majorLinkerVersion,
            minorLinkerVersion;

        public UInt32

            sizeOfCode,
            sizeOfInitializedData,
            sizeOfUninitializedData,
            addressOfEntryPoint,
            baseOfCode,
            baseOfData,
            imageBase,
            sectionAlignment,
            fileAlignment;

        public UInt16

            majorOperatingSystemVersion,
            minorOperatingSystemVersion,
            majorImageVersion,
            minorImageVersion,
            majorSubsystemVersion,
            minorSubsystemVersion;

        public UInt32

            win32VersionValue,
            sizeOfImage,
            sizeOfHeaders,
            checkSum;

        public UInt16

            subSystem,
            dllCharacteristics;

        public UInt32

            sizeOfStackReserve,
            sizeOfStackCommit,
            sizeOfHeapReserve,
            sizeOfHeapCommit,
            loaderFlags,
            numberOfRvaAndSizes;

        #endregion

        #region Data directories

        public fixed UInt32 dir[32];

        #endregion

        #region PE code section

        public fixed Byte name[8];

        public UInt32

            virtualSize,
            virtualAddress,
            sizeOfRawData,
            pointerToRawData,
            pointerToRelocations,
            pointerToLinenumbers;

        public UInt16

            numberOfRelocations,
            numberOfLinenumbers;

        public UInt32 characteristics_0; //Second characteristics (PE code section one)

        #endregion

        #region Import/data table

        public fixed UInt64 tableBytes[12];

        #endregion

    }
}
