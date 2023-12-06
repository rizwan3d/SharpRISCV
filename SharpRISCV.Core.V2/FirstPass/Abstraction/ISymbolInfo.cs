namespace SharpRISCV.Core.V2.FirstPass
{
    public interface ISymbolInfo
    {
        uint Address { get; }
        string Name { get; }
    }
}