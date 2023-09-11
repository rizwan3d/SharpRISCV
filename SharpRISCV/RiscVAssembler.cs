using System.Text.RegularExpressions;

class RiscVAssembler
{

    public static InstructionType IdentifyInstructionType(string instruction)
    {
        instruction = instruction.Trim();
        if (string.IsNullOrEmpty(instruction))
            return InstructionType.EmptyLine;
        string op = instruction.Split(' ')[0];
        OpCode opCode = op.ToEnum<OpCode>();
        switch (opCode)
        {
            case (OpCode)0b0110011:
                return InstructionType.R;
            case (OpCode)0b0010111:
                return InstructionType.U;
            case (OpCode)0b0110111:
                return InstructionType.U;
            case (OpCode)0b0010011:
                return InstructionType.I;
            case (OpCode)0b1100011:
                return InstructionType.B;
            case (OpCode)0b0000011:
                return InstructionType.I;
            case (OpCode)0b0100011:
                return InstructionType.S;
            case (OpCode)0b1101111:
                return InstructionType.J;
            default:
                return InstructionType.UnKnown;
        }
    }

    public static RiscVInstruction InstructionParser(string instruction, InstructionType type)
    {
        switch (type)
        {
            case InstructionType.R:
                return (new R_Parser()).Parse(instruction);
            case InstructionType.I:
                return (new I_Parser()).Parse(instruction);
            case InstructionType.S:
                return (new S_Parser()).Parse(instruction);
            case InstructionType.B:
                return (new B_Parser()).Parse(instruction);
            case InstructionType.U:
                return (new U_Parser()).Parse(instruction);
            case InstructionType.J:
                return (new J_Parser()).Parse(instruction);
            default:
                return null;
        }
    }
}
