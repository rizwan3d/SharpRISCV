using System.Text.RegularExpressions;

class U_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex uTypeRegex = new Regex(@"^(\w+)\s+(\w+),\s+([\w().%]+)$");
        Match uTypeMatch = uTypeRegex.Match(instruction);
        if (uTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = uTypeMatch.Groups[1].Value,
                Rd = uTypeMatch.Groups[2].Value,
                Immediate = uTypeMatch.Groups[3].Value,
                InstructionType = InstructionType.U,
                Rs1 = null,
                Rs2 = null
            };
        }
        return null;
    }
}
