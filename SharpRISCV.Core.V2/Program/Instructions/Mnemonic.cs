﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public enum Mnemonic
    {
        LA,
        LUI,
        AUIPC,
        ADDI,
        SLTI,
        SLTIU,
        XORI,
        ORI,
        ANDI,
        SLLI,
        SRLI,
        SRAI,
        ADD,
        SUB,
        SLL,
        SLT,
        SLTU,
        XOR,
        SRL,
        SRA,
        OR,
        AND,
        FENCE,
        FENCE_I,
        CSRRW,
        CSRRS,
        CSRRC,
        CSRRWI,
        CSRRSI,
        CSRRCI,
        ECALL,
        EBREAK,
        URET,
        SRET,
        MRET,
        WFI,
        SFENCE_VMA,
        LB,
        LH,
        LW,
        LBU,
        LHU,
        SB,
        SH,
        SW,
        JAL,
        JALR,
        BEQ,
        BNE,
        BLT,
        BGE,
        BLTU,
        BGEU,
        ADDIW,
        SLLIW,
        SRLIW,
        SRAIW,
        ADDW,
        SUBW,
        SLLW,
        SRLW,
        SRAW,
        LWU,
        LD,
        SD,
        MUL,
        MULH,
        MULHSU,
        MULHU,
        DIV,
        DIVU,
        REM,
        REMU,
        MULW,
        DIVW,
        DIVUW,
        REMW,
        REMUW,
        LR_W,
        SC_W,
        AMOSWAP_W,
        AMOADD_W,
        AMOXOR_W,
        AMOAND_W,
        AMOOR_W,
        AMOMIN_W,
        AMOMAX_W,
        AMOMINU_W,
        AMOMAXU_W,
        LR_D,
        SC_D,
        AMOSWAP_D,
        AMOADD_D,
        AMOXOR_D,
        AMOAND_D,
        AMOOR_D,
        AMOMIN_D,
        AMOMAX_D,
        AMOMINU_D,
        AMOMAXU_D,
        FMADD_S,
        FMSUB_S,
        FNMSUB_S,
        FNMADD_S,
        FADD_S,
        FSUB_S,
        FMUL_S,
        FDIV_S,
        FSQRT_S,
        FSGNJ_S,
        FSGNJN_S,
        FSGNJX_S,
        FMIN_S,
        FMAX_S,
        FCVT_W_S,
        FCVT_WU_S,
        FMV_X_W,
        FEQ_S,
        FLT_S,
        FLE_S,
        FCLASS_S,
        FCVT_S_W,
        FCVT_S_WU,
        FMV_W_X,
        FMADD_D,
        FMSUB_D,
        FNMSUB_D,
        FNMADD_D,
        FADD_DBEQZ,
        FSUB_D,
        FMUL_D,
        FDIV_D,
        FSQRT_D,
        FSGNJ_D,
        FSGNJN_D,
        FSGNJX_D,
        FMIN_D,
        FMAX_D,
        FCVT_S_D,
        FCVT_D_S,
        FEQ_D,
        FLT_D,
        FLE_D,
        FCLASS_D,
        FCVT_W_D,
        FCVT_WU_D,
        FCVT_D_W,
        FCVT_D_WU,
        FLW,
        FSW,
        FLD,
        FSD,
        FCVT_L_S,
        FCVT_LU_S,
        FCVT_S_L,
        FCVT_S_LU,
        FCVT_L_D,
        FCVT_LU_D,
        FMV_X_D,
        FCVT_D_L,
        FCVT_D_LU,
        FMV_D_X,
        C_ADDI4SPN,
        C_FLD,
        C_LW,
        C_FLW,
        C_LD,
        C_FSD,
        C_SW,
        C_FSW,
        C_SD,
        C_NOP,
        C_ADDI,
        C_JAL,
        C_ADDIW,
        C_LI,
        C_ADDI16SP,
        C_LUI,
        C_SRLI,
        C_SRAI,
        C_ANDI,
        C_SUB,
        C_XOR,
        C_OR,
        C_AND,
        C_SUBW,
        C_ADDW,
        C_J,
        C_BEQZ,
        C_BNEZ,
        C_SLLI,
        C_FLDSP,
        C_LWSP,
        C_FLWSP,
        C_LDSP,
        C_JR,
        C_MV,
        C_EBREAK,
        C_JALR,
        C_ADD,
        C_FSDSP,
        C_SWSP,
        C_FSWSP,
        C_SDSP,
        NOP,
        LI,
        MV,
        NOT,
        NEG,
        NEGW,
        SEXT_W,
        SEQZ,
        SNEZ,
        SLTZ,
        SGTZ,
        MV_S,
        FABS_S,
        FNEG_S,
        FMV_D,
        FABS_D,
        CSRC,
        BEQZ,
        BNEZ,
        BGEZ,
        BLTZ,
        BGTZ,
        BGT,
        BLE,
        BGTU,
        BLEU,
        J,
        JR,
        RET,
        CALL,
        TAIL,
        CSRR,
        CSRW,
        CSRS,
        CSRWI,
        CSRSI,
        CSRCI,
        FRCSR,
        FSCSR,
        FRRM,
        FSRM,
        FSRMI,
        FRFLAGS,
        FSFLAGSI
    }
}