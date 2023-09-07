
using System;

namespace SharpRISCV.MachineCode
{
    class U_MachineCode
    {
        //U type: .insn u opcode6, rd, simm20
        //+--------------------------+----+---------+
        //| simm20[20 | 10:1 | 11 | 19:12] | rd | opcode6 |
        //+--------------------------+----+---------+
        //31                         12   7         0

        public MachineCode Generate(RiscVInstruction instruction)
        {

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string rdBinary = Convert.ToString(int.Parse(instruction.Rd.Substring(1)), 2).PadLeft(5, '0');
            string imm = Convert.ToInt32(instruction.Immediate,16).ToBinary(20);

            return new MachineCode($"{imm}{rdBinary}{opcode}");
        }
    }
}
