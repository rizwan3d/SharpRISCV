using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode.Generaters
{
    public class UGenerater : Generater, IMachineCodeGenerateStrategy
    {
        /// <summary>
        //  U type: .insn u opcode6, rd, simm20
        //  +--------------------------+----+---------+
        //  | simm20[20|10:1|11|19:12] | rd | opcode6 |
        //  +--------------------------+----+---------+
        //  31                         12   7         0
        /// </summary>
        public override uint Generate(IInstruction instruction, ISymbolTable symbolTable, uint address)
        {
            uint machineCode = 0;

            // Calculate simm20 from the instruction's immediate value
            // simm20 is the immediate value that directly represents a constant (no need to adjust).
            uint simm20 = GetIntValForImm(instruction.Rs1, symbolTable, address);

            // Set simm20 (bits 31-12)
            machineCode |= (simm20 & 0xFFFFF) << 12;

            // Set rd (bits 11-7)
            machineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 7;

            // Set opcode6 (bits 6-0)
            machineCode |= (uint)instruction.Opcode;

            return machineCode;
        }
    }
}
