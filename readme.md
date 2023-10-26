# SharpRISCV #
SharpRISCV is an implementation of RISC-V assembly in C#.
First RISC V Assembly that build windows executable file.

### [Featured on RISC-V official website news section on home page.](https://web.archive.org/web/20231005155801/https://riscv.org/)

## [Try in Web Browser](https://rizwan3d.github.io/SharpRISCV/)

[Write RISC-V ASM code in your browser](https://rizwan3d.github.io/SharpRISCV/)

## RISC-V Virtual Machine 
You can use [RISC64-VM](https://github.com/rizwan3d/riscv64-vm) to run elf file on x86-x64 pc on linux and windows.

## Desktop Exectuable

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
1. [X] Generate PE (Even windows cannot support RISC V) AKA exe file - [Virustotal's Details](https://www.virustotal.com/gui/file/3a643bf62df82ae7824887bc2b9bdc45b0cd2ee7d9cbb54860833329b2ce2a3a/details)
1. [X] Generate Hex (FOR MCUs)
1. [X] Generate Hex (FOR MCUs) on Web Browser
1. [X] Generate Windows PE on Web Browser - [Virustotal's Details](https://www.virustotal.com/gui/file/068b7911f9bce1131c2fc0bf412e81e4231146a4e71dfc7b90e6d209d0c0826f/details)
2. [X] Generate ELF - [Virustotal's Details](https://www.virustotal.com/gui/file/95ee44dd11752294aa7cef26594c420989b7f5886ace0bf14e0771c95ffca200/details)
1. [ ] Generate Xilinx
1. [ ] Generate Altera
1. [ ] Generate Verilog

## Assembler Directives
1. [X] .text
1. [X] .data
1. [X] .bss
1. [X] .space
1. [X] .string and .asciz
1. [X] .word
1. [X] .%hi
1. [X] .%lo

# UesFull links and tools:
1. [RISC-V Instruction Formats](https://sourceware.org/binutils/docs/as/RISC_002dV_002dFormats.html)
1. [The RISC-V Instruction Set Manual](https://riscv.org/wp-content/uploads/2017/05/riscv-spec-v2.2.pdf) PDF Page 116
1. [RISC-V ASSEMBLY LANGUAGE Programmer Manual](https://shakti.org.in/docs/risc-v-asm-manual.pdf)
1. [RISC-V Instruction Encoder/Decoder](https://luplab.gitlab.io/rvcodecjs)
1. [PE file format - Introduction](https://0xrick.github.io/win-internals/pe1/) 
1. [Intel HEX](https://en.wikipedia.org/wiki/Intel_HEX)
1. [PE-bear for inspecting Windows PE (Portable Executable) files.](https://github.com/hasherezade/pe-bear) - [Download from here; I have added RISC-V machine code detection](https://ci.appveyor.com/project/hasherezade/pe-bear/builds/48225351)
1. [ELF file viewer/editor for Windows, Linux and MacOS](https://github.com/horsicq/XELFViewer)

## Support:
<p><a href="https://www.buymeacoffee.com/rizwan3d"> <img align="left" src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" height="50" width="210" alt="rizwan3d" /></a></p><br><br><br>

[![PayPal](https://img.shields.io/badge/PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white)](https://paypal.me/rizwan3d) 

