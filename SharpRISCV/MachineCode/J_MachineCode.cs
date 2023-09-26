
using SharpRISCV.Registers;
using System;

namespace SharpRISCV.MachineCode
{
    class J_MachineCode
    {
        //J type: .insn j opcode6, rd, symbol
        //UJ type: .insn uj opcode6, rd, symbol
        //+------------+--------------+------------+---------------+----+---------+
        //| simm20[20] | simm20[10:1] | simm20[11] | simm20[19:12] | rd | opcode6 |
        //+------------+--------------+------------+---------------+----+---------+
        //31           30             21           20              12   7         0

        public MachineCode Generate(RiscVInstruction instruction)
        {

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            int value = MachineCode.ProcessSource(instruction.Immediate);

            string rdBinary = Convert.ToString(Register.FromABI[instruction.Rd], 2).PadLeft(5, '0');
            string imm = value.ToBinary(20);
            if(imm.Length > 20)
            {
                imm = imm.Substring(imm.Length - 20, 20);
            }
            string imm20 = imm.Substring(0, 1);
            string imm19_12 = imm.Substring(1, 8);
            string imm10_1 = imm.Substring(9, 10);
            string imm11 = imm.Substring(11, 1);

            return new MachineCode($"{imm20}{imm10_1}{imm11}{imm19_12}{rdBinary}{opcode}", instruction.Instruction);
        }
    }
}
