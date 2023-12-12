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

        public void ExecuteStrategy(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (strategy is null)
                throw new Exception("Invalid Token Process Strategy");

            strategy.Process(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
        }
    }
}
