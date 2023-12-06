using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using System.Text.RegularExpressions;

namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public class LexerBase : ILexer
    {
        public string Text { get; protected set; } = string.Empty;
        public LexerBase(string text)
        {
            Text = text;
            Text = Regex.Replace(Text, Pattern.Comment, string.Empty, RegexOptions.Multiline);
        }

        public virtual IEnumerable<IToken> Tokenize()
        {
            throw new NotImplementedException();
        }

        public virtual IToken GetNextToken()
        {
            throw new NotImplementedException();
        }
    }
}
