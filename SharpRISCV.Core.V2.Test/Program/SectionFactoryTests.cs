using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class SectionFactoryTests
    {
        [TestMethod]
        public void CreateSection_ShouldReturnTextSection_WhenTokenIsText()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".text", 0, 0, 0);

            ISection section = SectionFactory.CreateSection(mockToken);

            Assert.IsNotNull(section);
            Assert.IsInstanceOfType(section, typeof(TextSection));
        }

        [TestMethod]
        public void CreateSection_ShouldReturnDataSection_WhenTokenIsData()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".data", 0, 0, 0);

            ISection section = SectionFactory.CreateSection(mockToken);

            Assert.IsNotNull(section);
            Assert.IsInstanceOfType(section, typeof(DataSection));
        }

        [TestMethod]
        public void CreateSection_ShouldReturnBssSection_WhenTokenIsBss()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".bss", 0, 0, 0);

            ISection section = SectionFactory.CreateSection(mockToken);

            Assert.IsNotNull(section);
            Assert.IsInstanceOfType(section, typeof(BssSection));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateSection_ShouldThrowException_WhenTokenIsInvalid()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0);

            ISection section = SectionFactory.CreateSection(mockToken);
        }
    }
}
