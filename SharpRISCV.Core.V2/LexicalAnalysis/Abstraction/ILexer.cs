using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.LexicalAnalysis.Abstraction
{
    public interface ILexer
    {
        string Text { get; }

        void Reset();
        IToken GetNextToken();
        IEnumerable<IToken> Tokenize();
    }
}