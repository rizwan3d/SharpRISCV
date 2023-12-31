﻿using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using System.Text;

namespace SharpRISCV.Core.V2.Program.Datas
{
    public class Data(IToken Type) : DataBase(Type), IData
    {
        private int? IntData { get; set; }
        private string StringData { get; set; } = string.Empty;

        public override bool IsComplete()
        {
            if (Directives.IsEnd(Type)) return true;
            return !string.IsNullOrEmpty(StringData) || IntData is not null;
        }

        public override object GetData()
        {
            return IntData is null ? StringData : IntData;
        }

        public List<byte> GetBytes()
        {
            List<byte> bytes = [];
            if (Directives.IsString(Type))
            {
                byte[] byteArray = new UTF8Encoding(true).GetBytes(StringData);
                bytes.AddRange(byteArray);
                bytes.Add(0);
            }
            else if (Directives.IsWord(Type))
            {
                byte[] byteArray = BitConverter.GetBytes(IntData.GetValueOrDefault());
                bytes.AddRange(byteArray);
            }
            else if (Directives.IsSpace(Type))
            {
                bytes.AddRange(new byte[IntData.GetValueOrDefault()]);
            }

            return bytes;
        }

        public override void SetData(IToken token)
        {
            if (Directives.IsWord(Type) || Directives.IsSpace(Type))
                IntData = token.NumericVal.GetValueOrDefault();
            else if (Directives.IsString(Type))
                StringData = token.Value;
            else
                throw new Exception($"Invalid Instruction at at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
