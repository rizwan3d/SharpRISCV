using System.Text.RegularExpressions;

class I_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex iTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(\w+)\s*,\s*(-*[0-9]*)$");
        Match iTypeMatch = iTypeRegex.Match(instruction);
        if (iTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = iTypeMatch.Groups[1].Value,
                Rd = iTypeMatch.Groups[2].Value,
                Rs1 = iTypeMatch.Groups[3].Value,
                Immediate = iTypeMatch.Groups[4].Value,
                Rs2 = null,
                InstructionType = InstructionType.I,
            };
        }

        iTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(-*[0-9A-Za-z]*)$");
        iTypeMatch = iTypeRegex.Match(instruction);
        if (iTypeMatch.Success)
        {            
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = iTypeMatch.Groups[1].Value,
                Rd = iTypeMatch.Groups[2].Value,
                Rs1 = "x0",
                Immediate = iTypeMatch.Groups[3].Value,
                Rs2 = null,
                InstructionType = InstructionType.I,
            };
        }
        return null;
    }
}
