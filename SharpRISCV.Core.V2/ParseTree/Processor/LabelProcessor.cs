using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class LabelProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ISection CurrentSections, IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            CurrentInstruction.ProcessOperand(token);
        }
    }
}
