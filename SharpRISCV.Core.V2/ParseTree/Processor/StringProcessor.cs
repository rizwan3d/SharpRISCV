using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public class StringProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ISection CurrentSections, IInstruction CurrentInstruction, IData CurrentData, IToken token)
        {
            if (CurrentSections is IDataSection)
            {
                CurrentData.SetData(token);
                CurrentSections.Data.Add(CurrentData);
            }
            else
            {
                throw new Exception($"Invalid Instruction at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
            }
        }
    }
}
