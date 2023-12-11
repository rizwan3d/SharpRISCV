using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.FirstPass;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas;

namespace SharpRISCV.Core.V2.Test.Program
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void IsComplete_ShouldReturnTrue_WhenStringDataIsNotEmpty()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE,".string",0,0,0));
            data.SetData(new Token(TokenType.STRING, ".data", 0, 0, 0));

            bool result = data.IsComplete();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsComplete_ShouldReturnTrue_WhenIntDataIsNotNull()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "350", 0, 0, 0));

            bool result = data.IsComplete();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsComplete_ShouldReturnFalse_WhenStringDataAndIntDataAreNull()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0));

            bool result = data.IsComplete();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetData_ShouldReturnStringData_WhenTypeIsString()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            data.SetData(new Token(TokenType.STRING, "Hello", 0, 0, 0));

            object result = data.GetData();

            Assert.AreEqual("Hello", result);
        }

        [TestMethod]
        public void GetData_ShouldReturnIntData_WhenTypeIsInt()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "452", 0, 0, 0));

            object result = data.GetData();

            Assert.AreEqual(452, result);
        }

        [TestMethod]
        public void GetBytes_ShouldReturnByteArray_WhenTypeIsString()
        {
            List<byte> expated = new UTF8Encoding(true).GetBytes("Hello").ToList();
            expated.Add(0);

            var data = new Data(new Token(TokenType.DIRECTIVE, ".string", 0, 0, 0));
            data.SetData(new Token(TokenType.STRING, "Hello", 0, 0, 0));

            List<byte> result = data.GetBytes();

            CollectionAssert.AreEqual(expated, result);
        }

        [TestMethod]
        public void GetBytes_ShouldReturnByteArray_WhenTypeIsInt()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".space", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "42", 0, 0, 0));

            List<byte> result = data.GetBytes();

            CollectionAssert.AreEqual(new byte[42], result);
        }

        [TestMethod]
        public void GetBytes_ShouldReturnByteArray_WhenTypeIsWord()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".word", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "42", 0, 0, 0));

            List<byte> result = data.GetBytes();

            CollectionAssert.AreEqual(BitConverter.GetBytes(42).ToList(), result);
        }

        public void GetBytes_ShouldReturnByteArray_WhenTypeIsWord_Lessthan_Four_Bytes()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".word", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "42", 0, 0, 0));

            List<byte> result = data.GetBytes();

            CollectionAssert.AreEqual(BitConverter.GetBytes(42).ToList(), result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetData_Should_Throw_Exception()
        {
            var data = new Data(new Token(TokenType.DIRECTIVE, ".asd", 0, 0, 0));
            data.SetData(new Token(TokenType.INTEGER, "42", 0, 0, 0));
        }
    }
}
