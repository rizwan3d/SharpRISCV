
using System;

namespace SharpRISCV.MachineCode
{
    class R_MachineCode
    {
        //R type: .insn r opcode6, func3, func7, rd, rs1, rs2
        //+-------+-----+-----+-------+----+---------+
        //| func7 | rs2 | rs1 | func3 | rd | opcode6 |
        //+-------+-----+-----+-------+----+---------+
        //31      25    20    15      12   7        0

        public MachineCode Generate(RiscVInstruction instruction)
        {

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string rdBinary = Convert.ToString(int.Parse(instruction.Rd.Substring(1)), 2).PadLeft(5, '0');
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(int.Parse(instruction.Rs1.Substring(1)), 2).PadLeft(5, '0');
            string rs2Binary = Convert.ToString(int.Parse(instruction.Rs2.Substring(1)), 2).PadLeft(5, '0');
            string func7 = ((int)instruction.Funct7).ToBinary(7);

            return new MachineCode($"{func7}{rs2Binary}{rs1Binary}{func3}{rdBinary}{opcode}");
        }
    }
}
