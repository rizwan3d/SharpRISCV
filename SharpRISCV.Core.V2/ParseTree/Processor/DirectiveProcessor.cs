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
        public void Process(IList<ISection> Sections, ISection CurrentSections, ref IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            if (Directives.IsSection(token))
            {
                ProcessSection(Sections, CurrentSections, CurrentInstruction, CurrentData, token);
            }
            else
            {
                ProcessOtherDirective(Sections, CurrentSections, CurrentInstruction, CurrentData, token);
            }
        }

        private void ProcessSection(IList<ISection> Sections, ISection CurrentSections, IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            if (CurrentInstruction is not null && CurrentInstruction.IsComplete())
                CurrentSections.Instructions.Add(CurrentInstruction);

            CurrentSections = SectionFactory.CreateSection(token);
            Sections.Add(CurrentSections);
        }

        private void ProcessOtherDirective(IList<ISection> Sections, ISection CurrentSections, IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            if (CurrentData is not null && CurrentData.IsComplete())
                CurrentSections.Data.Add(CurrentData);

            CurrentData = DataFactory.CreateData(token);
        }
    }
}
