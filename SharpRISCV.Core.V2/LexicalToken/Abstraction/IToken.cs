namespace SharpRISCV.Core.V2.LexicalToken.Abstraction
{
    public interface IToken
    {
        TokenType TokenType { get; }
        string Value { get; }
        int StartIndex { get; }
        int LineNumber { get; }
        int Length { get; }
        int? NumericVal { get; }
        static IToken? EndOfFile { get; }
        static bool IsEndOfFile(IToken token) => token.TokenType == TokenType.EPSILONE;
        static bool IsInstruction(IToken token) => token.TokenType == TokenType.INSTRUCTION;
        static bool IsLabelDefinition(IToken token) => token.TokenType == TokenType.LABELDEFINITION;
        static bool IsDirective(IToken token) => token.TokenType == TokenType.DIRECTIVE;
    }
}