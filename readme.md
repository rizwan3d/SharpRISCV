# SharpRISCV #
SharpRISCV is an implementation of RISC-V assembly in C#.
First RISC V Assembly that build windows executable file

![Output Image](https://github.com/rizwan3d/SharpRISCV/blob/master/img.jpg?raw=true)

## Usage
### Build Bin file
```bash 
SharpRISCV.exe -i file.s -o out.o -p bin
```
### Build Windows EXE
```bash 
SharpRISCV.exe -i file.s -o out.exe -p pe
```
### Console Output
```bash 
SharpRISCV.exe -i file.s -o console
```
### Build Linux ELF
```bash 
SharpRISCV.exe -i file.s -o out.o -p elf
```
### Build HEX
```bash 
SharpRISCV.exe -i file.s -o out.o -p hex
```

## Supported Instruction
1. [X] R Type
1. [X] U Type
1. [X] I Type
1. [X] B Type
1. [X] I Type
1. [X] S Type
1. [X] J Type


## To Do
1. [X] Gererate Console output.
1. [X] Lable Support.
1. [X] Read form file (*.s).
1. [X] Ignore Comments
1. [X] Generate PE (Even windows cannot support RISC V) AKA exe file - (Virustotal's Details)(https://www.virustotal.com/gui/file/3a643bf62df82ae7824887bc2b9bdc45b0cd2ee7d9cbb54860833329b2ce2a3a/details)
1. [ ] Generate Xilinx
1. [ ] Generate Altera
1. [ ] Generate Verilog
1. [ ] Generate ELF

## Assembler Directives
1. [X] .text
1. [X] .data
1. [X] .string and .asciz
1. [X] .word
1. [X] .%hi
1. [X] .%lo

# UesFull links:
1. [RISC-V Instruction Formats](https://sourceware.org/binutils/docs/as/RISC_002dV_002dFormats.html)
1. [The RISC-V Instruction Set Manual](https://riscv.org/wp-content/uploads/2017/05/riscv-spec-v2.2.pdf) PDF Page 116
1. [RISC-V ASSEMBLY LANGUAGE Programmer Manual](https://shakti.org.in/docs/risc-v-asm-manual.pdf)
1. [RISC-V Instruction Encoder/Decoder](https://luplab.gitlab.io/rvcodecjs)
1. [PE file format - Introduction](https://0xrick.github.io/win-internals/pe1/) 
