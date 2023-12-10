using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    class Instruction(IToken token) : InstructionBase(token), IInstruction
    {
        public override bool IsComplete()
        {
            return !string.IsNullOrEmpty(Opcode);
        }

        public override bool IsRd()
        {
            return string.IsNullOrEmpty(Rd);
        }

        public override bool IsRs1()
        {
            return string.IsNullOrEmpty(Rs1);
        }

        public override bool IsRs2()
        {
            return string.IsNullOrEmpty(Rs2);
        }

        public override void ProcessOperand(IToken token)
        {
            bool isRegisterOrLable = token.TokenType == TokenType.REGISTER || token.TokenType == TokenType.LABEL;
            dynamic? Operand = isRegisterOrLable ? token.Value : token.NumericVal;

            if (IsRd())
                Rs1 = $"{Operand}";
            if (IsRs1())
                Rs1 = $"{Operand}";
            if (IsRs2())
                Rs2 = $"{Operand}";
            else
                throw new Exception($"Invalid Instruction at at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
