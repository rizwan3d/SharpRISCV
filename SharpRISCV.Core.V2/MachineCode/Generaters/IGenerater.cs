using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode.Generaters
{
    /// <summary>
    /// I type: .insn i opcode6, func3, rd, rs1, simm12
    //  I type: .insn i opcode6, func3, rd, simm12(rs1)
    //  +--------------+-----+-------+----+---------+
    //  | simm12[11:0] | rs1 | func3 | rd | opcode6 |
    //  +--------------+-----+-------+----+---------+
    //  31             20    15      12   7         0
    /// </summary>
    public class IGenerater : Generater, IMachineCodeGenerateStrategy
    {
        public override uint Generate(IInstruction instruction, ISymbolTable symbolTable, uint address)
        {
            uint machineCode = 0;

            // Set rs2 (bits 24-20)
            machineCode |= (GetIntValForImm(instruction.Rs2, symbolTable, address) & 0xFFF) << 20;

            // Set rs1 (bits 19-15)
            machineCode |= (uint)instruction.Rs1.Value.ToEnum<Register>() << 15;

            // Set func3 (bits 14-12)
            machineCode |= (uint)instruction.Funct3 << 12;

            // Set rd (bits 11-7)
            machineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 7;

            // Set opcode6 (bits 6-0)
            machineCode |= (uint)instruction.Opcode;

            return machineCode;
        }
    }
}
