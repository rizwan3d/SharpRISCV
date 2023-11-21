using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.Elf
{
    public class Compile(string path)
    {
        public List<byte> BinaryWrite()
        {
            List<byte> finalBytes = bytes();
            using (FileStream fileStream = new(path, FileMode.Create))
            using (BinaryWriter writer = new(fileStream))
            {
                writer.Write(finalBytes.ToArray());
            }
            return finalBytes;
        }

        public List<byte> bytes()
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
            var entryPoint = Address.EntryPoint;

            finalBytes.AddRange([ 0x7F, (byte)'E', (byte)'L', (byte)'F' ]);
            finalBytes.Add(2); // class (64-bit)
            finalBytes.Add(1); // data encoding (little-endian)
            finalBytes.Add(1); // version
            finalBytes.AddRange(new byte[9]); // padding
            finalBytes.AddRange([ 2, 0 ]); // type (executable)
            finalBytes.AddRange([ 0xF3, 0 ]); // machine (RISC-V)
            finalBytes.AddRange([ 1, 0, 0, 0]); // version
            finalBytes.AddRange(BitConverter.GetBytes((ulong)(0x10000 + 0x40 + 0x38+ 0x38 + entryPoint))); // entry point
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x40)); // program header offset
            finalBytes.AddRange([ 0, 0, 0, 0, 0, 0, 0, 0 ]); // section header offset
            finalBytes.AddRange([ 0x4, 0, 0, 0 ]); // flags
            finalBytes.AddRange(BitConverter.GetBytes((Int16)0x40)); // ELF header size
            finalBytes.AddRange(BitConverter.GetBytes((Int16)0x38)); // program header size
            finalBytes.AddRange(BitConverter.GetBytes((Int16)2)); // program header entry size
            finalBytes.AddRange([ 0, 0 ]); // section header size
            finalBytes.AddRange([ 0, 0 ]); // section header entry size
            finalBytes.AddRange([ 0, 0 ]); // section header string index


            var size = 0x40 + 0x38 + 0x38 + opcodes.Count;

            //// Program header
            finalBytes.AddRange([ 1, 0, 0, 0 ]); // type (load)
            finalBytes.AddRange([ 0x5, 0, 0, 0 ]); // offset
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x00000)); // Virtual address of the segment in memory.
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x10000)); // virtual address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x10000 + entryPoint)); // physical address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)size)); // file size
            finalBytes.AddRange(BitConverter.GetBytes((Int64)size)); // memory size
            finalBytes.AddRange([ 0, 0x10, 0, 0 ]); // flags (execute and read)
            finalBytes.AddRange([ 00,00,00,00 ]); // alignment

            //// Program header
            finalBytes.AddRange([  1, 0, 0, 0 ]); // type (load)
            finalBytes.AddRange([ 06, 0, 0, 0 ]); // offset
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x00000 + size));
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x11000 + size)); // virtual address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x11000 + size)); // physical address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)dataSectBytes.Count)); // file size
            finalBytes.AddRange(BitConverter.GetBytes((Int64)dataSectBytes.Count)); // memory size
            finalBytes.AddRange([ 0, 0x10, 0, 0]); // flags (execute and read)
            finalBytes.AddRange([ 0, 0x00, 0, 0]); // alignment


            finalBytes.AddRange(opcodes);
            finalBytes.AddRange(dataSectBytes);

            return finalBytes;
        }
    }
}

