
using SharpRISCV.Registers;
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
            string rdBinary = Convert.ToString(Register.FromABI[instruction.Rd], 2).PadLeft(5, '0');
            string imm = Convert.ToInt32(instruction.Immediate).ToBinary(20);

            return new MachineCode($"{imm}{rdBinary}{opcode}", instruction.Instruction);
        }
    }
}
