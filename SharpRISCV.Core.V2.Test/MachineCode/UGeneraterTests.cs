using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class UGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var uGenerater = new UGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "lui", 0, 0, 0);
            IToken rd = new Token(TokenType.REGISTER, "sp", 0, 0, 0);
            IToken rs1 = new Token(TokenType.HEX, "0x0FF", 0, 0, 0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;

            // Calculate expected machine code:
            // - simm20[19:12] and simm20[11:0] (bits 31-12)
            // - rd (bits 11-7)
            // - opcode6 (bits 6-0)
            uint expectedMachineCode =
                ((uint)instruction.Rs1.NumericVal & 0xFFFFF) << 12 |
                (uint)instruction.Rd.Value.ToEnum<Register>() << 7 |
                (uint)instruction.Opcode;

            uint actualMachineCode = uGenerater.Generate(instruction, null, 0);

            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}