using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class LabelProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentInstruction is null)
                throw new Exception($"Invalid Instruction at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");

            CurrentInstruction.ProcessOperand(token);
            LableWithRelocation(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
        }

        private void LableWithRelocation(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            ITokenProcessStrategy processStrategy = new RelocationFunctionProcessor();
            processStrategy.Process(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
        }
    }
}
