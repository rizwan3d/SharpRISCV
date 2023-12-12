using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public class SemanticAnalysis(List<ISection> sections, ISymbolTable symbolTable)
    {
        public void Perform()
        {
            foreach (ISection section in sections.Where( s => s is ITextSection))
            {
                foreach (IInstruction ins in section.Instructions)
                {
                    string name = nameof(ins.Mnemonic).ToUpperFirstLetter();
                    Type? type = Type.GetType($"SharpRISCV.Core.V2.SemanticAnalysis.Analyzers.{name}Analyzer");
                    try
                    {
                        if (type is null)
                            throw new Exception();

                        IAnalyzer? Analyzer = Activator.CreateInstance(type) as IAnalyzer;

                        if (Analyzer is null)
                            throw new Exception();

                        Analyzer.Analyze(ins, symbolTable);
                    }
                    catch 
                    {
                        throw new Exception($"invlid Instruction at Line Number: {ins.Token.LineNumber}, Char: {ins.Token.StartIndex}.");
                    }
                }
            }
        }
    }
}
