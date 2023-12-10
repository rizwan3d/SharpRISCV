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
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 3))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 0, 3))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10), 4), Times.Once);
        }

        [TestMethod]
        public void ProcessSymbol_Start_AddsLabel_At_Start_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 0, 3))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10), 0), Times.Once);
        }

        [TestMethod]
        public void ProcessSymbol_Start_AddsLabel_Mulltipal_Time_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 0, 14))
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 10, 0, 14))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label3:", 3, 0, 10))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add(new Token(TokenType.LABELDEFINITION, "Label1:", 3, 0, 10), 0), Times.Once);
            symbolTableMock.Verify(st => st.Add(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10), 4), Times.Once);
            symbolTableMock.Verify(st => st.Add(new Token(TokenType.LABELDEFINITION, "Label3:", 3, 0, 10), 12), Times.Once);
        }

        [TestMethod]
        public void ProcessSymbol_Start_AddsLabel_Mulltipal_Time_In_Data_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            var ss = new SymbolTable();
            
            var lable1 = new Token(TokenType.LABELDEFINITION, "Label1", 3, 0, 10);
            var lable2 = new Token(TokenType.LABELDEFINITION, "Label2", 3, 0, 10);
            var lable3 = new Token(TokenType.LABELDEFINITION, "Label3", 3, 0, 10);
            var lable4 = new Token(TokenType.LABELDEFINITION, "Label4", 3, 0, 10);
            var lable5 = new Token(TokenType.LABELDEFINITION, "Label5", 3, 0, 10);
            var lable6 = new Token(TokenType.LABELDEFINITION, "Label6", 3, 0, 10);
            var lable7 = new Token(TokenType.LABELDEFINITION, "Label7", 3, 0, 10);
            var lable8 = new Token(TokenType.LABELDEFINITION, "Label8", 3, 0, 10);

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 3))
                .Returns(lable1)
                .Returns(new Token(TokenType.INSTRUCTION, "ADD", 0, 0, 3))
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 3))
                .Returns(lable2)
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 10, 0, 14))
                .Returns(new Token(TokenType.STRING, @"""Hello World""", 10, 0, 14))
                .Returns(lable3)
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 10, 0, 14))
                .Returns(new Token(TokenType.STRING, @"""Hello World""", 10, 0, 14))
                .Returns(lable4)
                .Returns(new Token(TokenType.DIRECTIVE, ".word", 10, 0, 14))
                .Returns(new Token(TokenType.INTEGER, @"10", 10, 0, 14))
                .Returns(lable5)
                .Returns(new Token(TokenType.DIRECTIVE, ".word", 10, 0, 14))
                .Returns(new Token(TokenType.HEX, @"0x10", 10, 0, 14))
                .Returns(lable6)
                .Returns(new Token(TokenType.DIRECTIVE, ".word", 10, 0, 14))
                .Returns(new Token(TokenType.BINARY, @"0b10", 10, 0, 14))
                .Returns(new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 3))
                .Returns(lable7)
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 10, 0, 14))
                .Returns(new Token(TokenType.INTEGER, @"256", 10, 0, 14))
                .Returns(lable8)
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 10, 0, 14))
                .Returns(new Token(TokenType.INTEGER, @"256", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, ss); // symbolTableMock.Object);

            processSymbol.Start();

            symbolTableMock.Verify(st => st.Add(lable1, 0), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable2, 4), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable3, 18), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable4, 32), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable5, 37), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable6, 42), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable7, 47), Times.Once);
            symbolTableMock.Verify(st => st.Add(lable8, 304), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_String_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_Word_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.DIRECTIVE, ".word", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_Data_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".asds", 0, 0, 3));

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_Space_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessSymbol_Start_AddsLabel_Invalid_Space_Size_Directive_ToSymbolTable()
        {
            var lexerMock = new Mock<ILexer>();
            var symbolTableMock = new Mock<ISymbolTable>();

            lexerMock.SetupSequence(l => l.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 3))
                .Returns(new Token(TokenType.LABELDEFINITION, "Label2:", 3, 0, 10))
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 10, 0, 14))
                .Returns(new Token(TokenType.DIRECTIVE, "asd", 10, 0, 14))
                .Returns(Token.EndOfFile);

            var processSymbol = new ProcessSymbol(lexerMock.Object, symbolTableMock.Object);

            processSymbol.Start();
        }
    }
}
