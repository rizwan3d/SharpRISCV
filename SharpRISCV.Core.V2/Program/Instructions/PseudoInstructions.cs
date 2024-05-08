using SharpRISCV.Core.V2.LexicalToken.Abstraction;
using SharpRISCV.Core.V2.Program.Instructions.Abstraction;
using System;
using System.Linq;

namespace SharpRISCV.Core.V2.Program.Instructions
{
    public static class PseudoInstructions
    {
        private static Dictionary<Mnemonic, uint> pseudoInstructions;

        static PseudoInstructions()
        {
            pseudoInstructions = new Dictionary<Mnemonic, uint>();
            pseudoInstructions.Add(Mnemonic.LA, 2);
            pseudoInstructions.Add(Mnemonic.LB, 2);
            pseudoInstructions.Add(Mnemonic.LH, 2);
            pseudoInstructions.Add(Mnemonic.LW, 2);
            pseudoInstructions.Add(Mnemonic.LD, 2);
            pseudoInstructions.Add(Mnemonic.SB, 2);
            pseudoInstructions.Add(Mnemonic.SH, 2);
            pseudoInstructions.Add(Mnemonic.SW, 2);
            pseudoInstructions.Add(Mnemonic.SD, 2);
            pseudoInstructions.Add(Mnemonic.FLW, 2);
            pseudoInstructions.Add(Mnemonic.FLD, 2);
            pseudoInstructions.Add(Mnemonic.FSW, 2);
            pseudoInstructions.Add(Mnemonic.FSD, 2);
            pseudoInstructions.Add(Mnemonic.NOP, 1);
            pseudoInstructions.Add(Mnemonic.LI, 1);
            pseudoInstructions.Add(Mnemonic.MV, 1);
            pseudoInstructions.Add(Mnemonic.NOT, 1);
            pseudoInstructions.Add(Mnemonic.NEG, 1);
            pseudoInstructions.Add(Mnemonic.NEGW, 1);
            pseudoInstructions.Add(Mnemonic.SEXT_W, 1);
            pseudoInstructions.Add(Mnemonic.SEQZ, 1);
            pseudoInstructions.Add(Mnemonic.SNEZ, 1);
            pseudoInstructions.Add(Mnemonic.SLTZ, 1);
            pseudoInstructions.Add(Mnemonic.FMV_S, 1);
            pseudoInstructions.Add(Mnemonic.FABS_S, 1);
            pseudoInstructions.Add(Mnemonic.FNEG_S, 1);
            pseudoInstructions.Add(Mnemonic.FMV_D, 1);
            pseudoInstructions.Add(Mnemonic.FABS_D, 1);
            pseudoInstructions.Add(Mnemonic.FNEG_D, 1);
            pseudoInstructions.Add(Mnemonic.BEQZ, 1);
            pseudoInstructions.Add(Mnemonic.BNEZ, 1);
            pseudoInstructions.Add(Mnemonic.BLEZ, 1);
            pseudoInstructions.Add(Mnemonic.BGEZ, 1);
            pseudoInstructions.Add(Mnemonic.BLTZ, 1);
            pseudoInstructions.Add(Mnemonic.BGTZ, 1);
            pseudoInstructions.Add(Mnemonic.BGT, 1);
            pseudoInstructions.Add(Mnemonic.BLE, 1);
            pseudoInstructions.Add(Mnemonic.BGTU, 1);
            pseudoInstructions.Add(Mnemonic.BLEU, 1);
            pseudoInstructions.Add(Mnemonic.J, 1);
            pseudoInstructions.Add(Mnemonic.JAL, 1);
            pseudoInstructions.Add(Mnemonic.JR, 1);
            pseudoInstructions.Add(Mnemonic.JALR, 1);
            pseudoInstructions.Add(Mnemonic.RET, 1);
            pseudoInstructions.Add(Mnemonic.CALL, 2);
            pseudoInstructions.Add(Mnemonic.TAIL, 2);
            pseudoInstructions.Add(Mnemonic.FENCE, 1);
            pseudoInstructions.Add(Mnemonic.CSRR, 1);
            pseudoInstructions.Add(Mnemonic.CSRW, 1);
            pseudoInstructions.Add(Mnemonic.CSRS, 1);
            pseudoInstructions.Add(Mnemonic.CSRC, 1);
            pseudoInstructions.Add(Mnemonic.CSRWI, 1);
            pseudoInstructions.Add(Mnemonic.CSRSI, 1);
            pseudoInstructions.Add(Mnemonic.CSRCI, 1);
            pseudoInstructions.Add(Mnemonic.FRCSR, 1);
            pseudoInstructions.Add(Mnemonic.FSCSR, 1);
            pseudoInstructions.Add(Mnemonic.FRRM, 1);
            pseudoInstructions.Add(Mnemonic.FSRM, 1);
            pseudoInstructions.Add(Mnemonic.FSRMI, 1);
            pseudoInstructions.Add(Mnemonic.FRFLAGS, 1);
            pseudoInstructions.Add(Mnemonic.FSFLAGS, 1);
            pseudoInstructions.Add(Mnemonic.FSFLAGSI, 1);
        }

        public static bool Is(Mnemonic mnemonic)
        {
            return pseudoInstructions.ContainsKey(mnemonic);
        }

        public static uint Count(string mnemonic) => Count(mnemonic.ToEnum<Mnemonic>());
        public static uint Count(IToken token) => Count(token.Value.ToUpper());
        public static uint Count(Mnemonic mnemonic)
        {
            return pseudoInstructions.ContainsKey(mnemonic) ? pseudoInstructions[mnemonic] : 1;
        }

    }
}
