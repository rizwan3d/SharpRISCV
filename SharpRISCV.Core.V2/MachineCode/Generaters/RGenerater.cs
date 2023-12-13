using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode.Generaters
{
    public class RGenerater : IMachineCodeGenerateStrategy
    {
        public IEnumerable<byte> Generate(IEnumerable<ISection> sections, ISymbolTable symbolTable)
        {
            return [];
        }
    }
}
