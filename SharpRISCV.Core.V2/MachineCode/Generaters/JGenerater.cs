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
    public class JGenerater : Generater, IMachineCodeGenerateStrategy
    {
        public override uint Generate(IInstruction instruction, ISymbolTable symbolTable, uint address)
        {
            uint machineCode = 0;

            // Calculate simm20 from the instruction's symbol and the current address
            // simm20 is the offset from the current instruction address to the target address.
            uint targetAddress = GetIntValForImm(instruction.Rs1, symbolTable, address);
            int offset = (int)(targetAddress - (address + Setting.InstructionSize)); // because of PC increment after execution
            uint simm20 = (uint)offset;

            // Set simm20 parts in machine code:
            // - simm20[20] goes to bit 31
            machineCode |= (simm20 & 0x80000) << 11;

            // - simm20[10:1] goes to bits 30-21
            machineCode |= (simm20 & 0x7FE) << 20;

            // - simm20[11] goes to bit 20
            machineCode |= (simm20 & 0x1000) << 9;

            // - simm20[19:12] goes to bits 19-12
            machineCode |= (simm20 & 0xFF000) >> 9;

            // Set rd (bits 11-7)
            machineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 7;

            // Set opcode6 (bits 6-0)
            machineCode |= (uint)instruction.Opcode;

            return machineCode;
        }
    }
}
