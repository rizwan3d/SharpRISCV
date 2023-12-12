using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public abstract class SemanticAnalyzerBase(IAnalyzerResolver analyzerResolver) : ISemanticAnalyzer
    {
        protected IAnalyzerResolver AnalyzerResolver { get; } = analyzerResolver;
        public abstract void Perform();
    }
}
