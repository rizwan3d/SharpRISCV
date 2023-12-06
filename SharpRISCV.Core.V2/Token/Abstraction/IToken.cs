﻿using SharpRISCV.Core.V2.Token;

namespace SharpRISCV.Core.V2.Token.Abstraction
{
    public interface IToken
    {
        TokenType TokenType { get; }
        string Value { get; }
        int StartIndex { get; }
        int Length { get; }
        static IToken? EndOfFile { get; }
        static bool IsEndOfFile(IToken token) => token.TokenType == TokenType.EPSILONE;
    }
}