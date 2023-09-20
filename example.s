.data                            # data segment
LC1:                             # string address
    .string "ABCDas"                  # 
.text
main2:                            # program entry
    addi  x2,  x2, -16           # reserve stack space addi  ;sp,  sp, -16 
    sw    x1,  12(x2)            # save return address       ;sw    ra,  12(sp) 
    lui   x10, 0x0               # load message address      ;lui   a0,  %hi(.LC0) 
    addi  x10, x10, 80           # load message address      ;lui   a0,  %hi(.LC0)
    la    x10, LC0               # test la: load address     ;la    x10, .LC0
.data                            # data segment
LC0:                             # string address
    .string "H"                  # 
    
 .text
main:                             # program entry
    addi  x2,  x2, -16           # reserve stack space addi  ;sp,  sp, -16 
    sw    x1,  12(x2)            # save return address
mainqw:
    addi  x2,  x2, -16           # reserve stack space addi  ;sp,  sp, -16 
    sw    x1,  12(x2)            # save return address