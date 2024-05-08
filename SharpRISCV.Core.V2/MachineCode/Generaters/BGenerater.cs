using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode.Generaters
{
    /// <summary>
    /// B type: .insn s opcode6, func3, rs1, rs2, symbol
    //  +-----------------+-----+-----+-------+----------------+---------+
    //  | simm12[12|10:5] | rs2 | rs1 | func3 | simm12[4:1|11] | opcode6 |
    //  +-----------------+-----+-----+-------+----------------+---------+
    //  31                25    20    15      12               7         0
    /// </summary>
    public class BGenerater : Generater, IMachineCodeGenerateStrategy
    {
        public override uint Generate(IInstruction instruction, ISymbolTable symbolTable, uint address)
        {
            uint machineCode = 0;

            uint targetAddress = GetIntValForImm(instruction.Rs2, symbolTable, address);
            uint simm12 = (uint)targetAddress;

            // simm12 parts
            // - simm12[12|10:5] goes into bits 31-25
            // - simm12[4:1|11] goes into bits 11-7
            machineCode |= (simm12 & 0x800) << 20; // simm12[12] at bit 31
            machineCode |= (simm12 & 0x7E0) << 20; // simm12[10:5] at bits 30-25
            machineCode |= (simm12 & 0x1E) << 7; // simm12[4:1] at bits 11-8
            machineCode |= (simm12 & 0x100) >> 4; // simm12[11] at bit 7

            // Set rs2 (bits 24-20)
            machineCode |= (uint)instruction.Rs1.Value.ToEnum<Register>() << 20;

            // Set rs1 (bits 19-15)
            machineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 15;

            // Set func3 (bits 14-12)
            machineCode |= (uint)instruction.Funct3 << 12;

            // Set opcode6 (bits 6-0)
            machineCode |= (uint)instruction.Opcode;

            return machineCode;
        }
    }
}
