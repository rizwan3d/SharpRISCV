using SharpRISCV.Elf;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        string[] assemblyLines =
        {
            "sub x3, x1, x2",
            "xor x3, x1, x2",
            "add x3, x1, x2",
            "addi x1, x2, 8",
            "sb x2, 3(x7)",
            "beq  x3, x4, 0x535",
            "bne  x1, x2, 0x458",
            "auipc x2, 0x8000",
            "lui x1, 0x42",
            "jal x2, 0x454",
        };

        foreach (var assemblyLine in assemblyLines)
        {
            var instruction = RiscVAssembler.IdentifyInstructionType(assemblyLine);
            if (instruction == InstructionType.EmptyLine) continue;
            var riscVInstruction = RiscVAssembler.InstructionParser(assemblyLine, instruction);
            var machineCode = riscVInstruction.MachineCode();
            Console.WriteLine($"{machineCode} {machineCode.Hex} # {assemblyLine}");
        }

        Console.ReadLine();
    }
}