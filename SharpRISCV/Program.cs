using SharpRISCV;
using System.Reflection.Emit;
using System.Reflection;
using SharpRISCV.Windows;

partial class Program
{
    static void Main(string[] args)
    {

        string inputFile = null;
        OutputType outputType = OutputType.BIN;
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
            else if (args[i] == "-p" && i + 1 < args.Length)
            {
                string type = args[i + 1];
                switch (type.ToLower())
                {
                    case "pe":
                        outputType = OutputType.PE;
                        break;
                    case "bin":
                        outputType = OutputType.BIN;
                        break;
                    case "elf":
                        outputType = OutputType.ELF;
                        break;
                    case "hex":
                        outputType = OutputType.HEX;
                        break;
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
            if (outputFile == "console")
            {
                string assemblyLines = File.ReadAllText(inputFile);
                RiscVAssembler.Assamble(assemblyLines);
                Console.WriteLine($"-------------------------------------------------------------------------");
                Console.WriteLine($" Entry Address: {Address.EntryPointHax}");
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
                Console.WriteLine($" Data Dump");
                Console.WriteLine($"-------------------------------------------------------------------------");
                foreach (var dump in DataSection.HexDum)
                {
                    Console.WriteLine($" 0x{dump.Key}\t|\t0x{dump.Value}\t|\t{dump.Value.HexToString().Reverse()}");
                }
                Console.WriteLine($"-------------------------------------------------------------------------");
            }

            if(outputType == OutputType.PE)
            
            {
                if (!outputFile.ToLower().EndsWith(".exe"))  outputFile += ".exe";
                Console.WriteLine("Build Started");
                Address.SetAddress((int)Compile.memAddress);
                string assemblyLines = File.ReadAllText(inputFile);
                RiscVAssembler.Assamble(assemblyLines);
                new Compile(outputFile).BinaryWrite();
                Console.WriteLine("Build Completed");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }   
}