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
    public class RegisterProcessorTests
    {
        [TestMethod]
        public void Process_ShouldProcessOperandAndSetInCurrentInstruction_WhenTokenIsRegister()
        {
            var registerProcessor = new RegisterProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData data = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.REGISTER, "x10", 0, 0, 0);

            registerProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);

            Assert.AreEqual("x10", currentInstruction.Rd);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Process_ShouldNotThrowException_WhenCurrentInstructionIsNull()
        {
            var registerProcessor = new RegisterProcessor();
            IInstruction currentInstruction = null;
            IData data = null;
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.REGISTER, "x10", 0, 0, 0);

            registerProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }

        [TestMethod]
        public void Process_ShouldNotThrowException_WhenCurrentInstructionIsComplete()
        {
            var registerProcessor = new RegisterProcessor();
            IData data = null;
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.REGISTER, "x10", 0, 0, 0);

            registerProcessor.Process(sections, ref textSection, ref currentInstruction, ref data, token);
        }
    }
}
