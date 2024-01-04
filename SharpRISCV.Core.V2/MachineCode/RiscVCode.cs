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
using System.Runtime.CompilerServices;
using SharpRISCV.Core.V2.MachineCode.Generaters;

namespace SharpRISCV.Core.V2.MachineCode
{
    public interface IMachineCodeGenerateStrategy
    {
        uint Generate(IInstruction instruction, ISymbolTable symbolTable);
    }

    public class RiscVCode(IEnumerable<ISection> sections, ISymbolTable symbolTable) : RiscVCodeBase(sections, symbolTable)
    {
        protected override void Init()
        {
            GenerateStrategys.Add(InstructionType.R, new RGenerater());
        }

        public void Build()
        {
            MachineCodeGenerateContext machineCodeGenerateContext = new();
            foreach (ISection section in sections)
            {
                if (section is ITextSection)
                {
                    foreach (IInstruction ins in section.Instructions)
                    {
                        if (!GenerateStrategys.ContainsKey(ins.InstructionType)) continue;
                        IMachineCodeGenerateStrategy generateStrategy = GenerateStrategys[ins.InstructionType];
                        machineCodeGenerateContext.SetStrategy(generateStrategy);
                        ins.MachineCode = machineCodeGenerateContext.ExecuteStrategy(ins, symbolTable);
                        section.Bytes.AddRange(ins.MachineCode.ToBytes());
                    }
                }
                if (section is IDataSection || section is IBssSection)
                {

                }
            }
        }
    }
}
