using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using static System.Collections.Specialized.BitVector32;

namespace SharpRISCV.Core.V2.MachineCode
{
    public interface IMachineCodeGenerateStrategy
    {
        IEnumerable<Byte> Generate(IEnumerable<ISection> sections, ISymbolTable symbolTable);
    }
    public class RiscVCode(IEnumerable<ISection> sections, ISymbolTable symbolTable)
    {
        private Dictionary<InstructionType, IMachineCodeGenerateStrategy> GenerateStrategys { get; }

        public void Build()
        {
            MachineCodeGenerateContext machineCodeGenerateContext = new();
            foreach (ISection section in sections)
            {
                if (section is ITextSection)
                {
                    foreach (IInstruction ins in section.Instructions)
                    {
                        IMachineCodeGenerateStrategy generateStrategy = GenerateStrategys[ins.InstructionType];
                        machineCodeGenerateContext.SetStrategy(generateStrategy);
                        section.Bytes.AddRange(machineCodeGenerateContext.ExecuteStrategy(sections, symbolTable));
                    }
                }
                if (section is IDataSection || section is IBssSection)
                { 

                }
            }
        }
    }
}
