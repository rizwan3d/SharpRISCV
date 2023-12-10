using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System.Linq;

namespace SharpRISCV.Core.V2
{
    public static class Setting
    {
        public static readonly uint InstructionSize = 4;
        private static readonly List<string> TwoBaseInstruction = ["la", "lb", "lh", "lw", "ld", "sb", "sh", "sw", "sd", "flw", "fls", "fsw", "fsd", "call", "tail"];
        public static bool HasTwoBaseInstruction(IToken token) => TwoBaseInstruction.Contains(token.Value);
    }
}
