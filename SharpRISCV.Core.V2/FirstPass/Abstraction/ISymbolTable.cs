namespace SharpRISCV.Core.V2.FirstPass.Abstraction
{
    public interface ISymbolTable
    {
        ISymbolInfo this[string name] { get; }

        int Count { get; }

        void Add(string name, uint address);
    }
}