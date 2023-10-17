window.addTooltips = () => {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
};
window.downloadFile = (fileName, base64String) => {
    const byteCharacters = atob(base64String);
    const byteNumbers = new Array(byteCharacters.length);

    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: "application/octet-stream" });

    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;

    document.body.appendChild(link);
    link.click();

    document.body.removeChild(link);
};

window.editor = null;

window.getEditorValue = () => {
    return window.editor.getValue();
};



window.createEditor = (elementId, initCode) => {
	window.addLang("rsicvasm");
    require(['vs/editor/editor.main'], function () {
        var editor = monaco.editor.create(document.getElementById(elementId), {
            value: initCode,
			language: "rsicvasm",
			automaticLayout: true,
			theme: "riscTheme",
        });

        window.editor = editor;
    });
};

window.addLang = () => {
	// Register a new language
	monaco.languages.register({ id: "rsicvasm" });

	// Register a tokens provider for the language
	monaco.languages.setMonarchTokensProvider("rsicvasm", {
		tokenizer: {
			root: [
				[/sp|ra|sp|gp|tp|fp|(a|s|x|t)[0-9]{1,2}/, "register"],
				[/([Aa-zA-Z0-9_\.]+:)/, "lable"],
				[/((?<!^)\.[a-zA-Z0-9_]+)|([Aa-zA-Z0-9_\.]+:)/, "lable"],
				[/^\s*\.[a-zA-Z0-9_]+/, "directive"],
				[/(?<!\S)(-?[0-9]+|0x[0-9A-Fa-f]+|0b[01]+)(?!\S)/, "numbers"],
				[/#.*$/, "comment"],
				[/([bruf]*)("""|'''|"|')(?:(?!\2)(?:\\.|[^\\]))*\2/, "string"],
				[/(?:^|\W)(la|lui|auipc|addi|slti|sltiu|xori|ori|andi|slli|srli|srai|add|sub|sll|slt|sltu|xor|srl|sra|or|and|fence|fence\.i|csrrw|csrrs|csrrc|csrrwi|csrrsi|csrrci|ecall|ebreak|uret|sret|mret|wfi|sfence\.vma|lb|lh|lw|lbu|lhu|sb|sh|sw|jal|jalr|beq|bne|blt|bge|bltu|bgeu|addiw|slliw|srliw|sraiw|addw|subw|sllw|srlw|sraw|lwu|ld|sd|mul|mulh|mulhsu|mulhu|div|divu|rem|remu|mulw|divw|divuw|remw|remuw|lr\.w|sc\.w|amoswap\.w|amoadd\.w|amoxor\.w|amoand\.w|amoor\.w|amomin\.w|amomax\.w|amominu\.w|amomaxu\.w|lr\.d|sc\.d|amoswap\.d|amoadd\.d|amoxor\.d|amoand\.d|amoor\.d|amomin\.d|amomax\.d|amominu\.d|amomaxu\.d|fmadd\.s|fmsub\.s|fnmsub\.s|fnmadd\.s|fadd\.s|fsub\.s|fmul\.s|fdiv\.s|fsqrt\.s|fsgnj\.s|fsgnjn\.s|fsgnjx\.s|fmin\.s|fmax\.s|fcvt\.w\.s|fcvt\.wu\.s|fmv\.x\.w|feq\.s|flt\.s|fle\.s|fclass\.s|fcvt\.s\.w|fcvt\.s\.wu|fmv\.w\.x|fmadd\.d|fmsub\.d|fnmsub\.d|fnmadd\.d|fadd\.dbeqz|fsub\.d|fmul\.d|fdiv\.d|fsqrt\.d|fsgnj\.d|fsgnjn\.d|fsgnjx\.d|fmin\.d|fmax\.d|fcvt\.s\.d|fcvt\.d\.s|feq\.d|flt\.d|fle\.d|fclass\.d|fcvt\.w\.d|fcvt\.wu\.d|fcvt\.d\.w|fcvt\.d\.wu|flw|fsw|fld|fsd|fcvt\.l\.s|fcvt\.lu\.s|fcvt\.s\.l|fcvt\.s\.lu|fcvt\.l\.d|fcvt\.lu\.d|fmv\.x\.d|fcvt\.d\.l|fcvt\.d\.lu|fmv\.d\.x|c\.addi4spn|c\.fld|c\.lw|c\.flw|c\.ld|c\.fsd|c\.sw|c\.fsw|c\.sd|c\.nop|c\.addi|c\.jal|c\.addiw|c\.li|c\.addi16sp|c\.lui|c\.srli|c\.srai|c\.andi|c\.sub|c\.xor|c\.or|c\.and|c\.subw|c\.addw|c\.j|c\.beqz|c\.bnez|c\.slli|c\.fldsp|c\.lwsp|c\.flwsp|c\.ldsp|c\.jr|c\.mv|c\.ebreak|c\.jalr|c\.add|c\.fsdsp|c\.swsp|c\.fswsp|c\.sdsp)(?:^|\W)/, "keyword"]
			],
		},
	});

	// Define a new theme that contains only rules that match this language
	monaco.editor.defineTheme("riscTheme", {
		base: "vs",
		inherit: false,
		rules: [
			{ token: "comment", foreground: "008000", fontStyle: 'italic' },
			{ token: "keyword", foreground: "0000FF", fontStyle: 'bold' },
			{ token: "numbers", foreground: "FFA500" },
			{ token: "string", foreground: "DF3079" },
			{ token: "directive", foreground: "808080" },
			{ token: "register", foreground: "800080", fontStyle: "bold" },
			{ token: "lable", foreground: "8B0000" },
		],
		colors: {
			"editor.foreground": "#000000",
		},
	});
};