using SharpRISCV.Core.V2.Program.Datas.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.Program.Sections
{
    public class SectionBase : ISection
    {
        public List<IInstruction> Instructions { get; set; } = [];
        public List<IData> Data { get; set; } = [];
    }
}
