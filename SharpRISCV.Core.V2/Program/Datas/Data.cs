using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Program.Datas
{
    class Data(IToken Type) : DataBase(Type), IData
    {
        private int? IntData { get; set; }
        private string StringData { get; set; } = string.Empty;

        public override bool IsComplete()
        {
            return !string.IsNullOrEmpty(StringData) || IntData is not null;
        }

        public override void SetData(IToken token)
        {
            if (Directives.IsWord(Type) || Directives.IsSpace(Type))
                IntData = token.NumericVal.GetValueOrDefault();
            if (Directives.IsString(Type))
                StringData = token.Value;
            else
                throw new Exception($"Invalid Instruction at at Line Number: {token.LineNumber}, Char: {token.StartIndex}.");
        }
    }
}
