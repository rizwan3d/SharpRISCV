using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree
{
    public abstract class RiscVTreeBase : IRiscVTree
    {
        protected readonly ILexer lexer;

        public abstract List<ISection> ParseProgram();

        protected RiscVTreeBase(ILexer lexer)
        {
            this.lexer = lexer;
            this.lexer.Reset();
        }
    }
}