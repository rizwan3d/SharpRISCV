using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Datas;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class SectionBaseTests
    {
        [TestMethod]
        public void Instructions_ShouldBeInitializedAsEmptyList()
        {
            var section = new SectionBase();

            IList<IInstruction> instructions = section.Instructions;

            Assert.IsNotNull(instructions);
            Assert.AreEqual(0, instructions.Count);
        }

        [TestMethod]
        public void Data_ShouldBeInitializedAsEmptyList()
        {
            var section = new SectionBase();

            IList<IData> data = section.Data;

            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        public void Instructions_ShouldBeUpdatedWithNewList()
        {
            var section = new SectionBase();
            var newInstructions = new List<IInstruction>
        {
            new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0)),
            new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0))
        };

            section.Instructions = newInstructions;

            CollectionAssert.AreEqual(newInstructions, section.Instructions.ToList());
        }

        [TestMethod]
        public void Data_ShouldBeUpdatedWithNewList()
        {
            var section = new SectionBase();
            var newData = new List<IData>
        {
            new Data(new Token(TokenType.DIRECTIVE,".string",0,0,0)),
            new Data(new Token(TokenType.DIRECTIVE,".string",0,0,0)),
        };

            section.Data = newData;

            CollectionAssert.AreEqual(newData, section.Data.ToList());
        }
    }
}
