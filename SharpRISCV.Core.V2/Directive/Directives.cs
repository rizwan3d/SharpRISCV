using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Directive
{
    public static class Directives
    {
        public static bool IsText(IToken token)
        {
            return token.Value.Equals(".text");
        }

        public static bool IsData(IToken token)
        {
            return token.Value.Equals(".data");
        }
        public static bool IsSection(IToken token)
        {
            return IsText(token) || IsData(token);
        }

        public static bool IsSizeModifier(IToken token)
        {
            throw new NotImplementedException();
        }

        internal static bool IsString(IToken token)
        {
            return token.Value.Equals(".string") || token.Value.Equals(".asciz");
        }

        internal static bool IsWord(IToken token)
        {
            return token.Value.Equals(".word");
        }
    }
}
