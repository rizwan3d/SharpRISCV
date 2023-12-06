using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.FirstPass
{
    public abstract class ProcessSymbolBase : IProcessSymbol
    {
        protected readonly ILexer lexer;
        protected readonly ISymbolTable symbolTable;
        protected ProcessSymbolBase(ILexer lexer, ISymbolTable symbolTable)
        {
            this.lexer = lexer;
            this.symbolTable = symbolTable;
        }

        public virtual void Start()
        {
        }
    }
}
