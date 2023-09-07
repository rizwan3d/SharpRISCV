using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpRISCV.MachineCode
{
    public class MachineCode
    {
        public MachineCode(string code)
        {
            this.Code = Convert.ToInt32(code, 2);
        }

        private int Code { get; }

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
    }
}
