using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Datas.Abstraction;

namespace SharpRISCV.Core.V2.Program.Datas
{
    class DataFactory
    {
        public static IData CreateData(IToken token)
        {
            return new Data(token);
        }
    }
}
