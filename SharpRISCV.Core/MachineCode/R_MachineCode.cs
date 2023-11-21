using SharpRISCV.Core.Registers;
using System;

namespace SharpRISCV.Core.MachineCode
{
    public class R_MachineCode
    {
        //R type: .insn r opcode6, func3, func7, rd, rs1, rs2
        //+-------+-----+-----+-------+----+---------+
        //| func7 | rs2 | rs1 | func3 | rd | opcode6 |
        //+-------+-----+-----+-------+----+---------+
        //31      25    20    15      12   7        0

        public MachineCode Generate(RiscVInstruction instruction)
        {
            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string rdBinary = Convert.ToString(Register.FromABI[instruction.Rd], 2).PadLeft(5, '0');
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(Register.FromABI[instruction.Rs1], 2).PadLeft(5, '0');
            string rs2Binary = Convert.ToString(Register.FromABI[instruction.Rs2], 2).PadLeft(5, '0');
            string func7 = ((int)instruction.Funct7).ToBinary(7);

            return new($"{func7}{rs2Binary}{rs1Binary}{func3}{rdBinary}{opcode}", instruction.Instruction);
        }
    }
}
