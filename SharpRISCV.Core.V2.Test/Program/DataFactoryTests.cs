using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalToken;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class DataFactoryTests
    {
        [TestMethod]
        public void CreateData_ShouldReturnDataInstance()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".word", 0, 0, 0);

            IData data = DataFactory.CreateData(mockToken);

            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(Data));
        }

        [TestMethod]
        public void CreateData_ShouldSetDataInCreatedInstance()
        {
            var mockToken = new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0);

            IData data = DataFactory.CreateData(mockToken);
            data.SetData(new Token(TokenType.STRING,"TestData",0,0,0));

            Assert.IsNotNull(data);
            Assert.AreEqual("TestData", ((Data)data).GetData());
        }
    }
}
