using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.Program.Instructions;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class InstructionTests
    {
        [TestMethod]
        public void IsComplete_ShouldReturnTrue_WhenMnemonicIsNotEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));

            bool result = instruction.IsComplete();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsRd_ShouldReturnTrue_WhenRdIsNullOrEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));

            bool result = instruction.IsRd();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsRs1_ShouldReturnTrue_WhenRs1IsNullOrEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));

            bool result = instruction.IsRs1();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsRs2_ShouldReturnTrue_WhenRs2IsNullOrEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));

            bool result = instruction.IsRs2();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessOperand_ShouldSetRd_WhenRs1IsEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            var operandToken = new Token(TokenType.REGISTER, "x0", 0, 0, 0);

            instruction.ProcessOperand(operandToken);

            Assert.AreEqual("x0", instruction.Rd.Value);
        }

        [TestMethod]
        public void ProcessOperand_ShouldSetRd_WhenRs1IsLabel()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            var operandToken = new Token(TokenType.LABEL, "_start", 0, 0, 0);

            instruction.ProcessOperand(operandToken);

            Assert.AreEqual("_start", instruction.Rd.Value);
        }

        [TestMethod]
        public void ProcessOperand_ShouldSetRd_WhenRs1IsInteger()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            var operandToken = new Token(TokenType.INTEGER, "450", 0, 0, 0);

            instruction.ProcessOperand(operandToken);

            Assert.AreEqual(450, instruction.Rd.NumericVal);
        }

        [TestMethod]
        public void ProcessOperand_ShouldSetRs2_WhenRs2IsEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            var operandToken = new Token(TokenType.REGISTER, "x0", 0, 0, 0);
            var operandToken2 = new Token(TokenType.REGISTER, "x1", 0, 0, 0);
            var operandToken3 = new Token(TokenType.REGISTER, "x3", 0, 0, 0);

            instruction.ProcessOperand(operandToken);
            instruction.ProcessOperand(operandToken2);
            instruction.ProcessOperand(operandToken3);

            Assert.AreEqual("x3", instruction.Rs2.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProcessOperand_ShouldThrowException_WhenAllOperandsAreNotEmpty()
        {
            var instruction = new Instruction(new Token(TokenType.INSTRUCTION, "addi", 0, 0, 0));
            var operandToken = new Token(TokenType.REGISTER, "x0", 0, 0, 0);
            var operandToken2 = new Token(TokenType.REGISTER, "x1", 0, 0, 0);
            var operandToken3 = new Token(TokenType.REGISTER, "x3", 0, 0, 0);
            var operandToken4 = new Token(TokenType.REGISTER, "x4", 0, 0, 0);

            instruction.ProcessOperand(operandToken);
            instruction.ProcessOperand(operandToken2);
            instruction.ProcessOperand(operandToken3);
            instruction.ProcessOperand(operandToken4);
        }
    }
}
