﻿using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System.Text.RegularExpressions;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class Lexer(string text) : LexerBase(text), ILexer
    {
        private int Position { get; set; } = 0;

        public IToken? CurrentToken { get; private set; }

        private readonly IDictionary<TokenType, Regex> Rules = new Dictionary<TokenType, Regex>
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
            { TokenType.LABEL, new Regex(Pattern.Label) },
        };

        private List<TokenType> IgnoredToken = [TokenType.COMMENT, TokenType.WHITESPACE, TokenType.COMMA];

        public override IToken GetNextToken()
        {
            Token? token = null;
            if (Position < Text.Length)
            {
                while (token is null)
                {
                    foreach (var rule in Rules)
                    {
                        Match? match = null;
                        try
                        {
                            match = rule.Value.Match(Text, Position);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return CurrentToken = Token.EndOfFile;
                        }
                        if (match.Success && match.Index == Position && !IgnoredToken.Contains(rule.Key))
                        {
                            int lineNumber = Text.Take(Position).Count(c => c == '\n') + 1;
                            token = new Token(rule.Key, match.Value, Position, lineNumber, match.Value.Length);
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
                        return CurrentToken = token;
                    }
                }
            }
            return CurrentToken = Token.EndOfFile;
        }

        public override IEnumerable<IToken> Tokenize()
        {
            List<IToken> tokenList = [];
            Reset();
            IToken token = GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                tokenList.Add(token);
                token = GetNextToken();
            }

            return tokenList;
        }

        public override void Reset()
        {
            Position = 0;
        }
    }
}
