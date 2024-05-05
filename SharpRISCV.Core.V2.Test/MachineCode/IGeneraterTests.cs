using Newtonsoft.Json.Linq;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.MachineCode.Generaters;
using SharpRISCV.Core.V2.Program;
using SharpRISCV.Core.V2.Program.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.MachineCode
{
    [TestClass]
    public class IGeneraterTests
    {
        [TestMethod]
        public void Generate_CorrectMachineCode()
        {
            var generater = new IGenerater();

            IToken ins = new Token(TokenType.INSTRUCTION, "lw", 0, 0, 0);
            IToken rd = new Token(TokenType.REGISTER, "x1", 0, 0, 0);
            IToken rs1 = new Token(TokenType.REGISTER, "x2", 0, 0, 0);
            IToken rs2 = new Token(TokenType.HEX, "0x123", 0, 0, 0);

            var instruction = new Instruction(ins);
            instruction.Rd = rd;
            instruction.Rs1 = rs1;
            instruction.Rs2 = rs2;

            // Calculate expected machine code:
            // - Immediate value (simm12) goes to bits 31-20
            // - Rs1 goes to bits 19-15
            // - Funct3 goes to bits 14-12
            // - Rd goes to bits 11-7
            // - Opcode6 goes to bits 6-0
            uint expectedMachineCode = (uint)(instruction.Rs2.NumericVal.Value & 0xFFF) << 20;
            expectedMachineCode |= (uint)instruction.Rs1.Value.ToEnum<Register>() << 15;
            expectedMachineCode |= (uint)instruction.Funct3 << 12;
            expectedMachineCode |= (uint)instruction.Rd.Value.ToEnum<Register>() << 7;
            expectedMachineCode |= (uint)instruction.Opcode;

            uint actualMachineCode = generater.Generate(instruction, null, 0);

            Assert.AreEqual(expectedMachineCode, actualMachineCode);
        }
    }
}
