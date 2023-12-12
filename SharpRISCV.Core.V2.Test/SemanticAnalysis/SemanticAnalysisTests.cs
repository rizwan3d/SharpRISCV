using Moq;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis;
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
            var mockInstruction = new Mock<IInstruction>();
            mockInstruction.SetupGet(i => i.Mnemonic).Returns(Mnemonic.ADD);
            mockInstruction.SetupGet(i => i.Rd).Returns(new Token(TokenType.REGISTER,"x1",0,0,0));
            mockInstruction.SetupGet(i => i.Rs1).Returns(new Token(TokenType.REGISTER, "x2", 0, 0, 0));
            mockInstruction.SetupGet(i => i.Rs2).Returns(new Token(TokenType.REGISTER, "x3", 0, 0, 0));

            mockTextSection.Instructions.Add(mockInstruction.Object);

            var mockSymbolTable = new Mock<ISymbolTable>();

            var semanticAnalysis = new SemanticAnalyzer(new List<ISection> { mockTextSection }, mockSymbolTable.Object);

            semanticAnalysis.Perform();
        }
    }
}
