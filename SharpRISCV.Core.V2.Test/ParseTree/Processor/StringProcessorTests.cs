using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.ParseTree.Processor;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.Program.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.Program.Datas;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Test.ParseTree.Processor
{
    [TestClass]
    public class StringProcessorTests
    {
        [TestMethod]
        public void Process_ShouldSetDataInCurrentDataAndAddToDataList_WhenCurrentSectionIsDataSection()
        {
            var stringProcessor = new StringProcessor();
            IData currentData = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            ISection dataSection = new DataSection();
            var sections = new List<ISection> { dataSection };
            var token = new Token(TokenType.STRING, "Hello", 0, 0, 0);
            IInstruction currentInstruction = null;

            stringProcessor.Process(sections, ref dataSection, ref currentInstruction,ref currentData, token);

            Assert.AreEqual("Hello", currentData.GetData());
            CollectionAssert.Contains(dataSection.Data, currentData);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Process_ShouldThrowException_WhenCurrentSectionIsNotDataSection()
        {
            var stringProcessor = new StringProcessor();
            IData currentData = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            ISection textSection = new TextSection();
            var sections = new List<ISection> { textSection };
            var token = new Token(TokenType.STRING, "Hello", 0, 0, 0);
            IInstruction currentInstruction = null;

            stringProcessor.Process(sections, ref textSection, ref currentInstruction,ref currentData, token);
        }
    }
}
