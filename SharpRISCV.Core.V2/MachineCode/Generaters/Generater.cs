using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.MachineCode.Generaters
{
    public abstract class Generater : IMachineCodeGenerateStrategy
    {
        public abstract uint Generate(IInstruction instruction, ISymbolTable symbolTable, uint address);

        protected uint GetIntValForImm(IToken imm, ISymbolTable symbolTable, uint address)
        {
            if (imm.NumericVal is null) return GetIntValForImm(imm.Value, imm, symbolTable, address);
            return (uint)imm.NumericVal;
        }

        private uint GetIntValForImm(string imm, IToken token, ISymbolTable symbolTable, uint address)
        {
            try
            {
                return (uint)imm.ToEnum<Register>();
            }
            catch { }

            Regex regex = new(Pattern.ValuesInRelocationFunction);
            var matchs = regex.Matches(imm);
            if (matchs.Count > 0)
            {
                string matchVal = (matchs[0] as Match)?.Groups[1].Value ?? string.Empty;
                return ProcessRelocationFunction(imm.Between("%", "("), GetIntValForImm(matchVal, token, symbolTable, address), address, token);
            }

            return symbolTable[imm].Address - address;
        }

        private uint ProcessRelocationFunction(string functionName, uint imm, uint pc, IToken token)
        {
            switch (functionName)
            {
                case "hi":
                    return (imm >> 16) & 0xFFFF;

                case "lo":
                    return imm & 0xFFFF;

                case "pcrel_hi":
                    return ((imm - pc) >> 16) & 0xFFFF;

                case "pcrel_lo":
                    return (imm - pc) & 0xFFFF;

                case "tprel_hi":
                //return ((imm - tp) >> 16) & 0xFFFF;

                case "tprel_lo":
                //return (imm - tp) & 0xFFFF;

                case "tprel_add":
                //return imm + tp;

                default:
                    throw new Exception($"invlid relocation function {functionName} at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
            }
        }

        protected uint EffectiveAddress (int offset,int baseRegister) 
        {
            return (uint)(baseRegister + offset);
        }

    }
}
