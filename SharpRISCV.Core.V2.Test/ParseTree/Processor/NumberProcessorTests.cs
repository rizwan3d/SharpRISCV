using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.ParseTree.Processor;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Datas;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Test.ParseTree.Processor
{
    [TestClass]
    public class NumberProcessorTests
    {
        [TestMethod]
        public void Process_ShouldThrowException_WhenCurrentInstructionIsNull()
        {
            var numberProcessor = new NumberProcessor();
            IInstruction currentInstruction = null;
            ISection textSection = new TextSection();
            IData data = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INTEGER,"42", 0, 0, 0);

            Assert.ThrowsException<Exception>(() => numberProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token));
        }

        [TestMethod]
        public void Process_ShouldAddDataToDataList_WhenCurrentSectionIsDataSection()
        {
            // Arrange
            var numberProcessor = new NumberProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData currentData = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            ISection dataSection = new DataSection();
            var sections = new List<ISection> { dataSection };
            var token = new Token(TokenType.INTEGER, "42", 0, 0, 0);

            // Act
            numberProcessor.Process(sections, ref dataSection, ref currentInstruction, ref currentData, token);

            // Assert
            CollectionAssert.Contains(dataSection.Data.ToList(), currentData);
        }

        [TestMethod]
        public void Process_ShouldAddDataToDataList_WhenCurrentSectionIsBssSection()
        {
            var numberProcessor = new NumberProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData currentData = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            ISection bssSection = new BssSection();
            var sections = new List<ISection> { bssSection };
            var token = new Token(TokenType.INTEGER, "42", 0, 0, 0);

            numberProcessor.Process(sections, ref bssSection, ref currentInstruction, ref currentData, token);

            CollectionAssert.Contains(bssSection.Data.ToList(), currentData);
        }

        [TestMethod]
        public void Process_ShouldProcessOperandInCurrentInstruction_WhenCurrentSectionIsNotDataOrBss()
        {
            var numberProcessor = new NumberProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            ISection textSection = new TextSection();
            IData data = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INTEGER, "42", 0, 0, 0);

            numberProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);

            Assert.AreEqual("42", currentInstruction.Rd.Value);
        }
    }
}
