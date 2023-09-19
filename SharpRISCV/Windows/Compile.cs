using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Windows
{
    internal class Compile
    {

        string path;

        public Compile(string path) {
            this.path = path;
        }

        public void BinaryWrite()
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
            PEHeaderFactory.dataSectBytes = finalBytes;

            uint memAddress = 0x00401000; // if and only if windows app
            PEHeader hdr = PEHeaderFactory.newHdr(opcodes, new List<byte>(), memAddress, 0, 0, Machine.IMAGE_FILE_MACHINE_RISCV32 ,false);
            finalBytes.AddRange(hdr.toBytes());
            finalBytes.AddRange(opcodes);


            if (dataSectBytes.Count != 0)
                finalBytes.AddRange(dataSectBytes);
            finalBytes.ToArray();

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    writer.Write(finalBytes.ToArray());
                }
            }
        }
    }
}
