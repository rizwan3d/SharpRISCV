using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.Windows
{


    public class Compile(string path)
    {
        [DllImport("ImageHlp.dll")]
        private static extern uint MapFileAndCheckSum(string Filename, out uint HeaderSum, out uint CheckSum);

        public const uint memAddress = 0x00401000; // if and only if windows app

        public byte[] BinaryWrite()
        {

            List<byte> finalBytes = BuildPeNoCheckSum();


            uint checkSum = 0;
            File.WriteAllBytes(path, finalBytes.ToArray());

            try
            {
                MapFileAndCheckSum(path, out checkSum, out checkSum);
            }
            catch (EntryPointNotFoundException e)
            {
                var data = File.ReadAllBytes(path);
                var PEStart = BitConverter.ToInt32(data, 0x3c);
                var PECoffStart = PEStart + 4;
                var PEOptionalStart = PECoffStart + 20;
                var PECheckSum = PEOptionalStart + 64;
                checkSum = CalcCheckSum(data, PECheckSum);
            }

            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                fs.Seek(216, SeekOrigin.Current);
                fs.Write(BitConverter.GetBytes(checkSum), 0, 4);
            }

            return File.ReadAllBytes(path);
        }

        public List<byte> BuildPeNoCheckSum()
        {
            List<byte> finalBytes = [];

            List<byte> opcodes = [];
            foreach (var instruction in RiscVAssembler.Instruction)
            {
                if (instruction.InstructionType == InstructionType.Lable)
                    continue;
                instruction.MachineCode().ForEach(machineCode =>
                    opcodes.AddRange(BitConverter.GetBytes(machineCode.Decimal))
                ); ;
            }

            List<byte> dataSectBytes = [];
            dataSectBytes.AddRange(DataSection.DataDirective);
            PEHeaderFactory.dataSectBytes = dataSectBytes;

            PEHeader hdr = PEHeaderFactory.newHdr(opcodes, null, memAddress + (uint)(opcodes.Count() * 4), 0, 0, Machine.IMAGE_FILE_MACHINE_RISCV32, false);
            finalBytes.AddRange(hdr.toBytes());

            //if (this.fillToNextPage)
            //{

            while (opcodes.Count % 512 != 0)
                opcodes.Add(0x00);

            //}

            finalBytes.AddRange(opcodes);


            if (dataSectBytes.Count != 0)
                finalBytes.AddRange(dataSectBytes);
            finalBytes.ToArray();

            return finalBytes;
        }

        public byte[] AddCheckSumForWeb(List<byte> finalBytes)
        {
            var PEStart = BitConverter.ToInt32(finalBytes.ToArray<byte>(), 0x3c);
            var PECoffStart = PEStart + 4;
            var PEOptionalStart = PECoffStart + 20;
            var PECheckSum = PEOptionalStart + 64;
            uint checkSum = CalcCheckSum(finalBytes.ToArray<byte>(), PECheckSum);

            MemoryStream ms = new(finalBytes.ToArray());
            ms.Seek(216, SeekOrigin.Current);
            ms.Write(BitConverter.GetBytes(checkSum), 0, 4);

            return ms.ToArray();
        }

        //https://stackoverflow.com/questions/6429779/can-anyone-define-the-windows-pe-checksum-algorithm
        uint CalcCheckSum(byte[] data, int PECheckSum)
        {
            long checksum = 0;
            var top = Math.Pow(2, 32);

            for (var i = 0; i < data.Length / 4; i++)
            {
                if (i == PECheckSum / 4)
                {
                    continue;
                }
                var dword = BitConverter.ToUInt32(data, i * 4);
                checksum = (checksum & 0xffffffff) + dword + (checksum >> 32);
                if (checksum > top)
                {
                    checksum = (checksum & 0xffffffff) + (checksum >> 32);
                }
            }

            checksum = (checksum & 0xffff) + (checksum >> 16);
            checksum = checksum + (checksum >> 16);
            checksum = checksum & 0xffff;

            checksum += (uint)data.Length;
            return (uint)checksum;

        }
    }
}
