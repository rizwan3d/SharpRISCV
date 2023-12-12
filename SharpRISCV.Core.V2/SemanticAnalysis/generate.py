import os

file_path = '../InstructionSet.txt'

output_directory = './Analyzers/'
os.makedirs(output_directory, exist_ok=True)

csharp_class = """using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;

namespace SharpRISCV.Core.V2.SemanticAnalysis.Analyzers
{
    public class CLASSNAME : Analyzer, IAnalyzer
    {
        public override void Analyze(IInstruction Instruction, ISymbolTable symbolTable)
        {
            __________________________
        }
    }
}
"""

with open(file_path, 'r') as file:
    i = 0
    for line in file:
        parts = line.strip().split()
        i = i + 1
        if len(parts) >= 1:
            new_file_name = parts[0].replace(".", "_")
            new_file_name = new_file_name.capitalize() + 'Analyzer'
            new_file_path = os.path.join(output_directory, new_file_name)  + ".cs"

            new_class = csharp_class.replace("CLASSNAME", new_file_name)

            if len(parts) > 1:
                values = parts[1].split(',')
                imm = "Rd"
                for value in values:
                    if(value == "rd"):
                        check = "            IsRegister(Instruction.Rd);\n            __________________________"
                        imm = "Rs1"
                    if(value == "rs1"):
                        check = "            IsRegister(Instruction.Rs1);\n            __________________________"
                        imm = "Rs2"
                    if(value == "rs2"):
                        check = "            IsRegister(Instruction.Rs2);\n            __________________________"
                    if(value == "imm"):
                        check = "            if(IToken.IsLabel(Instruction."+imm+"))\n                IsLabel(Instruction."+imm+", symbolTable);\n            else\n                IsImm(Instruction."+imm+");\n            __________________________"
                    new_class = new_class.replace("            __________________________",check);
            
            new_class = new_class.replace("            __________________________","");
            
            print("Analyzer created successfully for {}".format(line))
            
            with open(new_file_path, 'w') as new_file:
                new_file.write(new_class)

print("{} Analyzers created successfully in the '{}' directory.".format(i,output_directory))