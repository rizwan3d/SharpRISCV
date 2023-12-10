namespace SharpRISCV.Core.V2.LexicalAnalysis
{
    public static class Pattern
    {
        public static readonly string Comment = @"#\s.*$";
        public static readonly string String = @"""[^""]*""";
        public static readonly string Comma = ",";
        public static readonly string Instruction = @"\b(?:la|lui|auipc|addi|slti|sltiu|xori|ori|andi|slli|srli|srai|add|sub|sll|slt|sltu|xor|srl|sra|or|and|fence|fence\.i|csrrw|csrrs|csrrc|csrrwi|csrrsi|csrrci|ecall|ebreak|uret|sret|mret|wfi|sfence\.vma|lb|lh|lw|lbu|lhu|sb|sh|sw|jal|jalr|beq|bne|blt|bge|bltu|bgeu|addiw|slliw|srliw|sraiw|addw|subw|sllw|srlw|sraw|lwu|ld|sd|mul|mulh|mulhsu|mulhu|div|divu|rem|remu|mulw|divw|divuw|remw|remuw|lr\.w|sc\.w|amoswap\.w|amoadd\.w|amoxor\.w|amoand\.w|amoor\.w|amomin\.w|amomax\.w|amominu\.w|amomaxu\.w|lr\.d|sc\.d|amoswap\.d|amoadd\.d|amoxor\.d|amoand\.d|amoor\.d|amomin\.d|amomax\.d|amominu\.d|amomaxu\.d|fmadd\.s|fmsub\.s|fnmsub\.s|fnmadd\.s|fadd\.s|fsub\.s|fmul\.s|fdiv\.s|fsqrt\.s|fsgnj\.s|fsgnjn\.s|fsgnjx\.s|fmin\.s|fmax\.s|fcvt\.w\.s|fcvt\.wu\.s|fmv\.x\.w|feq\.s|flt\.s|fle\.s|fclass\.s|fcvt\.s\.w|fcvt\.s\.wu|fmv\.w\.x|fmadd\.d|fmsub\.d|fnmsub\.d|fnmadd\.d|fadd\.dbeqz|fsub\.d|fmul\.d|fdiv\.d|fsqrt\.d|fsgnj\.d|fsgnjn\.d|fsgnjx\.d|fmin\.d|fmax\.d|fcvt\.s\.d|fcvt\.d\.s|feq\.d|flt\.d|fle\.d|fclass\.d|fcvt\.w\.d|fcvt\.wu\.d|fcvt\.d\.w|fcvt\.d\.wu|flw|fsw|fld|fsd|fcvt\.l\.s|fcvt\.lu\.s|fcvt\.s\.l|fcvt\.s\.lu|fcvt\.l\.d|fcvt\.lu\.d|fmv\.x\.d|fcvt\.d\.l|fcvt\.d\.lu|fmv\.d\.x|c\.addi4spn|c\.fld|c\.lw|c\.flw|c\.ld|c\.fsd|c\.sw|c\.fsw|c\.sd|c\.nop|c\.addi|c\.jal|c\.addiw|c\.li|c\.addi16sp|c\.lui|c\.srli|c\.srai|c\.andi|c\.sub|c\.xor|c\.or|c\.and|c\.subw|c\.addw|c\.j|c\.beqz|c\.bnez|c\.slli|c\.fldsp|c\.lwsp|c\.flwsp|c\.ldsp|c\.jr|c\.mv|c\.ebreak|c\.jalr|c\.add|c\.fsdsp|c\.swsp|c\.fswsp|c\.sdsp|nop|li|mv|not|neg|negw|sext.w|seqz|snez|sltz|sgtz|mv.s|fabs.s|fneg.s|fmv.d|fabs.d|fneg.d|csrc|beqz|bnez|bnez|bgez|bltz|bgtz|bgt|ble|bgtu|bleu|j|jr|ret|call|tail|csrr|csrw|csrs|csrwi|csrsi|csrci|frcsr|fscsr|frrm|fsrm|fsrmi|fsrmi|frflags|fsflagsi)\b";
        public static readonly string Register = @"\b(?:zero|ra|sp|gp|tp|t[0-6]|s[0-1]|a[0-7]|s[0-9]|t[0-2][0-9]|t3[0-1]|x[0-9]{1,2})\b";
        public static readonly string Integer = @"\b\d+\b";
        public static readonly string Float = @"\b\d+\.\d+\b";
        public static readonly string Hex = @"\b0x[0-9A-Fa-f]+\b";
        public static readonly string Binary = @"\b0b[01]+\b";
        public static readonly string Directive = @"\.\w+";
        public static readonly string Lable = $@"\b[a-zA-Z_][a-zA-Z_0-9]*(?!({Register}))";
        public static readonly string LabeDefinition = @"\b[a-zA-Z_][a-zA-Z_0-9]*:";
        public static readonly string WhiteSpace = @"\s+";
    }
}
