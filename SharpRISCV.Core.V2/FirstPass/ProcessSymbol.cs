using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Linq;
using System.Text;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed class ProcessSymbol(ILexer lexer, ISymbolTable symbolTable) : ProcessSymbolBase(lexer, symbolTable), IProcessSymbol
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        private IToken CurrentDirective = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        private uint CurrentAddress = 0;
        public override void Start()
        {
            var token = lexer.GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                if (IToken.IsDirective(token) && !Directives.IsValid(token))
                    throw new Exception($"Invalid directives {token.Value} at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");

                if (IToken.IsDirective(token) && Directives.IsSection(token))
                    CurrentDirective = token;

                else if (IToken.IsInstruction(token))
                    if (Setting.HasTwoBaseInstruction(token))
                        CurrentAddress += Setting.InstructionSize * 2;
                    else
                        CurrentAddress += Setting.InstructionSize;

                else if (IToken.IsLabelDefinition(token) && Directives.IsText(CurrentDirective))
                {
                    symbolTable.Add(token, CurrentAddress);
                }

                else if (IToken.IsLabelDefinition(token) && Directives.IsData(CurrentDirective))
                {
                    IToken lable = Token.FromToken(token);
                    token = lexer.GetNextToken();
                    uint incrementedAddress = 0;

                    if (IToken.IsDirective(token) && Directives.IsString(token))
                    {
                        token = lexer.GetNextToken();

                        if (token.TokenType != TokenType.STRING)
                            throw new Exception($"Invalid Use of word directive at {token.LineNumber}, Char: {token.StartIndex}.");

                        string text = token.Value.Replace("\\n", "\n");
                        uint size = (uint)new UTF8Encoding(true).GetBytes(text).Length + 1;
                        incrementedAddress += size;
                    }
                    else if (IToken.IsDirective(token) && Directives.IsWord(token))
                    {
                        token = lexer.GetNextToken();

                        if (token.NumericVal is null)
                            throw new Exception($"Invalid Use of word directive at {token.LineNumber}, Char: {token.StartIndex}.");

                        byte[] byteArray = BitConverter.GetBytes(token.NumericVal.Value);
                        incrementedAddress += (uint)byteArray.Length + 1;
                    }
                    else
                        throw new Exception($"Invalid Use of directive at {token.LineNumber}, Char: {token.StartIndex}.");

                    symbolTable.Add(lable, CurrentAddress);
                    CurrentAddress += incrementedAddress;
                }

                else if (IToken.IsLabelDefinition(token) && Directives.IsBss(CurrentDirective))
                {
                    IToken lable = Token.FromToken(token);
                    token = lexer.GetNextToken();
                    uint incrementedAddress = 0;

                    if (IToken.IsDirective(token) && Directives.IsSpace(token))
                    {
                        token = lexer.GetNextToken();

                        if (token.NumericVal is null)
                            throw new Exception($"Invalid Use of space directive at {token.LineNumber}, Char: {token.StartIndex}.");

                        byte[] byteArray = new byte[token.NumericVal.Value];
                        incrementedAddress += (uint)byteArray.Length + 1;

                        symbolTable.Add(lable, CurrentAddress);
                        CurrentAddress += incrementedAddress;
                    }
                    else
                        throw new Exception($"Invalid Use of directive at {token.LineNumber}, Char: {token.StartIndex}.");
                }
                token = lexer.GetNextToken();
            }
        }

    }
}
