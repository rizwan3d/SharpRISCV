using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SharpRISCV.Core.V2.SemanticAnalysis
{
    public class SemanticAnalysis(List<ISection> sections)
    {
        public void Perform()
        {
            foreach (ISection section in sections.Where( s => s is ITextSection))
            {

            }
        }
    }
}
