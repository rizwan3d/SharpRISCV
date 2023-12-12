using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test
{
    [TestClass]
    public class DirectivesTests
    {
        [TestMethod]
        public void IsText_ShouldReturnTrue_WhenTokenIsText()
        {
            IToken textToken = new Token(TokenType.DIRECTIVE,".text", 0, 0, 0);

            bool result = Directives.IsText(textToken);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsText_ShouldReturnFalse_WhenTokenIsNotText()
        {
            IToken dataToken = new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0);

            bool result = Directives.IsText(dataToken);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsData_ShouldReturnTrue_WhenTokenIsData()
        {
            IToken dataToken = new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0);

            bool result = Directives.IsData(dataToken);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsData_ShouldReturnFalse_WhenTokenIsNotData()
        {
            IToken textToken = new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0);

            bool result = Directives.IsData(textToken);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsText_ShouldReturnTrue_WhenTokenIsSection()
        {
            IToken textToken = new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0);

            bool result = Directives.IsSection(textToken);
            Assert.IsTrue(result);

            textToken = new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0);

            result = Directives.IsSection(textToken);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsText_ShouldReturnTrue_WhenTokenIsString()
        {
            IToken textToken = new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0);

            bool result = Directives.IsString(textToken);
            Assert.IsTrue(result);

            textToken = new Token(TokenType.DIRECTIVE, ".asciz", 0, 0, 0);

            result = Directives.IsString(textToken);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsText_ShouldReturnTrue_WhenTokenIsSpace()
        {
            IToken textToken = new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0);

            bool result = Directives.IsSpace(textToken);
            Assert.IsTrue(result);
        }
    }
}
