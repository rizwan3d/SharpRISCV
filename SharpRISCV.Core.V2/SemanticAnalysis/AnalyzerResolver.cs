using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
