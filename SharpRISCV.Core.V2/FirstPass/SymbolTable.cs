using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.FirstPass.Abstraction;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed class SymbolTable : ISymbolTable
    {
        List<ISymbolInfo> symbolInfos = [];

        public ISymbolInfo this[string name]
        {
            get => symbolInfos.FirstOrDefault(lable => lable.Name == name) ?? throw new IndexOutOfRangeException();
        }

        public void Add(string name, uint address)
        {
            if (symbolInfos.FirstOrDefault( lable => lable.Name.Equals(name)) is not null)
                throw new Exception($"Label name {name} is alredy defined.");

            symbolInfos.Add(new SymbolInfo(name, address));
        }

        public int Count => symbolInfos.Count;
    }
}
