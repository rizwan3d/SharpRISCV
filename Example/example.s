.text                            # code segment
    addi  sp,  sp, -16           # reserve stack space
    sw    ra,  12(sp)            # save return address
_start:                            # program entry
    lui   a0,  %hi(.LC0)         # load message address
    addi  a0,  a0, %lo(.LC0)     # message address
    la    x10, .LC0              # test la: load address
    lui   a5,  %hi(w_addr)       # test %hi() address
    lw    a5,  %lo(w_addr)(a5)   # test lw %lo() address
    lw    ra,  12(sp)            # restore return address
    addi  sp,  sp, 16            # release stack space
.data                            # data segment
w_addr:                          # word address
    .word 1495296332             # 1 word
    .word 1852403041             # 1 word
    .word 0,0                    # 2 words
.LC0: 
    .string "Hello, World!\n"  # a string
.end
