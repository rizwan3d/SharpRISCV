using SharpRISCV;

partial class Program
{
    static void Main(string[] args)
    {

        string inputFile = null;
        string outputFile = "console";
        RegisterSize? registerSize = RegisterSize.R32;

        // Parse command-line arguments
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-i" && i + 1 < args.Length)
            {
                inputFile = args[i + 1];
                i++;
            }
            else if (args[i] == "-o" && i + 1 < args.Length)
            {
                outputFile = args[i + 1];
                i++;
            }
            else if (args[i] == "-r" && i + 1 < args.Length)
            {
                if (int.TryParse(args[i + 1], out int aValue) && (aValue == 32 || aValue == 64 || aValue == 128))
                {
                    registerSize = (RegisterSize)aValue;
                    i++;
                }
                else
                {
                    Console.WriteLine("Invalid value for -r. Please use 32, 64, or 128. (Default:32)");
                    return;
                }
            }
        }

        // Check if required arguments are provided
        if (inputFile == null)
        {
            Console.WriteLine("Usage: -i [input file] -o [output file](Default:console) -r [32|64|128](Default:32)");
            return;
        }

        try
        {
            string assemblyLines = File.ReadAllText(inputFile);
            RiscVAssembler.Assamble(assemblyLines);

            if (outputFile == "console")
            {
                Console.WriteLine($"-------------------------------------------------------------------------");
                Console.WriteLine($" Address \t|\tHex Code\t|\tInstruction");
                Console.WriteLine($"-------------------------------------------------------------------------");

                foreach (var instruction in RiscVAssembler.Instruction)
                {
                    if (instruction.InstructionType == InstructionType.Lable)
                    {
                        Console.WriteLine($"-------------------------------------------------------------------------");
                        Console.WriteLine($" \t{instruction.Instruction}");
                        continue;
                    }
                    instruction.MachineCode().ForEach( machineCode => 
                    Console.WriteLine($" 0x{machineCode.Address:X8}\t|\t{machineCode.Hex}\t|\t{instruction.Instruction}"));
                }
                Console.WriteLine($"-------------------------------------------------------------------------");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }   
}