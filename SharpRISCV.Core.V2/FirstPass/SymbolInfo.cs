using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.FirstPass.Abstraction;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed record SymbolInfo(string name, uint address) : ISymbolInfo
    {
        public string Name { get; } = name;
        public uint Address { get; } = address;
    }
}
