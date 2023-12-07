using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.FirstPass.Abstraction
{
    public interface ISymbolTable
    {
        ISymbolInfo this[string name] { get; }

        int Count { get; }

        void Add(IToken token, uint address);
    }
}