namespace SharpRISCV.Core.V2.LexicalAnalysis.Abstraction
{
    public interface IToken
    {
        TokenType TokenType { get; }
        string Value { get; }
        int StartIndex { get; }
        int Length { get; }
    }
}