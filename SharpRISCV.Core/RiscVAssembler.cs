using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpRISCV.Core
{
    public class RiscVAssembler
    {
        private static List<RiscVInstruction> instruction = [];

        public static List<RiscVInstruction> Instruction { get { return instruction; } }

        public static Dictionary<string, List<string>> DirectiveCode = [];

        private static List<string> sectionsDirective = [".data", ".text", ".bss"];

        private static string currentDirective = "";

        public static void Assamble(string code)
        {
            cleanAll();

            code = RemoveComments(code);
            code = TrimLines(code);
            code = Regex.Replace(code, ":\\s*\\.", $":{Environment.NewLine}.", RegexOptions.Multiline);
            BuildDirective(code);

            //Process .Text Lables
            if (DirectiveCode.ContainsKey(".text"))
                foreach (var directive in DirectiveCode[".text"])
                {
                    var lines = directive.SplitStingByNewLine();
                    ProcessLables(lines);
                }

            var x = Address.Labels;

            // For testing Linker Script script RAM (rwx) : ORIGIN = 0x00010000, LENGTH = 0x08000
            //Address.SetAddress(65536);

            //Process .Data Lables
            if (DirectiveCode.ContainsKey(".data"))
            {
                Address.StartDataAddress();
                foreach (var directive in DirectiveCode[".data"])
                {
                    var lines = directive.SplitStingByNewLine();
                    ProcessDataLables(lines);
                }
            }

            if (DirectiveCode.ContainsKey(".bss"))
            {
                Address.StartBssAddress();
                foreach (var directive in DirectiveCode[".bss"])
                {
                    var lines = directive.SplitStingByNewLine();
                    ProcessBssLables(lines);
                }
            }

            Address.Reset();

            //Process .Text Instructions
            if (DirectiveCode.ContainsKey(".text"))
            foreach (var directive in DirectiveCode[".text"])
                    {
                var lines = directive.SplitStingByNewLine();
                foreach (var line in lines)
                {
                    var instructionType = IdentifyInstructionType(line);
                    if (instructionType == InstructionType.EmptyLine) continue;
                    var riscVInstruction = InstructionParser(line.Trim(), instructionType);
                    instruction.Add(riscVInstruction);
                }
            }
        }

        private static void cleanAll()
        {
            instruction.Clear();
            DirectiveCode.Clear();
            Address.Clear();
            DataSection.Clear();
        }

        private static RiscVInstruction? DirectiveParser(string v)
        {
            if (v.EndsWith(":"))
                return new Lable_Parser().Parse(v);

            string directive = v.Substring(0, v.IndexOf(' ')).Trim();

            switch (directive)
            {
                case ".string":
                case ".asciz":
                    return null;
                default:
                    return null;
            }
        }

        public static List<MachineCode.MachineCode> MachineCode()
        {
            List<MachineCode.MachineCode> mc = [];

            foreach (var instruction in Instruction)
            {
                mc.AddRange(instruction.MachineCode());
            }

            return mc;
        }
        static void BuildDirective(string code)
        {
            string[] lines = code.SplitStingByNewLine();


            StringBuilder directiveCode = new();

            foreach (var line in lines)
            {
                if (line.StartsWith(".") && sectionsDirective.Any(x => x == line.ToLower()))
                {
                    AddToRespativeDirective(directiveCode.ToString());
                    directiveCode.Clear();
                    currentDirective = line.ToLower();
                    continue;
                }

                directiveCode.AppendLine(line);
            }

            if (directiveCode.Length > 0)
                AddToRespativeDirective(directiveCode.ToString());
        }

        static void AddToRespativeDirective(string directiveCode)
        {
            if (!string.IsNullOrEmpty(currentDirective))
            {
                if (DirectiveCode.ContainsKey(currentDirective))
                    DirectiveCode[currentDirective].Add(directiveCode.ToString());
                else
                    DirectiveCode[currentDirective] = [ directiveCode.ToString() ];
            }
        }

        static string RemoveComments(string lines)
        {
            // Regular expression to match comments starting with '#' and everything after
            string pattern = @"#.*$";
            lines = Regex.Replace(lines, pattern, string.Empty, RegexOptions.Multiline);
            return lines;
        }

        static string TrimLines(string lines)
        {
            lines = string.Join("\n", lines.Split("\n\r",StringSplitOptions.RemoveEmptyEntries));
            lines = string.Join("\n", lines.Split("\n",StringSplitOptions.RemoveEmptyEntries));
            string pattern = @"^\s+|\s+$";
            lines = Regex.Replace(lines, pattern, string.Empty, RegexOptions.Multiline | RegexOptions.Singleline);
            return lines;
        }

        public static void ProcessLables(string[] code)
        {
            foreach (var assemblyLine in code)
            {
                var line = assemblyLine.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith(".")) continue;
                if (line.EndsWith(":"))
                {
                    string label = line.Substring(0, line.Length - 1);
                    Address.Labels.Add(label, Address.CurrentAddress);
                    continue;
                }
                Address.GetAndIncreseAddress();
                if (line.StartsWith("la")) Address.GetAndIncreseAddress();
            }
        }

        public static void ProcessDataLables(string[] code)
        {
            foreach (var assemblyLine in code)
            {
                var line = assemblyLine.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith(".string") || line.StartsWith(".asciz"))
                {
                    string pattern = @"\"".*\""";
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        string extractedString = match.Groups[0].Value;
                        string data = extractedString.Substring(1, extractedString.Length - 2);
                        data = data.Replace("\\n", "\n");
                        byte[] b = new UTF8Encoding(true).GetBytes(data);
                        DataSection.Add(b);
                        DataSection.Add([ 0 ]);
                        Address.GetAndIncreseAddress(b.Length + 1);
                        continue;
                    }
                    else
                        throw new ("Invaild use of .string");
                };
                if (line.StartsWith(".word"))
                {
                    string pattern = @"\.word\s+([\w,\s]+)";
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        string extractedString = match.Groups[1].Value;

                        foreach (var item in extractedString.Split(','))
                        {
                            int inval = 0;
                            if(item.StartsWith("0x"))
                            {
                                inval = Convert.ToInt32(item,16);
                            }
                            else if (item.StartsWith("0b"))
                            {
                                inval = Convert.ToInt32(item, 2);
                            }
                            else if (int.TryParse(item.Trim(), out int val))
                            {
                                inval = val;
                            }

                            byte[] byteArray = BitConverter.GetBytes(inval);
                            if (byteArray.Length < 4)
                            {
                                byte[] paddedArray = new byte[4];
                                Array.Copy(byteArray, paddedArray, byteArray.Length); // Copy the original bytes
                                byteArray = paddedArray; // Replace byteArray with the padded array
                            }
                            DataSection.Add(byteArray);
                            Address.GetAndIncreseAddress(byteArray.Length + 1);
                        }
                        continue;
                    }
                    else
                        throw new ("Invaild use of .string");
                };
                if (line.StartsWith(".half"))
                {
                    string pattern = @"\.half\s+([\w,\s]+)";
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        string extractedString = match.Groups[1].Value;

                        foreach (var item in extractedString.Split(','))
                        {
                            short inval = 0;
                            if (item.StartsWith("0x"))
                            {
                                inval = Convert.ToInt16(item, 16);
                            }
                            else if (item.StartsWith("0b"))
                            {
                                inval = Convert.ToInt16(item, 2);
                            }
                            else if (int.TryParse(item.Trim(), out int val))
                            {
                                inval = Convert.ToInt16(val);
                            }

                            byte[] byteArray = BitConverter.GetBytes(inval);
                            if (byteArray.Length < 2)
                            {
                                byte[] paddedArray = new byte[2];
                                Array.Copy(byteArray, paddedArray, byteArray.Length); // Copy the original bytes
                                byteArray = paddedArray; // Replace byteArray with the padded array
                            }
                            DataSection.Add(byteArray);
                            Address.GetAndIncreseAddress(byteArray.Length + 1);
                        }
                        continue;
                    }
                    else
                        throw new("Invaild use of .string");
                };
                if (line.EndsWith(":"))
                {
                    string label = line.Substring(0, line.Length - 1);
                    Address.Labels.Add(label, Address.CurrentAddress);
                    continue;
                }
                Address.GetAndIncreseAddress();
            }
        }
        public static void ProcessBssLables(string[] code)
        {
            foreach (var assemblyLine in code)
            {
                var line = assemblyLine.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith(".space"))
                {
                    string pattern = @"[0-9]+";
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        string size = match.Groups[0].Value;
                        if(int.TryParse(size, out int value))
                        {
                            DataSection.Add(new byte[value]);
                            Address.GetAndIncreseAddress(value);
                        }
                        continue;
                    }
                    else
                        throw new Exception("Invaild use of .space");
                };
                if (line.EndsWith(":"))
                {
                    string label = line.Substring(0, line.Length - 1);
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
                case (OpCode)0b1110011:
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

        public static RiscVInstruction? InstructionParser(string instruction, InstructionType type)
        {
            switch (type)
            {
                case InstructionType.R:
                    return new R_Parser().Parse(instruction);
                case InstructionType.I:
                    return new I_Parser().Parse(instruction);
                case InstructionType.S:
                    return new S_Parser().Parse(instruction);
                case InstructionType.B:
                    return new B_Parser().Parse(instruction);
                case InstructionType.U:
                    return new U_Parser().Parse(instruction);
                case InstructionType.J:
                    return new J_Parser().Parse(instruction);
                case InstructionType.Lable:
                    return new Lable_Parser().Parse(instruction);
                default:
                    return null;
            }
        }
    }
}
