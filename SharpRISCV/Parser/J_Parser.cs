using System.Text.RegularExpressions;

class J_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex jTypeRegex = new Regex(@"^(\w+)\s+(\w+),\s+([\dA-Za-z]+)$");
        Match jTypeMatch = jTypeRegex.Match(instruction);
        if (jTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = jTypeMatch.Groups[1].Value,
                Label =  null,
                Rd = jTypeMatch.Groups[2].Value,
                Rs1 = null,
                Rs2 = null,
                Immediate = jTypeMatch.Groups[3].Value,
                InstructionType = InstructionType.J
            };
        }
        return null;
    }
}
