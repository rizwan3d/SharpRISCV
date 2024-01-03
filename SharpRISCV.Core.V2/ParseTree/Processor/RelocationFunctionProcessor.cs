using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System.Text.RegularExpressions;

namespace SharpRISCV.Core.V2.ParseTree.Processor
{
    public sealed class RelocationFunctionProcessor : ITokenProcessStrategy
    {
        public void Process(IList<ISection> Sections, ref ISection CurrentSections, ref IInstruction CurrentInstruction, ref IData CurrentData, IToken token)
        {
            var trimedValue = token.Value.Trim();
            if (!trimedValue.StartsWith("%"))
                return;

            Regex patteren = new(Pattern.RelocationFunction);
            var match = patteren.Match(trimedValue, 0);

            if (match.Success)
                return;
            throw new Exception($"Invalid Relocation Functions at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
