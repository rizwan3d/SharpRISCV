using System.Text.RegularExpressions;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class Token(TokenType tokenType, string value, int startIndex, int length) : IToken
    {
        public TokenType TokenType { get { return tokenType; } }
        public string Value { get { return value; } }
        public int StartIndex { get { return startIndex; } }
        public int Length { get { return length; } }

        public static Token EndOfFile { get => new Token(TokenType.EPSILONE,"EOF", 0, 0); }
    }
}
