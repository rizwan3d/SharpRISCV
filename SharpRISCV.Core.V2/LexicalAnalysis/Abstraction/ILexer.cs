namespace SharpRISCV.Core.V2.LexicalAnalysis.Abstraction
{
    public interface ILexer
    {
        string Text { get; }
        Token GetNextToken();
        IEnumerable<Token> Tokenize();
    }
}