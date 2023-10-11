using System.Text.RegularExpressions;

public class B_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex bTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(\w+)\s*,\s*(\w+)$");
        Match bTypeMatch = bTypeRegex.Match(instruction);
        if (bTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = bTypeMatch.Groups[1].Value,
                Rs1 = bTypeMatch.Groups[2].Value,
                Rs2 = bTypeMatch.Groups[3].Value,
                Label = bTypeMatch.Groups[4].Value,
                Rd = null,
                Immediate = null,
                InstructionType = InstructionType.B
            };
        }
        return null;
    }
}
