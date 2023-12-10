using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.Directive
{
    public static class Directives
    {
        static List<string> DirectivesList = [".text", ".data", ".space", ".string", ".asciz", ".word", ".bss"];

        public static bool IsValid(IToken token)
        {
            return DirectivesList.Contains(token.Value);
        }

        public static bool IsText(IToken token)
        {
            return token.Value.Equals(".text");
        }

        public static bool IsBss(IToken token)
        {
            return token.Value.Equals(".bss");
        }

        public static bool IsData(IToken token)
        {
            return token.Value.Equals(".data");
        }
        public static bool IsSection(IToken token)
        {
            return IsText(token) || IsData(token) || IsBss(token);
        }

        public static bool IsSpace(IToken token)
        {
            return token.Value.Equals(".space");
        }

        public static bool IsString(IToken token)
        {
            return token.Value.Equals(".string") || token.Value.Equals(".asciz");
        }

        public static bool IsWord(IToken token)
        {
            return token.Value.Equals(".word");
        }
    }
}
