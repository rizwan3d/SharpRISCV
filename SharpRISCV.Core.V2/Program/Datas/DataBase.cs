using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Program.Datas
{
    public abstract class DataBase : IData
    {
        protected DataBase(IToken Type) { this.Type = Type; }
        public IToken Type { get; set; }

        public abstract bool IsComplete();

        public abstract void SetData(IToken token);
    }
}