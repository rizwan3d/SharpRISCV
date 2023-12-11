using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public class InstructionFactory
    {
        public static IInstruction CreateInstruction(IToken token)
        {
            return new Instruction(token);
        }
    }
}
