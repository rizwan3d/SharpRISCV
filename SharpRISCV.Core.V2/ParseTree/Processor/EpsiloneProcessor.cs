using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    internal class EpsiloneProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            ProcessInstruction(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
            ProcessData(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
        }

        private void ProcessInstruction(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentInstruction is not null && CurrentInstruction.IsComplete())
                if (CurrentSections is null)
                    throw new Exception($"invlid section definition at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
                else if (CurrentSections is not ITextSection)
                    throw new Exception($"Instruction only allowed in text section at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
                else
                    CurrentSections.Instructions.Add(CurrentInstruction);
        }

        private void ProcessData(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentData is not null)
                if (CurrentData.IsComplete())
                    CurrentSections.Data.Add(CurrentData);
                else
                    throw new Exception($"Invalid Instruction at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
