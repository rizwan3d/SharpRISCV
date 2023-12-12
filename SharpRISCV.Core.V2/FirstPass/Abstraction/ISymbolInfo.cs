namespace SharpRISCV.Core.V2.FirstPass.Abstraction
{
    public interface ISymbolInfo
    {
        uint Address { get; }
        string Name { get; }
    }
}