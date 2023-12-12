using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.Program.Sections
{
    public class SectionFactory
    {
        public static ISection CreateSection(IToken token)
        {
            if (Directives.IsText(token)) return new TextSection();
            if (Directives.IsData(token)) return new DataSection();
            if (Directives.IsBss(token)) return new BssSection();

            throw new InvalidOperationException($"Invalid section directives {token.Value} at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
