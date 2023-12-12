using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public class SemanticAnalyzer(List<ISection> sections, ISymbolTable symbolTable)
: ISemanticAnalyzer
    {
        public void Perform()
        {
            foreach (ISection section in sections.Where(s => s is ITextSection))
            {
                foreach (IInstruction ins in section.Instructions)
                {
                    string name = ins.Mnemonic.ToString().ToLower().ToUpperFirstLetter();
                    IAnalyzer? Analyzer;

                    Type? type = Type.GetType($"SharpRISCV.Core.V2.SemanticAnalysis.Analyzers.{name}Analyzer");
                    try
                    {
                        if (type is null)
                            throw new Exception();

                        Analyzer = Activator.CreateInstance(type) as IAnalyzer;
                    }
                    catch
                    {
                        throw new Exception($"invlid Instruction at Line Number: {ins.Token.LineNumber}, Char: {ins.Token.StartIndex}.");
                    }

                    if (Analyzer is null)
                        throw new Exception($"invlid Instruction at Line Number: {ins.Token.LineNumber}, Char: {ins.Token.StartIndex}.");

                    Analyzer.Analyze(ins, symbolTable);
                }
            }
        }
    }
}
