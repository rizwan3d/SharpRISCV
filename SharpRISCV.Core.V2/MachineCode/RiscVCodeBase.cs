using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode
{
    public abstract class RiscVCodeBase
    {

        protected readonly IEnumerable<ISection> sections;
        protected readonly ISymbolTable symbolTable;
        protected Dictionary<InstructionType, IMachineCodeGenerateStrategy> GenerateStrategys { get; set; } = [];

        protected uint ProcessedByte { get; private set; } = 0;
        protected RiscVCodeBase(IEnumerable<ISection> sections, ISymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;
            this.sections = sections;

            Init();
        }

        protected abstract void Init();

        protected void ProcessedBytes(IEnumerable<Byte> bytes)
        {
            ProcessedByte += (uint)bytes.Count();
        }
    }
}
