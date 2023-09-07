using System.Text.RegularExpressions;

class R_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex rTypeRegex = new Regex(@"^(\w+)\s+(\w+),\s+(\w+),\s+(\w+)$");
        Match rTypeMatch = rTypeRegex.Match(instruction);
        if (rTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Opcode = rTypeMatch.Groups[1].Value,
                Rd = rTypeMatch.Groups[2].Value,
                Rs1 = rTypeMatch.Groups[3].Value,
                Rs2 = rTypeMatch.Groups[4].Value,
                Immediate = null,
                InstructionType = InstructionType.R
            };
        }
        return null;
    }
}
