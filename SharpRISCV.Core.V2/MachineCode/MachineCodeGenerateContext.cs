using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.MachineCode
{
    public class MachineCodeGenerateContext
    {
        private IMachineCodeGenerateStrategy? strategy = null;

        public void SetStrategy(IMachineCodeGenerateStrategy strategy)
        {
            this.strategy = strategy;
        }

        public uint ExecuteStrategy(IInstruction instruction, ISymbolTable symbolTable)
        {
            if (strategy is null)
                throw new Exception("Invalid Machine Code Generater Strategy");

            return strategy.Generate(instruction, symbolTable);
        }
    }
}
