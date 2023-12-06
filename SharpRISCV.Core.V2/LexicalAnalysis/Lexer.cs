using System.Text.RegularExpressions;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.Token;
using SharpRISCV.Core.V2.Token.Abstraction;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class Lexer(string text) : LexerBase(text), ILexer
    {
        private int Position { get; set; } = 0;
        private IEnumerable<IToken> tokens;

        private new Dictionary<TokenType, Regex> Rules = new Dictionary<TokenType, Regex>
        {
            { TokenType.WHITESPACE, new Regex(Pattern.WhiteSpace) },
            { TokenType.COMMENT, new Regex(Pattern.Comment) },
            { TokenType.COMMA, new Regex(Pattern.Comma) },
            { TokenType.STRING, new Regex(Pattern.String) },
            { TokenType.FLOAT, new Regex(Pattern.Float) },
            { TokenType.HEX, new Regex(Pattern.Hex) },
            { TokenType.BINARY, new Regex(Pattern.Binary) },
            { TokenType.INTEGER, new Regex(Pattern.Integer) },
            { TokenType.REGISTER, new Regex(Pattern.Register) },
            { TokenType.INSTRUCTION, new Regex(Pattern.Instruction) },
            { TokenType.DIRECTIVE, new Regex(Pattern.Directive) },
            { TokenType.LABELDEFINITION, new Regex(Pattern.LabeDefinition) },
            { TokenType.LABEL, new Regex(Pattern.Lable) },
        };

        private List<TokenType> IgnoredToken = [TokenType.COMMENT, TokenType.WHITESPACE, TokenType.COMMA];

        public override IToken GetNextToken()
        {
            Token token = null;
            if (Position < Text.Length)
            {
                while (token is null)
                {
                    foreach (var rule in Rules)
                    {
                        Match match = null;
                        try
                        {
                            match = rule.Value.Match(Text, Position);
                        }
                        catch (ArgumentOutOfRangeException error)
                        {
                            return Token.EndOfFile;
                        }
                        if (match.Success && match.Index == Position && !IgnoredToken.Contains(rule.Key))
                        {
                            token = new Token(rule.Key, match.Value, Position, match.Value.Length);
                            Position += match.Length;
                            break;
                        }
                    }

                    if (token == null)
                    {
                        Position++;
                    }
                    else
                    {
                        return token;
                    }
                }
            }
            return Token.EndOfFile;
        }

        public override IEnumerable<IToken> Tokenize()
        {
            List<IToken> tokenList = [];
            Position = 0;
            IToken token = GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                tokenList.Add(token);
                token = GetNextToken();
            }

            return tokenList;
        }
    }
}
