using Moq;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.SemanticAnalysis
{
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void IsRegister_ValidRegister_ReturnsTrue()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            mockToken.Setup(t => t.Value).Returns("x0");
            
            var result = analyzer.IsRegister(mockToken.Object);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsRegister_InvalidRegister_ThrowsException()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            mockToken.Setup(t => t.Value).Returns("InvalidRegister");

            var result = analyzer.IsRegister(mockToken.Object);
        }

        [TestMethod]
        public void IsLabel_ValidLabel_ReturnsTrue()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            var mockSymbolTable = new Mock<ISymbolTable>();
            var mockSymbolInfo = new Mock<ISymbolInfo>();
            mockToken.Setup(t => t.Value).Returns("ValidLabel");
            mockSymbolTable.Setup(st => st["ValidLabel"]).Returns(mockSymbolInfo.Object);

            var result = analyzer.IsLabel(mockToken.Object, mockSymbolTable.Object);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsLabel_InvalidLabel_ThrowsException()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            var mockSymbolTable = new Mock<ISymbolTable>();
            var mockSymbolInfo = new Mock<ISymbolInfo>();
            mockToken.Setup(t => t.Value).Returns("InvalidLabel");
            mockSymbolTable.Setup(st => st["InvalidLabel"]).Throws(new Exception());

            var result = analyzer.IsLabel(mockToken.Object, mockSymbolTable.Object);
        }

        [TestMethod]
        public void IsImm_NumericValue_ReturnsTrue()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            mockToken.Setup(t => t.NumericVal).Returns(42);

            var result = analyzer.IsImm(mockToken.Object);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsImm_NullNumericValue_ThrowsException()
        {
            var analyzer = new Mock<PublicAnalyzer>().Object;
            var mockToken = new Mock<IToken>();
            mockToken.Setup(t => t.NumericVal).Returns((int?)null);

            var result = analyzer.IsImm(mockToken.Object);
        }
    }

    public class PublicAnalyzer : Analyzer
    {
        public override void Analyze(IInstruction Instruction, ISymbolTable symbolTable)
        {
            throw new NotImplementedException();
        }

        public bool IsRegister(IToken Instruction)
        {
            return base.IsRegister(Instruction);
        }


        public bool IsLabel(IToken Instruction, ISymbolTable symbolTable)
        {
            return base.IsLabel(Instruction, symbolTable);
        }

        public bool IsImm(IToken Instruction)
        {
            return base.IsImm(Instruction);
        }
    }
}
