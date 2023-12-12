using Moq;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.SemanticAnalysis
{
    [TestClass]
    public class SemanticAnalysisTests
    {
        [TestMethod]
        public void Perform_AnalyzesInstructions_CallsAnalyzerForEachInstruction()
        {
            var mockTextSection = new TextSection();
            IInstruction mockInstruction = new Instruction(new Token(TokenType.REGISTER, "ADD", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x1", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x2", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x3", 0, 0, 0));
            mockTextSection.Instructions.Add(mockInstruction);

            var mockSymbolTable = new Mock<ISymbolTable>();
            var mockIAnalyzerResolver = new Mock<IAnalyzerResolver>();

            mockIAnalyzerResolver.Setup(x => x.FindAnalyzer("Add")).Returns(typeof(AddAnalyzer));

            var semanticAnalysis = new SemanticAnalyzer(new List<ISection> { mockTextSection }, mockSymbolTable.Object, mockIAnalyzerResolver.Object);

            semanticAnalysis.Perform();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Perform_AnalyzesInstructions_CannotFindAnalyzer()
        {
            var mockTextSection = new TextSection();
            IInstruction mockInstruction = new Instruction(new Token(TokenType.REGISTER, "ADD", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x1", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x2", 0, 0, 0));
            mockInstruction.ProcessOperand(new Token(TokenType.REGISTER, "x3", 0, 0, 0));
            mockTextSection.Instructions.Add(mockInstruction);

            var mockSymbolTable = new Mock<ISymbolTable>();
            var mockIAnalyzerResolver = new Mock<IAnalyzerResolver>();

            mockIAnalyzerResolver.Setup(x => x.FindAnalyzer("Add")).Throws( new Exception());

            var semanticAnalysis = new SemanticAnalyzer(new List<ISection> { mockTextSection }, mockSymbolTable.Object, mockIAnalyzerResolver.Object);

            semanticAnalysis.Perform();
        }

        internal class AddAnalyzer : IAnalyzer
        {
            public virtual void Analyze(IInstruction Instruction, ISymbolTable symbolTable)
            {
            }
        }
    }

}

