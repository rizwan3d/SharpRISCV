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
using static System.Collections.Specialized.BitVector32;

namespace SharpRISCV.Core.V2.Test.ParseTree
{
    [TestClass]
    public class RiscVTreeTests
    {
        [TestMethod]
        public void ParseProgram_ShouldProcessTokensAndReturnSections()
        {
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


        [TestMethod]
        public void Program_Test_0()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                //.text
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0))
                //addi x1, x0, 10
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x1", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x0", 0, 0, 0))
                .Returns(new Token(TokenType.INTEGER, "10", 0, 0, 0))
                //addi x2, x1, 20
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x2", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x1", 0, 0, 0))
                .Returns(new Token(TokenType.INTEGER, "20", 0, 0, 0))
                //.data
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0))
                //data:   .string "Hello"
                .Returns(new Token(TokenType.LABELDEFINITION, "data:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                //.text
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0))
                //add x4, x1, x3
                .Returns(new Token(TokenType.INSTRUCTION, "add", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x4", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x1", 0, 0, 0))
                .Returns(new Token(TokenType.REGISTER, "x3", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0))
                //.bss
                .Returns(new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 0))
                //add x4, x1, x3
                .Returns(new Token(TokenType.INSTRUCTION, "data2:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0))
                .Returns(new Token(TokenType.INTEGER, "20", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();

            Assert.IsNotNull(sections);
            Assert.AreEqual(3, sections.Count);
            Assert.IsInstanceOfType(sections[0], typeof(TextSection));
            Assert.IsInstanceOfType(sections[1], typeof(DataSection));
            Assert.IsInstanceOfType(sections[2], typeof(TextSection));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_1()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_2()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_3()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_4()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0))
                .Returns(new Token(TokenType.LABELDEFINITION, "data:", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_5()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.LABELDEFINITION, "data:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_6()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.LABELDEFINITION, "data:", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_7()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            riscVTree.ParseProgram();
        }

        [TestMethod]
        public void Program_Test_8()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0))
                .Returns(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();

            Assert.IsNotNull(sections);
            Assert.AreEqual(1, sections.Count);
            Assert.IsInstanceOfType(sections[0], typeof(ITextSection));
            Assert.AreEqual("addi", sections[0].Instructions[0].Opcode);

        }

        [TestMethod]
        public void Program_Test_9()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0))
                .Returns(new Token(TokenType.LABELDEFINITION, "lable:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            List<ISection> sections = riscVTree.ParseProgram();

            Assert.IsNotNull(sections);
            Assert.AreEqual(1, sections.Count);
            Assert.IsInstanceOfType(sections[0], typeof(IDataSection));
            Assert.AreEqual(@"""Hello""", sections[0].Data[0].GetData());

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Program_Test_10()
        {
            var lexer = new Mock<ILexer>();

            lexer.SetupSequence(x => x.GetNextToken())
                .Returns(new Token(TokenType.LABELDEFINITION, "lable:", 0, 0, 0))
                .Returns(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0))
                .Returns(new Token(TokenType.STRING, @"""Hello""", 0, 0, 0))
                .Returns(new Token(TokenType.EPSILONE, "", 0, 0, 0));

            IRiscVTree riscVTree = new RiscVTree(lexer.Object);

            riscVTree.ParseProgram();
        }
    }
}
