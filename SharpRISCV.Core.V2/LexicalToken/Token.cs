using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.LexicalToken
{
    public record Token(TokenType tokenType, string value, int startIndex, int lineNumber, int length) : IToken
    {
        public TokenType TokenType { get { return tokenType; } }
        public string Value { get { return value; } }
        public int StartIndex { get { return startIndex; } }
        public int Length { get { return length; } }
        public int LineNumber { get { return lineNumber; } }

        public static IToken EndOfFile { get => new Token(TokenType.EPSILONE, "EOF", 0, 0, 0); }

        public int? NumericVal
        {
            get
            {
                int? inval = null;
                if (TokenType == TokenType.HEX)
                {
                    inval = Convert.ToInt32(Value.Substring(2), 16);
                }
                else if (TokenType == TokenType.BINARY)
                {
                    inval = Convert.ToInt32(Value.Substring(2), 2);
                }
                else if (TokenType == TokenType.INTEGER)
                {
                    inval = Convert.ToInt32(Value);
                }

                return inval;
            }
        }

        public static IToken FromToken(IToken token)
        {
            return new Token(token.TokenType, token.Value, token.StartIndex, token.LineNumber, token.Length);
        }
    }
}
