using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree
{
    public class TokenProcessContext
    {
        private ITokenProcessStrategy? strategy = null;

        public void SetStrategy(ITokenProcessStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void ExecuteStrategy(IList<ISection> Sections, ISection CurrentSections, ref IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            strategy?.Process(Sections, CurrentSections, ref CurrentInstruction, CurrentData, token);
        }
    }
}
