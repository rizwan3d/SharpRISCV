namespace SharpRISCV.Core.V2.LexicalToken
{
    public enum TokenType
    {
        INSTRUCTION, INTEGER, FLOAT, HEX, BINARY, REGISTER, LABEL, COMMENT, WHITESPACE, DIRECTIVE,
        STRING, LABELDEFINITION,
        COMMA,
        EPSILONE,
        EMPTY
    }
}
