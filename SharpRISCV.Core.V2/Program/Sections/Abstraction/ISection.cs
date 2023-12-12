using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;

namespace SharpRISCV.Core.V2.Program.Sections.Abstraction
{
    public interface ISection
    {
        List<IInstruction> Instructions { get; set; }
        List<IData> Data { get; set; }
    }
}
