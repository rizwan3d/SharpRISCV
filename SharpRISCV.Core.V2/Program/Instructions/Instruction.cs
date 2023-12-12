using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public sealed class Instruction(IToken token) : InstructionBase(token), IInstruction
    {
        public override bool IsComplete()
        {
            return !string.IsNullOrEmpty(Token?.Value);
        }

        public override bool IsRd()
        {
            return string.IsNullOrEmpty(Rd?.Value);
        }

        public override bool IsRs1()
        {
            return string.IsNullOrEmpty(Rs1?.Value);
        }

        public override bool IsRs2()
        {
            return string.IsNullOrEmpty(Rs2?.Value);
        }

        public override void ProcessOperand(IToken token)
        {
            if (IsRd())
                Rd = token;
            else if (IsRs1())
                Rs1 = token;
            else if (IsRs2())
                Rs2 = token;
            else
                throw new Exception($"Invalid Instruction at at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }

        protected override void IdentifyInstructionType()
        {
            if(IToken.IsLable(Token))
                InstructionType = InstructionType.Lable;

            switch (Opcode)
            {
                case (OpCode)0b0110011:
                    InstructionType = InstructionType.R;
                    break;
                case (OpCode)0b0010111:
                case (OpCode)0b0110111:
                    InstructionType = InstructionType.U;
                    break;
                case (OpCode)0b0010011:
                case (OpCode)0b1110011:
                case (OpCode)0b0000011:
                    InstructionType = InstructionType.I;
                    break;
                case (OpCode)0b1100011:
                    InstructionType = InstructionType.B;
                    break;
                case (OpCode)0b0100011:
                    InstructionType = InstructionType.S;
                    break;
                case (OpCode)0b1101111:
                    InstructionType = InstructionType.J;
                    break;
                default:
                    InstructionType = InstructionType.UnKnown;
                    break;
            }
        }
    }
}
