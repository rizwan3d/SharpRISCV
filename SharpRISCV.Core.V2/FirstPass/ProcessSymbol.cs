using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed class ProcessSymbol(ILexer lexer, ISymbolTable symbolTable) : ProcessSymbolBase(lexer, symbolTable), IProcessSymbol
    {
        private uint CurrentAddress = 0;
        public override void Start()
        {
            var token = lexer.GetNextToken();
            while (!IToken.IsEndOfFile(token))
            {
                if (IToken.IsInstruction(token))
                    CurrentAddress += Setting.InstructionSize;

                if (IToken.IsLableDefinition(token))
                {
                    symbolTable.Add(token.Value.Replace(":", string.Empty), CurrentAddress);
                }

                token = lexer.GetNextToken();
            }
        }
    }
}
