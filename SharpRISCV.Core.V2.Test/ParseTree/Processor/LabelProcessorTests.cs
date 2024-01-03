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
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Test.ParseTree.Processor
{
    [TestClass]
    public class LabelProcessorTest
    {
        [TestMethod]
        public void Process_ShouldProcessOperandAndSetInCurrentInstruction_WhenTokenIsRegister()
        {
            var labelProcessor = new LabelProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            ISection textSection = new TextSection();
            IData data = null;
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.LABEL, "_start", 0, 0, 0);

            labelProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);

            Assert.AreEqual("_start", currentInstruction.Rd.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Process_ShouldNotThrowException_WhenCurrentInstructionIsNull()
        {
            var labelProcessor = new LabelProcessor();
            IData data = null;
            IInstruction currentInstruction = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.REGISTER, "_start", 0, 0, 0);

            labelProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }

        [TestMethod]
        public void Process_ShouldNotThrowException_WhenCurrentInstructionIsComplete()
        {
            var labelProcessor = new LabelProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData data = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.REGISTER, "_start", 0, 0, 0);

            labelProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }

        [TestMethod]
        public void Process_ShouldNotThrowException_WhenLableWithValidReloacationFuncation()
        {
            var labelProcessor = new LabelProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData data = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.LABEL, "%hi(.LOC)", 0, 0, 0);

            labelProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Process_ShouldThrowException_WhenLableWithInValidReloacationFuncation()
        {
            var labelProcessor = new LabelProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData data = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.LABEL, "%hi(.LOC", 0, 0, 0);

            labelProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }
    }
}
