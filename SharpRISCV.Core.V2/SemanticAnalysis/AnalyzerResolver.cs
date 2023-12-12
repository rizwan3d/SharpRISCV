using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public sealed class AnalyzerResolver : IAnalyzerResolver
    {
        public Type FindAnalyzer(string Mnemonic)
        {
            Type? type = Type.GetType($"SharpRISCV.Core.V2.SemanticAnalysis.Analyzers.{Mnemonic}Analyzer");
            return type ?? throw new Exception();
        }
    }
}
