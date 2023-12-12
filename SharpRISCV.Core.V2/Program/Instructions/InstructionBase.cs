using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public abstract class InstructionBase : IInstruction
    {
        public IToken Token { get; }
        public OpCode Opcode { get { return Token.Value.ToEnum<OpCode>(); } }
        public Funct3 Funct3 { get { return Token.Value.ToEnum<Funct3>(); } }
        public Funct7 Funct7 { get { return Token.Value.ToEnum<Funct7>(); } }
        public Mnemonic Mnemonic { get { return Token.Value.ToUpper().ToEnum<Mnemonic>(); } }
        public IToken Rd { get; set; }
        public IToken Rs1 { get; set; }
        public IToken Rs2 { get; set; }
        public InstructionType InstructionType { get; protected set; }

        protected InstructionBase(IToken token)
        {
            try
            {
                token.Value.ToUpper().ToEnum<Mnemonic>();
            }
            catch (Exception)
            {
                throw new Exception($"invlid mnemonic at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
            }
            Token = token;
            IdentifyInstructionType();
        }

        public abstract bool IsComplete();

        public abstract bool IsRd();

        public abstract bool IsRs1();

        public abstract bool IsRs2();

        public abstract void ProcessOperand(IToken token);

        protected abstract void IdentifyInstructionType();
    }
}