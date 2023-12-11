using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Instructions;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class InstructionFactoryTests
    {
        [TestMethod]
        public void CreateInstruction_ShouldReturnInstructionInstance()
        {
            var mockToken = new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0);

            IInstruction data = InstructionFactory.CreateInstruction(mockToken);

            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(Instruction));
        }
    }
}
