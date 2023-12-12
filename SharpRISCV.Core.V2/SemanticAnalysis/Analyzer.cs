using SharpRISCV.Core.V2.FirstPass;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public abstract class Analyzer : IAnalyzer
    {
        public abstract void Analyze(IInstruction Instruction, ISymbolTable symbolTable);

        protected bool IsRegister(IToken Instruction)
        {
            try
            {
                Instruction.Value.ToEnum<Register>();
                return true;
            }
            catch
            {
                throw new Exception($"invlid section definition at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
            }
        }


        protected bool IsLable(IToken Instruction, ISymbolTable symbolTable)
        {
            try
            {
                var val = symbolTable[Instruction.Value];
                return true;
            }
            catch
            {
                throw new Exception($"invlid lable at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
            }
        }

        protected bool IsImm(IToken Instruction)
        {
            if (Instruction.NumericVal is null)
                throw new Exception($"invlid offset or number at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
            return true;
        }
    }
}
