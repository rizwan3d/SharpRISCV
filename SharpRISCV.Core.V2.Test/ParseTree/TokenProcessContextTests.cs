using Moq;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.ParseTree;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.ParseTree
{
    [TestClass]
    public class TokenProcessContextTests
    {
        [TestMethod]
        public void ExecuteStrategy_ShouldCallProcessOnStrategy_WhenStrategyIsSet()
        {
            var context = new TokenProcessContext();
            var strategy = new Mock<ITokenProcessStrategy>();
            context.SetStrategy(strategy.Object);
            var sections = new List<ISection>();
            ISection currentSections = null;
            IInstruction currentInstruction = null;
            IData currentData = null;
            IToken token = null;

            context.ExecuteStrategy(sections, ref currentSections, ref currentInstruction, ref currentData, token);

            strategy.Verify(service => service.Process(sections, ref currentSections, ref currentInstruction,ref currentData, token), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]

        public void ExecuteStrategy_ShouldNotThrowException_WhenStrategyIsNull()
        {
            var context = new TokenProcessContext();
            context.SetStrategy(null);
            var sections = new List<ISection>();
            ISection currentSections = null;
            IInstruction currentInstruction = null;
            IData currentData = null;
            IToken token = null;

            context.ExecuteStrategy(sections, ref currentSections, ref currentInstruction,ref currentData, token);
        }
    }
}
