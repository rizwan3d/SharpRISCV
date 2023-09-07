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

        public int Code { get; }

        public override string ToString()
        {
            return String.Join("-", Regex.Matches(Code.ToBinary(32), @"\d{4}").Cast<Match>()); ;
        }
    }
}
