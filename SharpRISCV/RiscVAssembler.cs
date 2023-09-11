using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace SharpRISCV
{
    class RiscVAssembler
    {
        private static List<MachineCode.MachineCode> mc = new List<MachineCode.MachineCode>();

        public static List<MachineCode.MachineCode> MachineCode { get { return mc; } }


        public static void Assamble(string[] code)
        {
            ProcessLables(code);
            Address.Reset();
            foreach (var assemblyLine in code)
            {
                var instruction = IdentifyInstructionType(assemblyLine);
                if (instruction == InstructionType.EmptyLine ||
                    instruction == InstructionType.Lable) continue;
                var riscVInstruction = InstructionParser(assemblyLine, instruction);
                mc.Add(riscVInstruction.MachineCode());

            }
        }

        public static void ProcessLables(string[] code)
        {
            foreach (var assemblyLine in code)
            {
                if (assemblyLine.Trim().EndsWith(":"))
                {
                    string label = assemblyLine.Substring(0, assemblyLine.Length - 1);
                    Address.Labels.Add(label, Address.CurrentAddress);
                    continue;
                }
                Address.GetAndIncreseAddress();
            }
        }

        public static InstructionType IdentifyInstructionType(string instruction)
        {
            instruction = instruction.Trim();
            if (string.IsNullOrEmpty(instruction))
                return InstructionType.EmptyLine;

            if (instruction.EndsWith(":"))
                return InstructionType.Lable;

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
}
