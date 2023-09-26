using SharpRISCV.Core.MachineCode;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class RiscVInstruction
{
    private string _Opcode { get; set; }

    public InstructionType InstructionType { get; set; }

    public string Opcode
    {
        get
        {
            return _Opcode.ToLower();
        }
        set
        {
            _Opcode = value;
        }
    }
    public OpCode OpcodeBin
    {
        get
        {
            OpCode op = Opcode.ToEnum<OpCode>();
            return op;
        }
    }

    public Funct3 Funct3
    {
        get
        {
            Funct3 op = Opcode.ToEnum<Funct3>();
            return op;
        }
    }

    public Funct7 Funct7
    {
        get
        {
            Funct7 op = Opcode.ToEnum<Funct7>();
            return op;
        }
    }
    public string Rd { get; set; }
    public string Rs1 { get; set; }
    public string Rs2 { get; set; }
    public string Immediate { get; set; }
    public string Label { get; internal set; }
    public string Instruction { get; internal set; }

    public List<MachineCode> MachineCode()
    {

        switch (InstructionType)
        {
            case InstructionType.R:
                return new List<MachineCode> { new R_MachineCode().Generate(this)};
            case InstructionType.I:
                return new I_MachineCode().Generate(this);
            case InstructionType.S:
                return new List<MachineCode> { new S_MachineCode().Generate(this) };
            case InstructionType.B:
                return new List<MachineCode> { new B_MachineCode().Generate(this) };
            case InstructionType.U:
                return new List<MachineCode> { new U_MachineCode().Generate(this) };
            case InstructionType.J:
                return new List<MachineCode> { new J_MachineCode().Generate(this) };
            default:
                return null;
        }
    }
}
