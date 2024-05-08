using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class RGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var generater = new RGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "add",0,0,0);
            IToken rd = new Token(TokenType.REGISTER, "sp",0,0,0);
            IToken rs1 = new Token(TokenType.REGISTER, "sp",0,0,0);
            IToken rs2 = new Token(TokenType.REGISTER, "x10",0,0,0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;
            instruction.Rs2 = rs2;

            // Expected machine code calculation:
            // - Func7 (bits 31-25): 0x00 (shifted left 25)
            // - Rs2 (bits 24-20): X3 (shifted left 20)
            // - Rs1 (bits 19-15): X2 (shifted left 15)
            // - Func3 (bits 14-12): 0x0 (shifted left 12)
            // - Rd (bits 11-7): X1 (shifted left 7)
            // - Opcode6 (bits 6-0): 0x33 
            uint expectedMachineCode = (uint)instruction.Funct7 << 25;
            expectedMachineCode |= (uint)instruction.Rs2.Value.ToEnum<Register>() << 20;
            expectedMachineCode |= (uint)instruction.Rs1.Value.ToEnum<Register>() << 15;
            expectedMachineCode |= (uint)instruction.Funct3 << 12;
            expectedMachineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 7;
            expectedMachineCode |= (uint)instruction.Opcode;

            uint actualMachineCode = generater.Generate(instruction, null, 0);

            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}
