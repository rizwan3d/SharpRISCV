using SharpRISCV.Core.Registers;
using System;

namespace SharpRISCV.Core.MachineCode
{
    public class S_MachineCode
    {
        //+--------------+-----+-----+-------+-------------+---------+
        //| simm12[11:5] | rs2 | rs1 | func3 | simm12[4:0] | opcode6 |
        //+--------------+-----+-----+-------+-------------+---------+
        //31             25    20    15      12            7         0

        public MachineCode Generate(RiscVInstruction instruction)
        {

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string imm = MachineCode.ProcessSource(instruction.Immediate).ToBinary(12);
            if (imm.Length > 12)
            {
                imm = imm.Substring(imm.Length - 12, 12);
            }

            string imm4_0 = imm.Substring(imm.Length - 5);
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(Register.FromABI[instruction.Rs1], 2).PadLeft(5, '0');
            string rs2Binary = Convert.ToString(Register.FromABI[instruction.Rd], 2).PadLeft(5, '0');
            string imm11_5 = imm.Substring(0, imm.Length - 5);

            return new($"{imm11_5}{rs2Binary}{rs1Binary}{func3}{imm4_0}{opcode}", instruction.Instruction);
        }
    }
}
