using System.Text.RegularExpressions;

public class Lable_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        return new RiscVInstruction
        {
            Instruction = instruction,
            InstructionType = InstructionType.Lable
        };
    }
}

