using System;
using System.Linq;

namespace SharpRISCV.Core.V2.SemanticAnalysis.Abstraction
{
    public interface IAnalyzerResolver
    {
        Type FindAnalyzer(string Mnemonic);
    }
}
