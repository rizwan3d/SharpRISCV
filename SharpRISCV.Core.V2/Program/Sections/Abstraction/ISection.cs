using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Sections.Abstraction
{
    public interface ISection
    {
        IList<IInstruction> Instructions { get; set; }
        IList<IData> Data { get; set; }
        IList<Byte> Bytes { get; set; }
    }
}
