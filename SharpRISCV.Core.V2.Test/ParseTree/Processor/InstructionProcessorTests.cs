using SharpRISCV.Core.V2.ParseTree.Processor;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Test.ParseTree.Processor
{
    [TestClass]
    public class InstructionProcessorTests
    {
        [TestMethod]
        public void Process_ShouldAddCurrentInstructionToInstructionsList_WhenCurrentInstructionIsComplete()
        {
            var instructionProcessor = new InstructionProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            ISection textSection = new TextSection();
            IData currentData = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INSTRUCTION, "add", 0, 0, 0);

            instructionProcessor.Process(sections, ref textSection, ref currentInstruction,ref currentData, token);

            var completedInstruction = textSection.Instructions.FirstOrDefault();
            Assert.AreEqual("addi", completedInstruction?.Opcode);
            Assert.AreEqual("add", currentInstruction?.Opcode);
        }

        [TestMethod]
        public void Process_ShouldNotAddCurrentInstructionToInstructionsList_WhenCurrentInstructionIsIncomplete()
        {
            var instructionProcessor = new InstructionProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "", 0, 0, 0));
            ISection textSection = new TextSection();
            IData currentData = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0);

            instructionProcessor.Process(sections, ref textSection, ref currentInstruction,ref currentData, token);

            CollectionAssert.DoesNotContain(textSection.Instructions, currentInstruction);
        }

        [TestMethod]
        public void Process_ShouldCreateNewInstruction_WhenCurrentInstructionIsComplete()
        {
            var instructionProcessor = new InstructionProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            ISection textSection = new TextSection();
            IData currentData = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0);

            instructionProcessor.Process(sections, ref textSection, ref currentInstruction, ref currentData, token);

            Assert.IsNotNull(currentInstruction);
            Assert.AreNotEqual("NewOpcode", currentInstruction.Opcode);
        }

        [TestMethod]
        public void Process_ShouldCreateNewInstruction_WhenCurrentInstructionIsNull()
        {
            var instructionProcessor = new InstructionProcessor();
            IInstruction currentInstruction = null;
            IData currentData = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0);

            instructionProcessor.Process(sections, ref textSection, ref currentInstruction,ref currentData, token);

            Assert.IsNotNull(currentInstruction);
        }
    }
}
