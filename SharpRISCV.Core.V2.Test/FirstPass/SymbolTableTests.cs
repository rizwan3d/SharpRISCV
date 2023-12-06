using SharpRISCV.Core.V2.FirstPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Test.FirstPass
{
    [TestClass]
    public class SymbolTableTests
    {
        [TestMethod]
        public void SymbolTable_Add_AddsSymbol()
        {
            SymbolTable symbolTable = new SymbolTable();

            symbolTable.Add("_start", 0x123);

            Assert.AreEqual(1, symbolTable.Count);
        }

        [TestMethod]
        public void SymbolTable_Indexer_ReturnsCorrectSymbol()
        {
            SymbolTable symbolTable = new SymbolTable();
            symbolTable.Add("_start", 0x123);

            ISymbolInfo symbol = symbolTable["_start"];

            Assert.IsNotNull(symbol);
            Assert.AreEqual("_start", symbol.Name);
            Assert.AreEqual((uint)0x123, symbol.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SymbolTable_Indexer_ThrowsExceptionForMissingSymbol()
        {
            SymbolTable symbolTable = new SymbolTable();

            ISymbolInfo symbol = symbolTable["_doAgain"];
        }
    }
}
