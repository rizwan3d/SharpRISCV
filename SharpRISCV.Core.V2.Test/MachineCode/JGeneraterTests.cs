using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class JGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var jGenerater = new JGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "jal", 0, 0, 0);
            IToken rd = new Token(TokenType.REGISTER, "sp", 0, 0, 0);
            IToken rs1 = new Token(TokenType.HEX, "0x0FF", 0, 0, 0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;

            // Calculate expected machine code:
            // - simm20[20] (bit 31)
            // - simm20[10:1] (bits 30-21)
            // - simm20[11] (bit 20)
            // - simm20[19:12] (bits 19-12)
            // - rd (bits 11-7)
            // - opcode6 (bits 6-0)
            uint expectedMachineCode =
                (uint)(instruction.Rs1.NumericVal & 0x80000) << 11 |
                (uint)(instruction.Rs1.NumericVal & 0x7FE) << 20 |
                (uint)(instruction.Rs1.NumericVal & 0x1000) >> 9 |
                (uint)(instruction.Rs1.NumericVal & 0xFF000) >> 9 |
                (uint)instruction.Rd.Value.ToEnum<Register>() << 7 |
                (uint)instruction.Opcode;

            uint actualMachineCode = jGenerater.Generate(instruction, null, 0);

            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}
