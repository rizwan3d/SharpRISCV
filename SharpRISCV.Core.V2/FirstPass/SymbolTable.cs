using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.FirstPass
{
    public sealed class SymbolTable : ISymbolTable
    {
        List<ISymbolInfo> symbolInfos = [];

        public ISymbolInfo this[string name]
        {
            get => symbolInfos.FirstOrDefault(lable => lable.Name.Equals(name)) ?? throw new IndexOutOfRangeException();
        }

        public void Add(IToken token,uint address)
        {
            if (symbolInfos.FirstOrDefault(lable => lable.Name.Equals(token.Value)) is not null)
                throw new Exception($"Label name {token.Value} is already defined at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");

            symbolInfos.Add(new SymbolInfo(token.Value.Replace(":", string.Empty), address));
        }

        public int Count => symbolInfos.Count;
    }
}
