using SharpRISCV.Core.V2.LexicalToken.Abstraction;

namespace SharpRISCV.Core.V2.Program.Datas.Abstraction
{
    public interface IData
    {
        bool IsComplete();
        void SetData(IToken token);

        IToken Type { get; set; }
    }
}