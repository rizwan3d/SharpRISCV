using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public abstract class InstructionBase : IInstruction
    {
        public string Opcode { get; }
        public string Rd { get; set; } = string.Empty;
        public string Rs1 { get; set; } = string.Empty;
        public string Rs2 { get; set; } = string.Empty;

        protected InstructionBase(IToken token)
        {
            Opcode = token.Value;
        }

        public abstract bool IsComplete();

        public abstract bool IsRd();

        public abstract bool IsRs1();

        public abstract bool IsRs2();

        public abstract void ProcessOperand(IToken token);
    }
}