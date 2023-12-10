using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Abstraction
{
    public interface ITokenProcessStrategy
    {
        void Process(IList<ISection> Sections, ISection CurrentSections, IInstruction CurrentInstruction, IData CurrentData, IToken token);
    }
}
