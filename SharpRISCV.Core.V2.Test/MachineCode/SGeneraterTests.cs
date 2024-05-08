using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class SGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var sGenerater = new SGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "sb", 0, 0, 0);
            IToken rd = new Token(TokenType.REGISTER, "sp", 0, 0, 0);
            IToken rs1 = new Token(TokenType.HEX, "0x0FF", 0, 0, 0);
            IToken rs2 = new Token(TokenType.REGISTER, "sp", 0, 0, 0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;
            instruction.Rs2 = rs2;

            // Calculate expected machine code:
            // - simm12[4:0] (bits 11-7) and simm12[11:5] (bits 31-25)
            // - rs2 goes to bits 24-20
            // - rs1 goes to bits 19-15
            // - func3 goes to bits 14-12
            // - opcode6 goes to bits 6-0
            uint expectedMachineCode =
                ((uint)instruction.Rs1.NumericVal & 0x1F) << 7 |
                ((uint)instruction.Rs1.NumericVal & 0xFE0) << 20 |
                (uint)instruction.Rs2.Value.ToEnum<Register>() << 20 |
                (uint)instruction.Rd.Value.ToEnum<Register>() << 15 |
                (uint)instruction.Funct3 << 12 |
                (uint)instruction.Opcode;

            uint actualMachineCode = sGenerater.Generate(instruction, null, 0);

            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}
