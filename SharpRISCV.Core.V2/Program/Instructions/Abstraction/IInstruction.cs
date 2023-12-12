using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions.Abstraction
{
    public interface IInstruction
    {
        bool IsComplete();
        bool IsRd();
        bool IsRs1();
        bool IsRs2();

        InstructionType InstructionType { get; }

        Mnemonic Mnemonic { get; }
        OpCode Opcode { get; }
        Funct3 Funct3 { get; }
        Funct7 Funct7 { get; }
        IToken Rd { get; set; }
        IToken Rs1 { get; set; }
        IToken Rs2 { get; set; }

        void ProcessOperand(IToken token);
    }
}