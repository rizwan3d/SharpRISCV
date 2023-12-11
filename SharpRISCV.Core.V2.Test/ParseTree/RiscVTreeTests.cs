using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using SharpRISCV.Core.V2.ParseTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalAnalysis;
using Moq;

namespace SharpRISCV.Core.V2.Test.ParseTree
{
    [TestClass]
    public class RiscVTreeTests
    {
        [TestMethod]
        public void ParseProgram_ShouldProcessTokensAndReturnSections()
        {
            // Arrange
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0))
                .Returns(new Token(TokenType.LABELDEFINITION, "data:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""text""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();

            Assert.IsNotNull(sections);
            Assert.AreEqual(2, sections.Count); 
            Assert.IsInstanceOfType(sections[0], typeof(TextSection));
            Assert.IsInstanceOfType(sections[1], typeof(DataSection));

        }
    }
}
