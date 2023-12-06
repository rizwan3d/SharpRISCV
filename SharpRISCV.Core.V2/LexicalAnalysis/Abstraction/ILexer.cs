namespace SharpRISCV.Core.V2.LexicalAnalysis.Abstraction
{
    public interface ILexer
    {
        string Text { get; }
        IToken GetNextToken();
        IEnumerable<IToken> Tokenize();
    }
}