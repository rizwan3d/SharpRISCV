using SharpRISCV.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpRISCV.MachineCode
{
    public class MachineCode
    {
        public MachineCode(string code,string instruction)
        {
            this.Code = Convert.ToInt32(code, 2);
            this.Instruction = instruction;
            if(!instruction.Trim().EndsWith(":"))
            this.Address = SharpRISCV.Address.GetAndIncreseAddress();
        }

        private int Code { get; }

        public int Address { get; }
        public string Instruction { get; }

        public string Hex { get { return $"0x{Code.ToString("X8")}"; } }
        public int Decimal { get { return Code; }}
        public byte[] Data { get {
                string str = Code.ToString("X8");
                byte[] val = new byte[str.Length / 2];
                for (int i = 0; i < val.Length; i++)
                {
                    int index = i * 2;
                    int j = Convert.ToInt32(str.Substring(index, 2), 16);
                    val[i] = (byte)j;
                }
                return val;

            } }

        public override string ToString()
        {
            return String.Join("-", Regex.Matches(Code.ToBinary(32), @"\d{4}").Cast<Match>()); ;
        }


        public static int ProcessLoHi(string imm)
        {
            if (imm.ToLower().StartsWith("%hi"))
            {
                return GetHiPart(imm);
            }

            return GetLoPart(imm);
        }

        private static int GetHiPart(string imm)
        {
            string pattern = @"%(hi|HI|Hi|hI)\((.*?)\)";
            MatchCollection matches = Regex.Matches(imm, pattern);
            string capturedText = matches[0].Groups[2].Value;

            int hiPart = (ProcessSource(capturedText) >> 12) & 0xFFFFF;
            return hiPart;
        }

        private static int GetLoPart(string imm)
        {
            string pattern = @"%(lo|LO|Lo|lO)\((.*?)\)";
            MatchCollection matches = Regex.Matches(imm, pattern);
            string capturedText = matches[0].Groups[2].Value;
            int hiPart = (ProcessSource(capturedText) >> 12) & 0xFFFFF;
            return hiPart;
        }

        public static int ProcessSource(string Immediate)
        {

            if (Immediate.StartsWith("%"))
            {
                Immediate = ProcessLoHi(Immediate).ToString();
            }

            int value;
            if (SharpRISCV.Address.Labels.TryGetValue(Immediate, out int labelLineNumber))
            {
                value = SharpRISCV.Address.Labels[Immediate] - SharpRISCV.Address.CurrentAddress;
            }
            else if (Register.FromABI.TryGetValue(Immediate, out int register))
            {
                value = register;
            }
            else if (int.TryParse(Immediate, out value))
            {
                // Operand is a numeric value
            }
            else
            throw new Exception("Invalid Lable");

            return value;
        }
    }
}
