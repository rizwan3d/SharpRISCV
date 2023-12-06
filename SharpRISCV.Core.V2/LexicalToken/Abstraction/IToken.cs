using SharpRISCV.Core.V2.LexicalToken;

namespace SharpRISCV.Core.V2.LexicalToken.Abstraction
{
    public interface IToken
    {
        TokenType TokenType { get; }
        string Value { get; }
        int StartIndex { get; }
        int Length { get; }
        static IToken? EndOfFile { get; }
        static bool IsEndOfFile(IToken token) => token.TokenType == TokenType.EPSILONE;
        static bool IsInstruction(IToken token) => token.TokenType == TokenType.INSTRUCTION;
        static bool IsLableDefinition(IToken token) => token.TokenType == TokenType.LABELDEFINITION;
    }
}