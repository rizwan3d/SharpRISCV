using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.SemanticAnalysis.Abstraction
{
    public interface IAnalyzer
    {
        void Analyze(IInstruction Instruction, ISymbolTable symbolTable);
    }
}
