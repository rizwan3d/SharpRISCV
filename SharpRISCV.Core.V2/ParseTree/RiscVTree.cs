using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Processor;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree
{
    class RiscVTree : RiscVTreeBase, IRiscVTree
    {
        private Dictionary<TokenType, ITokenProcessStrategy> ProcessStrategys { get; }

        private List<ISection> Sections { get; } = [];

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        private ISection CurrentSections = null;
        private IInstruction CurrentInstruction = null;
        private IData CurrentData = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        public RiscVTree(ILexer lexer) : base(lexer)
        {
            ProcessStrategys = new Dictionary<TokenType, ITokenProcessStrategy>();
            ProcessStrategys.Add(TokenType.DIRECTIVE, new DirectiveProcessor());
            ProcessStrategys.Add(TokenType.INSTRUCTION, new InstructionProcessor());
            ProcessStrategys.Add(TokenType.INTEGER, new NumberProcessor());
            ProcessStrategys.Add(TokenType.HEX, new NumberProcessor());
            ProcessStrategys.Add(TokenType.FLOAT, new NumberProcessor());
            ProcessStrategys.Add(TokenType.BINARY, new NumberProcessor());
            ProcessStrategys.Add(TokenType.LABEL, new LabelProcessor());
            ProcessStrategys.Add(TokenType.STRING, new StringProcessor());
        }

        public override List<ISection> ParseProgram()
        {
            TokenProcessContext tokenProcessContext = new();

            var token = lexer.GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                ITokenProcessStrategy processStrategy = ProcessStrategys[token.TokenType];
                tokenProcessContext.SetStrategy(processStrategy);
                tokenProcessContext.ExecuteStrategy(Sections, CurrentSections, CurrentInstruction, CurrentData, token); ;

                token = lexer.GetNextToken();
            }

            return Sections;
        }
    }
}
