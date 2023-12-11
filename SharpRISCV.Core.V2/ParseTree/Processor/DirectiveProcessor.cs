using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class DirectiveProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (Directives.IsSection(token))
            {
                ProcessSection(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
            }
            else
            {
                ProcessOtherDirective(Sections, ref CurrentSections, ref CurrentInstruction, ref CurrentData, token);
            }
        }

        private void ProcessSection(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentInstruction is not null && CurrentInstruction.IsComplete())
                if (CurrentSections is null)
                    throw new Exception($"invlid section definition at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
                else if (CurrentSections is not ITextSection)
                    throw new Exception($"Instruction only allowed in text section at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
                else
                    CurrentSections.Instructions.Add(CurrentInstruction);

            CurrentInstruction = null;
            CurrentSections = SectionFactory.CreateSection(token);
            Sections.Add(CurrentSections);
        }

        private void ProcessOtherDirective(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            if (CurrentData is not null)
                if (CurrentData.IsComplete())
                    CurrentSections.Data.Add(CurrentData);
                else
                    throw new Exception($"Invalid Instruction at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");

            CurrentData = DataFactory.CreateData(token);
        }
    }
}
