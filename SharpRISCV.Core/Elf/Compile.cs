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
    public class Compile
    {

        string path;
        public Compile(string path)
        {
            this.path = path;
        }
        public List<byte> BinaryWrite()
        {
            List<byte> finalBytes = bytes();
            using (FileStream fileStream = new FileStream(this.path, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(finalBytes.ToArray());
            }
            return finalBytes;
        }

        public List<byte> bytes()
        {
            List<byte> finalBytes = new List<byte>();

            List<byte> opcodes = new List<byte>();
            foreach (var instruction in RiscVAssembler.Instruction)
            {
                if (instruction.InstructionType == InstructionType.Lable)
                    continue;
                instruction.MachineCode().ForEach(machineCode =>
                    opcodes.AddRange(BitConverter.GetBytes(machineCode.Decimal))
                ); ;
            }

            List<byte> dataSectBytes = new List<byte>();
            dataSectBytes.AddRange(DataSection.DataDirective);
            var entryPoint = 0x40 + 0x38 + Address.EntryPoint;

            finalBytes.AddRange(new byte[] { 0x7F, (byte)'E', (byte)'L', (byte)'F' });
            finalBytes.Add(2); // class (64-bit)
            finalBytes.Add(1); // data encoding (little-endian)
            finalBytes.Add(1); // version
            finalBytes.AddRange(new byte[9]); // padding
            finalBytes.AddRange(new byte[] { 2, 0 }); // type (executable)
            finalBytes.AddRange(new byte[] { 0xF3, 0 }); // machine (RISC-V)
            finalBytes.AddRange(new byte[] { 1, 0, 0, 0 }); // version
            finalBytes.AddRange(BitConverter.GetBytes((Int64)entryPoint)); // entry point
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x40)); // program header offset
            finalBytes.AddRange(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }); // section header offset
            finalBytes.AddRange(new byte[] { 0, 0, 0, 0 }); // flags
            finalBytes.AddRange(BitConverter.GetBytes((Int16)0x40)); // ELF header size
            finalBytes.AddRange(BitConverter.GetBytes((Int16)0x38)); // program header size
            finalBytes.AddRange(BitConverter.GetBytes((Int16)1)); // program header entry size
            finalBytes.AddRange(new byte[] { 0, 0 }); // section header size
            finalBytes.AddRange(new byte[] { 0, 0 }); // section header entry size
            finalBytes.AddRange(new byte[] { 0, 0 }); // section header string index


            var size = 96 + sizeof(byte) * opcodes.Count + sizeof(byte) * dataSectBytes.Count;

            //// Program header
            finalBytes.AddRange(new byte[] { 1, 0, 0, 0 }); // type (load)
            finalBytes.AddRange(new byte[] { 0, 0, 0, 0 }); // offset
            finalBytes.AddRange(BitConverter.GetBytes((Int64)0x40)); // virtual address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)entryPoint)); // virtual address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)entryPoint)); // physical address
            finalBytes.AddRange(BitConverter.GetBytes((Int64)size)); // file size
            finalBytes.AddRange(BitConverter.GetBytes((Int64)size)); // memory size
            finalBytes.AddRange(new byte[] { 5, 0, 0, 0 }); // flags (execute and read)
            finalBytes.AddRange(new byte[] { 0, 0, 0, 0, 0, 0,0,0 }); // alignment


            finalBytes.AddRange(opcodes);
            finalBytes.AddRange(dataSectBytes);

            return finalBytes;
        }
    }
}

