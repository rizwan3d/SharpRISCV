using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.Registers
{
    public static class Register
    {
        public static Dictionary<string, int> FromABI = new Dictionary<string, int>();

        static Register()
        {
            FromABI.Add("Zero", 0);
            FromABI.Add("ra", 1);
            FromABI.Add("sp", 2);
            FromABI.Add("gp", 3);
            FromABI.Add("tp", 4);
            FromABI.Add("t0", 5);
            FromABI.Add("t1", 6);
            FromABI.Add("t2", 7);
            FromABI.Add("s0", 8);
            FromABI.Add("fp", 8);
            FromABI.Add("s1", 9);
            FromABI.Add("a0", 10);
            FromABI.Add("a1", 11);
            FromABI.Add("a2", 12);
            FromABI.Add("a3", 13);
            FromABI.Add("a4", 14);
            FromABI.Add("a5", 15);
            FromABI.Add("a6", 16);
            FromABI.Add("a7", 17);
            FromABI.Add("s2", 18);
            FromABI.Add("s3", 19);
            FromABI.Add("s4", 20);
            FromABI.Add("s5", 21);
            FromABI.Add("s6", 22);
            FromABI.Add("s7", 23);
            FromABI.Add("s8", 24);
            FromABI.Add("s9", 25);
            FromABI.Add("s10", 26);
            FromABI.Add("s11", 27);
            FromABI.Add("t3", 28);
            FromABI.Add("t4", 29);
            FromABI.Add("t5", 30);
            FromABI.Add("t6", 31);
            FromABI.Add("x0", 0);
            FromABI.Add("x1", 1);
            FromABI.Add("x2", 2);
            FromABI.Add("x3", 3);
            FromABI.Add("x4", 4);
            FromABI.Add("x5", 5);
            FromABI.Add("x6", 6);
            FromABI.Add("x7", 7);
            FromABI.Add("x8", 8);
            FromABI.Add("x9", 9);
            FromABI.Add("x10", 10);
            FromABI.Add("x11", 11);
            FromABI.Add("x12", 12);
            FromABI.Add("x13", 13);
            FromABI.Add("x14", 14);
            FromABI.Add("x15", 15);
            FromABI.Add("x16", 16);
            FromABI.Add("x17", 17);
            FromABI.Add("x18", 18);
            FromABI.Add("x19", 19);
            FromABI.Add("x20", 20);
            FromABI.Add("x21", 21);
            FromABI.Add("x22", 22);
            FromABI.Add("x23", 23);
            FromABI.Add("x24", 24);
            FromABI.Add("x25", 25);
            FromABI.Add("x26", 26);
            FromABI.Add("x27", 27);
            FromABI.Add("x28", 28);
            FromABI.Add("x29", 29);
            FromABI.Add("x30", 30);
            FromABI.Add("x31", 31);
        }
    }
}
