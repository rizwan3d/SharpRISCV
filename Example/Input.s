.text
_start:

        addi  a0, x0, 1      # 1 = StdOut
        la    a1, prompt     # load address of helloworld
        addi  a2, x0, 24     # length of our string
        addi  a7, x0, 64     # linux write system call
        ecall                # Call linux to output the string

        addi  a0, x0, 0     # File descriptor, 0 for STDIN
        la    a1, buffer    # Address of buffer to store string
        addi  a2, x0, 256   # Maximum number of chars to store
        addi  a7, x0, 63    # linux call code for read       
        ecall

        addi  s0, a0, 0 

        addi  a0, x0, 1      # 1 = StdOut
        la    a1, msg        # load address of helloworld
        addi  a2, x0, 12     # length of our string
        addi  a7, x0, 64     # linux write system call
        ecall                # Call linux to output the string

        addi  a0, x0, 1      # 1 = StdOut
        la    a1, buffer     # load address of helloworld
        addi  a2, s0, 0      # length of our string
        addi  a7, x0, 64     # linux write system call
        ecall                # Call linux to output the string


# Setup the parameters to exit the program
# and then call Linux to do it.

        addi    a0, x0, 0   # Use 0 return code
        addi    a7, x0, 93  # Service command code 93 terminates
        ecall               # Call linux to terminate the program

.data
msg:          .string  "You entered: "
prompt:       .string  "Please enter a string: "

.bss
buffer:       .space  256

