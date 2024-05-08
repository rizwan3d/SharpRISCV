using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

public class I_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex iTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(\w+)\s*,\s*([-\w().%]+)$");
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

        iTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(-?[\w.%()]*)$");
        iTypeMatch = iTypeRegex.Match(instruction);
        if (iTypeMatch.Success)
        {

            var rs1 = iTypeMatch.Groups[2].Value;
            var immediate = iTypeMatch.Groups[3].Value;

            var iSourceRegex = new Regex(@"^(([\w%]*\([^)]+\))|-?\d+)\(([^)]+)\)$");//这部分后来的解码还需要再看一下（怎么解出来的负数）
            var iSourceMatch = iSourceRegex.Match(immediate);

            int i = 1;
            if (iSourceMatch.Success)
            {
                immediate = iSourceMatch.Groups[i].Value;
                HashSet<string> seenValues = new HashSet<string>();
                seenValues.Add(iSourceMatch.Groups[i].Value);
                while (seenValues.Contains(iSourceMatch.Groups[i].Value)){ i++; }

                rs1 = iSourceMatch.Groups[i].Value;
                while(string.IsNullOrEmpty(rs1) && i < iSourceMatch.Groups.Count)
                {
                    rs1 = iSourceMatch.Groups[++i].Value;
                }
            }

                return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = iTypeMatch.Groups[1].Value,
                Rd = iTypeMatch.Groups[2].Value,
                Rs1 = rs1,
                Immediate = immediate,
                Rs2 = null,
                InstructionType = InstructionType.I,
            };
        }

        if(instruction == "ecall" || instruction == "ebreak")
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
                Opcode = instruction,
                Rd = string.Empty,
                Rs1 = string.Empty,
                InstructionType = InstructionType.I,
            };
        }

        return null;
    }
}
