using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalToken;

namespace SharpRISCV.Core.V2.Test.LexicalAnalysis
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Lexer_Tokenize_SingleLine_Success()
        {
            string assemblyCode = "add $t0, $t1, $t2\nlw $s0, 100($sp)\n# This is a comment";

            Lexer lexer = new Lexer(assemblyCode);
            var tokens = lexer.Tokenize().ToList();
            tokens = tokens.ToList();

            Assert.IsNotNull(tokens);
            Assert.AreEqual(8, tokens.Count);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].TokenType);
            Assert.AreEqual("add", tokens[0].Value);

            Assert.AreEqual(TokenType.REGISTER, tokens[1].TokenType);
            Assert.AreEqual("t0", tokens[1].Value);

            Assert.AreEqual(TokenType.REGISTER, tokens[2].TokenType);
            Assert.AreEqual("t1", tokens[2].Value);

            Assert.AreEqual(TokenType.REGISTER, tokens[3].TokenType);
            Assert.AreEqual("t2", tokens[3].Value);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens[4].TokenType);
            Assert.AreEqual("lw", tokens[4].Value);

            Assert.AreEqual(TokenType.REGISTER, tokens[5].TokenType);
            Assert.AreEqual("s0", tokens[5].Value);

            Assert.AreEqual(TokenType.INTEGER, tokens[6].TokenType);
            Assert.AreEqual("100", tokens[6].Value);

            Assert.AreEqual(TokenType.REGISTER, tokens[7].TokenType);
            Assert.AreEqual("sp", tokens[7].Value);
        }

        [TestMethod]
        public void Lexer_Tokenize_MultiLine_Success()
        {
            string assemblyCode = @"#
            # Risc-V Assembler program to print ""Hello World!\\""
            # to stdout.
            #
            # a0-a2 - parameters to linux function services
            # a7 - linux function number
            # Linux System Calls https://marcin.juszkiewicz.com.pl/download/tables/syscalls.html
            #
            
            # Setup the parameters to print hello world
            # and then call Linux to do it.
            .text
            _start:
                        addi a0, x0, 1      # 1 = StdOut
                    la a1, helloworld
            #load address of helloworld
                    addi a2, x0, 13     # length of our string
                    addi a7, x0, 64     # linux write system call
                    ecall                # Call linux to output the string
            
            # Setup the parameters to exit the program
            # and then call Linux to do it.
            
                    addi    a0, x0, 0   # Use 0 return code
                    addi a7, x0, 93  # Service command code 93 terminates
                    ecall               # Call linux to terminate the program
            
            .data
            helloworld: .string ""Hello World!\n""";

            Lexer lexer = new Lexer(assemblyCode);
            var tokens = lexer.Tokenize().ToList();

            Assert.IsNotNull(tokens);
            Assert.AreEqual(35, tokens.Count);

            Assert.AreEqual(TokenType.DIRECTIVE, tokens[0].TokenType);
            Assert.AreEqual(".text", tokens[0].Value);

            Assert.AreEqual(TokenType.LABELDEFINITION, tokens[1].TokenType);
            Assert.AreEqual("_start:", tokens[1].Value);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens[2].TokenType);
            Assert.AreEqual("addi", tokens[2].Value);

            Assert.AreEqual(TokenType.LABEL, tokens[8].TokenType);
            Assert.AreEqual("helloworld", tokens[8].Value);

            Assert.AreEqual(TokenType.STRING, tokens[34].TokenType);
            Assert.AreEqual(@"""Hello World!\n""", tokens[34].Value);
        }

        [TestMethod]
        public void Lexer_Tokenize_Single_At_A_Time_SingleLine_Success()
        {
            string assemblyCode = "add $t0, $t1, $t2\nlw $s0, 100($sp)\n# This is a comment";

            Lexer lexer = new Lexer(assemblyCode);

            var tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens.TokenType);
            Assert.AreEqual("add", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.REGISTER, tokens.TokenType);
            Assert.AreEqual("t0", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.REGISTER, tokens.TokenType);
            Assert.AreEqual("t1", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.REGISTER, tokens.TokenType);
            Assert.AreEqual("t2", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens.TokenType);
            Assert.AreEqual("lw", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.REGISTER, tokens.TokenType);
            Assert.AreEqual("s0", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.INTEGER, tokens.TokenType);
            Assert.AreEqual("100", tokens.Value);

            tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.REGISTER, tokens.TokenType);
            Assert.AreEqual("sp", tokens.Value);
        }

        [TestMethod]
        public void Lexer_Tokenize_Pseudo_Instruction_Success()
        {
            string assemblyCode = "mv $t0, $t1";
            Lexer lexer = new Lexer(assemblyCode);

            var tokens = lexer.GetNextToken();
            Assert.IsNotNull(tokens);

            Assert.AreEqual(TokenType.INSTRUCTION, tokens.TokenType);
            Assert.AreEqual("mv", tokens.Value);
        }
    }
}
