using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.FirstPass;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.Test.FirstPass
{
    [TestClass]
    public class ProcessSymbolTests
    {
        [TestMethod]
        public void ProcessSymbol_Start_AddsLabelToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 3))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add("Label1", 4), Times.Once);
        }

        [TestMethod]
        public void ProcessSymbol_Start_AddsLabel_At_Start_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 3))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add("Label1", 0), Times.Once);
        }

        [TestMethod]
        public void ProcessSymbol_Start_AddsLabel_Mulltipal_Time_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 14))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 14))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label3:", 3, 10))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add("Label1", 0), Times.Once);
            symbolTableMock.Verify(st => st.Add("Label2", 4), Times.Once);
            symbolTableMock.Verify(st => st.Add("Label3", 12), Times.Once);
        }
    }
}
