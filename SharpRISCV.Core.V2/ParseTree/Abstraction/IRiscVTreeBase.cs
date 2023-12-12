using SharpRISCV.Core.V2.Program.Sections.Abstraction;

namespace SharpRISCV.Core.V2.ParseTree.Abstraction
{
    public interface IRiscVTree
    {
        List<ISection> ParseProgram();
    }
}