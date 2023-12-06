using Newtonsoft.Json.Linq;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.LexicalAnalysis
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void Token_Properties_SetAndGetCorrectly()
        {
            TokenType expectedType = TokenType.INSTRUCTION;
            string expectedValue = "Add";

            IToken token = new Token(expectedType, expectedValue, 0, expectedValue.Length);

            Assert.AreEqual(expectedType, token.TokenType);
            Assert.AreEqual(expectedValue, token.Value);
            Assert.AreEqual(0, token.StartIndex);
            Assert.AreEqual(expectedValue.Length, token.Length);
        }
    }
}
