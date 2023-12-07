using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed class ProcessSymbol(ILexer lexer, ISymbolTable symbolTable) : ProcessSymbolBase(lexer, symbolTable), IProcessSymbol
    {
        private IToken CurrentDirective;
        private uint CurrentAddress = 0;
        public override void Start()
        {
            var token = lexer.GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                if (IToken.IsDirective(token) && Directives.IsSection(token))
                    CurrentDirective = token;

                if (IToken.IsInstruction(token))
                    CurrentAddress += Setting.InstructionSize;

                if (IToken.IsLableDefinition(token) && Directives.IsText(CurrentDirective))
                {
                    symbolTable.Add(token.Value.Replace(":", string.Empty), CurrentAddress);
                }

                if (IToken.IsLableDefinition(token) && Directives.IsData(CurrentDirective))
                {
                    string lableName = token.Value.Replace(":", string.Empty);
                    token = lexer.GetNextToken();
                    uint incrementedAddress = 0;

                    if (IToken.IsDirective(token) && Directives.IsString(token))
                    {
                        token = lexer.GetNextToken();

                        if(token.TokenType != LexicalToken.TokenType.STRING)
                            throw new Exception("Invalid Use of word directive.");

                        string text = token.Value.Replace("\\n", "\n");
                        uint size = (uint)new UTF8Encoding(true).GetBytes(text).Length + 1;
                        incrementedAddress += size;
                    }
                    else if (IToken.IsDirective(token) && Directives.IsWord(token))
                    {
                        token = lexer.GetNextToken();
                        int? inval = null;
                        if (token.TokenType == LexicalToken.TokenType.HEX)
                        {
                            inval = Convert.ToInt32(token.Value.Substring(2), 16);
                        }
                        else if (token.TokenType == LexicalToken.TokenType.BINARY)
                        {
                            inval = Convert.ToInt32(token.Value.Substring(2), 2);
                        }
                        else if (token.TokenType == LexicalToken.TokenType.INTEGER)
                        {
                            inval = Convert.ToInt32(token.Value);
                        }

                        if (inval is null)
                            throw new Exception("Invalid Use of word directive.");

                        byte[] byteArray = BitConverter.GetBytes((int)inval);
                        incrementedAddress += (uint)byteArray.Length + 1;
                    }

                    symbolTable.Add(lableName, CurrentAddress);
                    CurrentAddress += incrementedAddress;
                }

                token = lexer.GetNextToken();
            }
        }

    }
}
