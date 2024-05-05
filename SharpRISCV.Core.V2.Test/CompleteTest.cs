using SharpRISCV.Core.V2.Directive;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.LexicalToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.ParseTree.Abstraction;
using SharpRISCV.Core.V2.ParseTree;
using SharpRISCV.Core.V2.Program.Sections.Abstraction;
using SharpRISCV.Core.V2.FirstPass;
using SharpRISCV.Core.V2.FirstPass.Abstraction;
using SharpRISCV.Core.V2.SemanticAnalysis;
using SharpRISCV.Core.V2.SemanticAnalysis.Abstraction;
using SharpRISCV.Core.V2.MachineCode;

namespace SharpRISCV.Core.V2.Test
{
    [TestClass]
    public class CompleteTest
    {
        [TestMethod]
        public void Sucess()
        {
            string assemblyCode = @".text                            # code segment
                                        add  sp,  sp, x10            # for testing R type
                                        addi  sp,  sp, -16           # reserve stack space

                                        sw    ra,  12(sp)            # save return address
                                    _start:                          # program entry
                                        lui   a0,  %hi(.LC0)         # load message address
                                        addi  a0,  a0, %lo(.LC0)     # message address
                                        la    x10, .LC0              # test la: load address
                                        lui   a5,  %hi(  w_addr)     # test %hi() address
                                        lw    a5,  %lo(w_addr)(a5)   # test lw %lo() address
                                        lw    ra,  12(sp)            # restore return address
                                        addi  sp,  sp, 16            # release stack space
                                    .data                            # data segment
                                    w_addr:                          # word address
                                        .word 1495296332             # 1 word
                                        .word 1852403041             # 1 word
                                        .word 0                      # 1 wordsd
                                    .LC0: 
                                        .string ""Hello, World!\n""    # a string
                                    .end
                                    ";
            ILexer lexer = new Lexer(assemblyCode);
            ISymbolTable symbolTable = new SymbolTable();
            IProcessSymbol processSymbol = new ProcessSymbol(lexer, symbolTable);
            processSymbol.Start();
            IRiscVTree riscVTree = new RiscVTree(lexer);
            List<ISection> sections = riscVTree.ParseProgram();
            IAnalyzerResolver analyzerResolver = new AnalyzerResolver();
            ISemanticAnalyzer semanticAnalysis = new SemanticAnalyzer(sections, symbolTable, analyzerResolver);
            semanticAnalysis.Perform();
            RiscVCode riscVCode = new RiscVCode(sections, symbolTable);
            riscVCode.Build();

            Assert.AreEqual<uint>(0x00a10133, sections[0].Instructions[0].MachineCodes.First());
            Assert.AreEqual<uint>(0xff010113, sections[0].Instructions[1].MachineCodes.First());
        }
    }
}
