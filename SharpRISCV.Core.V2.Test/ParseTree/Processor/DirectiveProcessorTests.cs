using SharpRISCV.Core.V2.ParseTree.Processor;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Datas;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using static System.Collections.Specialized.BitVector32;

namespace SharpRISCV.Core.V2.Test.ParseTree.Processor
{
    [TestClass]
    public class DirectiveProcessorTests
    {
        [TestMethod]
        public void Process_ShouldCreateNewSection_WhenTokenIsSectionDirective()
        {
            var directiveProcessor = new DirectiveProcessor();
            IInstruction currentInstruction = null;
            IData currentData = null;
            ISection section = null;
            var sections = new List<ISection> { };
            var token = new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0);

            directiveProcessor.Process(sections, ref section, ref currentInstruction, ref currentData, token);

            Assert.AreEqual(1, sections.Count);
            Assert.IsInstanceOfType(sections[0], typeof(ITextSection));
        }

        [TestMethod]
        public void Process_ShouldAddCurrentInstructionToInstructionsList_WhenTokenIsNotSectionDirectiveAndCurrentInstructionIsComplete()
        {
            var directiveProcessor = new DirectiveProcessor();
            IInstruction currentInstruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            IData currentData = null;
            ISection section = new TextSection();
            var sections = new List<ISection> { section };
            var token = new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0);

            directiveProcessor.Process(sections, ref section, ref currentInstruction, ref currentData, token);
            Assert.AreEqual(Mnemonic.ADDI, sections[0].Instructions[0].Mnemonic);
            Assert.AreEqual(2, sections.Count);
        }

        [TestMethod]
        public void Process_ShouldNotAddCurrentInstructionToInstructionsList_WhenTokenIsNotSectionDirectiveAndCurrentInstructionIsIncomplete()
        {
            var directiveProcessor = new DirectiveProcessor();
            IInstruction currentInstruction = null;
            ISection section = null;
            IData currentData = null;
            var sections = new List<ISection> { };
            var token = new Token(TokenType.DIRECTIVE, "string", 0, 0, 0);

            directiveProcessor.Process(sections, ref section, ref currentInstruction, ref currentData, token);

            Assert.IsNotNull(currentData);
        }

        [TestMethod]
        public void Process_ShouldCreateNewData_WhenTokenIsNotSectionDirectiveAndCurrentDataIsComplete()
        {
            // Arrange
            var directiveProcessor = new DirectiveProcessor();
            IInstruction currentInstruction = null;
            IData currentData = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            currentData.SetData(new Token(TokenType.REGISTER, "x0", 0, 0, 0));
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0);

            directiveProcessor.Process(sections, ref textSection, ref currentInstruction,ref currentData, token);

            Assert.IsNotNull(currentData);
        }
    }
}
