
using System;

namespace SharpRISCV.MachineCode
{
    class B_MachineCode
    {
        //B type: .insn s opcode6, func3, rs1, rs2, symbol
        //SB type: .insn sb opcode6, func3, rs1, rs2, symbol
        //+-----------------+-----+-----+-------+----------------+---------+
        //| simm12[12 | 10:5] | rs2 | rs1 | func3 | simm12[4:1 | 11] | opcode6 |
        //+-----------------+-----+-----+-------+----------------+---------+
        //31                25    20    15      12               7         0

        public MachineCode Generate(RiscVInstruction instruction)
        {
            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);

            int value;
            if (Address.Labels.TryGetValue(instruction.Label, out int labelLineNumber))
            {
                value = Address.Labels[instruction.Label] - Address.CurrentAddress;
            }
            else if (int.TryParse(instruction.Label, out value))
            {
                // Operand is a numeric value
            }
            else
            if (!Address.Labels.ContainsKey(instruction.Label)) throw new Exception("Invalid Lable");

            string imm = value.ToBinary(12);
            if (imm.Length > 12)
            {
                imm = imm.Substring(imm.Length - 12, 12);
            }
            string imm11 = imm.Substring(1,1);
            string imm4_1 = imm.Substring(imm.Length-5,4);
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(int.Parse(instruction.Rs1.Substring(1)), 2).PadLeft(5, '0');
            string rs2Binary = Convert.ToString(int.Parse(instruction.Rs2.Substring(1)), 2).PadLeft(5, '0');
            string imm10_5 = imm.Substring(1, 6);
            string imm12 = imm.Substring(0, 1);

            return new MachineCode($"{imm12}{imm10_5}{rs2Binary}{rs1Binary}{func3}{imm4_1}{imm11}{opcode}", instruction.Instruction);
        }
    }
}
