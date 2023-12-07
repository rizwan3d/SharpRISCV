using SharpRISCV.Core.V2.FirstPass.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed record SymbolInfo(string name, uint address) : ISymbolInfo
    {
        public string Name { get; } = name;
        public uint Address { get; } = address;
    }
}
