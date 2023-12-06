using SharpRISCV.Core.V2.LexicalAnalysis;
using SharpRISCV.Core.V2.LexicalAnalysis.Abstraction;
using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.ParseTree
{
    /// <summary>
    /// program -> statement program | epsilon
    /// statement -> instruction | directive | label_definition | label
    /// instruction -> opcode operands
    /// operands -> operand operands | epsilon
    /// operand -> register | immediate | label
    /// register -> 'zero' | 'ra' | 'sp' | 'gp' | 'tp' | 't[0-6]' | 's[0-1]' | 'a[0-7]' | 's[0-9]' | 't[0-2][0-9]' | 't3[0-1]' | 'x[0-9]{1,2}'
    /// immediate -> integer | float | hex | binary | string
    /// directive -> '.directive' operands
    /// label_definition -> label ':'
    /// label -> [a-zA-Z_]
    ///     [a-zA-Z0-9_]*
    /// opcode -> [a-zA-Z]+
    /// integer -> [0-9]+
    /// float -> [0-9]+\.[0-9]+
    /// hex -> '0x'[0-9A-Fa-f]+
    /// binary -> '0b'[01]+
    /// string -> '"' .* '"'
    /// </summary>
    class Parser
    {
        private readonly ILexer lexer;

        public Parser(ILexer lexer)
        {
            this.lexer = lexer;
        }

        public Node ParseProgram()
        {
            Node programNode = new Node("program");
            while (true)
            {
                var token = lexer.GetNextToken();
                if (IToken.IsEndOfFile(token))
                    break;

                Node statementNode = ParseStatement(token);
                programNode.AddChild(statementNode);
            }

            return programNode;
        }

        private Node ParseStatement(IToken token)
        {
            Node statementNode = new Node("statement");
            return statementNode;
        }

    }
}
