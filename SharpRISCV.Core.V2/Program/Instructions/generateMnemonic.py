import os

file_path = '../../InstructionSet.txt'

new_file_path = os.path.join("./", "Mnemonic.cs")

csharp_class = """namespace SharpRISCV.Core.V2.Program.Instructions
{
    public enum Mnemonic
    {
    __________________________
    }
}"""

unique_names = set()

with open(file_path, 'r') as file:
    i = 0
    for line in file:
        parts = line.strip().split()
        if len(parts) >= 1:
            name = parts[0].replace(".", "_").upper()
            if name not in unique_names:
                unique_names.add(name)
                i = i + 1
                csharp_class = csharp_class.replace("__________________________","    " + name +",\n    __________________________")

    csharp_class = csharp_class.replace("    __________________________","");
    with open(new_file_path, 'w') as new_file:
        new_file.write(csharp_class)

print("{} Mnemonic created successfully.".format(i))