using System.Text.RegularExpressions;

class Lable_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        return new RiscVInstruction
        {
            Instruction = instruction,
            InstructionType = InstructionType.Lable
        };
        return null;
    }
}

