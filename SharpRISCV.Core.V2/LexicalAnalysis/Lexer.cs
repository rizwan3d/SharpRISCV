using System.Text.RegularExpressions;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class Lexer(string text) : LexerBase(text), ILexer
    {
        private IEnumerable<Token> tokens;

        private new Dictionary<TokenType, Regex> Rules = new Dictionary<TokenType, Regex>
        {
            { TokenType.WHITESPACE, new Regex(Pattern.WhiteSpace) },
            { TokenType.COMMENT, new Regex(Pattern.Comment) },
            { TokenType.Comma, new Regex(Pattern.Comma) },
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

        private List<TokenType> IgnoredToken = [TokenType.COMMENT, TokenType.WHITESPACE, TokenType.Comma];

        public override IEnumerable<Token> Tokenize()
        {
            List<Token> tokenList = new List<Token>();
            Text = Regex.Replace(Text, Pattern.Comment, string.Empty, RegexOptions.Multiline);

            int position = 0;
            while (position < Text.Length)
            {
                Token token = null;
                foreach (var rule in Rules)
                {
                    Match match = rule.Value.Match(Text, position);
                    if (match.Success && match.Index == position)
                    {
                        token = new Token(rule.Key, match.Value, position, match.Value.Length);
                        position += match.Length;
                        break;
                    }
                }

                if (token == null)
                {
                    position++;
                }
                else
                {
                    tokenList.Add(token);
                }
            }

            return tokenList.Where(token => !IgnoredToken.Contains(token.TokenType));
        }
    }
}
