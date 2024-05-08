using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class BGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var bGenerater = new BGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "beq", 0, 0, 0);
            IToken rd = new Token(TokenType.REGISTER, "sp", 0, 0, 0);
            IToken rs1 = new Token(TokenType.REGISTER, "sp", 0, 0, 0);
            IToken rs2 = new Token(TokenType.HEX, "0x0FF", 0, 0, 0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;
            instruction.Rs2 = rs2;

            uint branchOffset = (uint)instruction.Rs2.NumericVal;

            // Calculate expected machine code:
            // - simm12[12|10:5] (bits 31-25) is taken from branchOffset
            // - simm12[4:1|11] (bits 11-7) is taken from branchOffset
            // - rs2 goes to bits 24-20
            // - rs1 goes to bits 19-15
            // - func3 goes to bits 14-12
            // - opcode6 goes to bits 6-0
            uint expectedMachineCode =
                (branchOffset & 0x800) << 20 |
                (branchOffset & 0x7E0) << 20 |
                (branchOffset & 0x1E) << 7 |
                (branchOffset & 0x100) >> 4 |
                (uint)instruction.Rs1.Value.ToEnum<Register>() << 20 |
                (uint)instruction.Rd.Value.ToEnum<Register>() << 15 |
                (uint)instruction.Funct3 << 12 |
                (uint)instruction.Opcode;

            uint actualMachineCode = bGenerater.Generate(instruction, null, 0);
            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}
