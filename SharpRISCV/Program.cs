using SharpRISCV;

class Program
{
    static void Main()
    {
        string[] assemblyLines =
        {
            "sub x3, x1, x2",
            "xor x3, x1, x2",
            "doit:",
            "add x3, x1, x2",
            "addi x1, x2, 8",
            "sb x2, 3(x7)",
            "beq  x3, x4, doit",
            "bne  x1, x2, doit2",
            "add x3, x1, x2",
            "add x3, x1, x2",
            "doit2:",
            "add x3, x1, x2",
        //    "auipc x2, 0x8000",
        //    "lui x1, 0x42",
            "jal x2, doit",
        };

        RiscVAssembler.Assamble(assemblyLines);
        foreach (var machineCode in RiscVAssembler.MachineCode)
        {
            Console.WriteLine($"0x{machineCode.Address:X8} {machineCode.Hex} # {machineCode.Instruction}");
        }
    }
}