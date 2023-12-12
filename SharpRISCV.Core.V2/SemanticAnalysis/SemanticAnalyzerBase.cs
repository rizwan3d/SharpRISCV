using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public abstract class SemanticAnalyzerBase(IAnalyzerResolver analyzerResolver) : ISemanticAnalyzer
    {
        protected IAnalyzerResolver AnalyzerResolver { get; } = analyzerResolver;
        public abstract void Perform();
    }
}
