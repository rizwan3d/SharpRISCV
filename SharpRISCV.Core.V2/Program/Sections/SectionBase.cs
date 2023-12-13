using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.Program.Sections
{
    public class SectionBase : ISection
    {
        public IList<IInstruction> Instructions { get; set; } = [];
        public IList<IData> Data { get; set; } = [];
        public IList<Byte> Bytes { get; set; } = [];
    }
}
