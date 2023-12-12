using Newtonsoft.Json.Linq;
using SharpRISCV.Core.V2.FirstPass;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.FirstPass
{
    [TestClass]
    public class SymbolInfoTest
    {
        [TestMethod]
        public void SymbolInfo_Properties_SetAndGetCorrectly()
        {
            string expectedName = "_start";
            uint expectedAddress = 40;

            ISymbolInfo symbol = new SymbolInfo(expectedName, expectedAddress);

            Assert.AreEqual(expectedName, symbol.Name);
            Assert.AreEqual(expectedAddress, symbol.Address);
        }
    }
}
