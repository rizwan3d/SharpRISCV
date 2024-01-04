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

def generate_check_lines(values):
    imm = "Rd"
    check_lines = []
    check_lines.append("           if(OperandCount(Instruction) == "+str(len(values))+")")
    check_lines.append("           {")
    for value in values:
        if value == "rd":
            check_lines.append("                IsRegister(Instruction.Rd);\n")
            imm = "Rs1"
        elif value == "rs1":
            check_lines.append("                IsRegister(Instruction.Rs1);\n")
            imm = "Rs2"
        elif value == "rs2":
            check_lines.append("                IsRegister(Instruction.Rs2);\n")
        elif value == "imm":
            check_lines.append("                if(IToken.IsLabel(Instruction."+imm+"))")
            check_lines.append("                    IsLabel(Instruction."+imm+", symbolTable);")
            check_lines.append("                else")
            check_lines.append("                    IsImm(Instruction."+imm+");")
    check_lines.append("           } else")
    check_lines.append("            __________________________")
    return check_lines

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
                variations = parts[1].strip().split('|')
                
                for variation in variations:
                    values = variation.split(',')
                    check_lines = generate_check_lines(values)
                    new_class = new_class.replace("            __________________________","\n".join(check_lines));
            
            new_class = new_class.replace("            __________________________","                throw new Exception($\"Invalid Instruction Operand Count at Line Number: {Instruction.Token.LineNumber}, Char: {Instruction.Token.StartIndex}.\");");
            print("Analyzer created successfully for {}".format(line))
            
            with open(new_file_path, 'w') as new_file:
                new_file.write(new_class)

print("{} Analyzers created successfully in the '{}' directory.".format(i,output_directory))

