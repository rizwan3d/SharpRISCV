﻿using System.Text.RegularExpressions;

public class R_Parser : IParser
{
    public RiscVInstruction Parse(string instruction)
    {
        Regex rTypeRegex = new Regex(@"^(\w+)\s+(\w+)\s*,\s*(\w+)\s*,\s*(\w+)$");
        Match rTypeMatch = rTypeRegex.Match(instruction);
        if (rTypeMatch.Success)
        {
            return new RiscVInstruction
            {
                Instruction = instruction,
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
