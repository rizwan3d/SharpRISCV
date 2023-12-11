using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class InstructionProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentInstruction is not null && CurrentInstruction.IsComplete())
                CurrentSections.Instructions.Add(CurrentInstruction);

            CurrentInstruction = InstructionFactory.CreateInstruction(token);
        }
    }
}
