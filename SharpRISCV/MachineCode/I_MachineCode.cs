
using System;

namespace SharpRISCV.MachineCode
{
    class I_MachineCode
    {
        //I type: .insn i opcode6, func3, rd, simm12(rs1)
        //+--------------+-----+-------+----+---------+
        //| simm12[11:0] | rs1 | func3 | rd | opcode6 |
        //+--------------+-----+-------+----+---------+
        //31             20    15      12   7         0

        public MachineCode Generate(RiscVInstruction instruction)
        {

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string rdBinary = Convert.ToString(int.Parse(instruction.Rd.Substring(1)), 2).PadLeft(5, '0');
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(int.Parse(instruction.Rs1.Substring(1)), 2).PadLeft(5, '0');
            string imm = int.Parse(instruction.Immediate).ToBinary(11);
            if (imm.Length > 11)
            {
                imm = imm.Substring(imm.Length - 11, 11);
            }

            return new MachineCode($"{imm}{rs1Binary}{func3}{rdBinary}{opcode}", instruction.Instruction);
        }
    }
}
