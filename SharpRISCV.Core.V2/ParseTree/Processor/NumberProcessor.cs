﻿using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class NumberProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentSections is IDataSection || CurrentSections is IBssSection)
            {
                CurrentData.SetData(token);
                CurrentSections.Data.Add(CurrentData);
                return;
            }
            if (CurrentInstruction is null)
                throw new Exception($"Invalid Instruction at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");

            CurrentInstruction.ProcessOperand(token);
        }
    }
}
