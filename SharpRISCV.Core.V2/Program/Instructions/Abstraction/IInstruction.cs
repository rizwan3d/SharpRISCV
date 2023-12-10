using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions.Abstraction
{
    public interface IInstruction
    {
        bool IsComplete();
        bool IsRd();
        bool IsRs1();
        bool IsRs2();

        string Opcode { get; }
        string Rd { get; set; }
        string Rs1 { get; set; }
        string Rs2 { get; set; }

        void ProcessOperand(IToken token);
    }
}