using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class LexerBase : ILexer
    {
        public string Text { get; protected set; } = string.Empty;
        public LexerBase(string text)
        {
            Text = text;
        }

        public virtual IEnumerable<Token> Tokenize()
        {
            throw new NotImplementedException();
        }
    }
}
