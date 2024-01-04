using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System.Text.RegularExpressions;

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
                throw new Exception($"invlid register at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
            }
        }


        protected bool IsLabel(IToken Instruction, ISymbolTable symbolTable)
        {
            try
            {
                Regex regex = new(Pattern.ValuesInRelocationFunction);
                var matchs = regex.Matches(Instruction.Value);
                if (matchs.Count > 0)
                {
                    foreach (var match in matchs)
                    {
                        string matchVal = (match as Match)?.Groups[1].Value ?? string.Empty;
                        try
                        {
                            matchVal?.ToEnum<Register>();
                        }
                        catch
                        {
                            try
                            {
                                var rempval = symbolTable[matchVal];
                            }
                            catch
                            {
                                throw new Exception($"invlid register at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
                            }
                        }
                    }
                    return true;
                }
                
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
            if (Instruction.NumericVal is null && !Instruction.Value.Contains("%"))
                throw new Exception($"invlid offset or number at Line Number: {Instruction.LineNumber}, Char: {Instruction.StartIndex}.");
            return true;
        }

        protected int OperandCount(IInstruction Instruction)
        {
            int count = 0;
            if (!Instruction.IsRd()) count++;
            if (!Instruction.IsRs1()) count++;
            if (!Instruction.IsRs2()) count++;
            return count;
        }
    }
}
