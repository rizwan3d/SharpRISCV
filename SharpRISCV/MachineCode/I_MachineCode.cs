
using System;
using System.Collections.Generic;

namespace SharpRISCV.MachineCode
{
    class I_MachineCode
    {
        //I type: .insn i opcode6, func3, rd, simm12(rs1)
        //+--------------+-----+-------+----+---------+
        //| simm12[11:0] | rs1 | func3 | rd | opcode6 |
        //+--------------+-----+-------+----+---------+
        //31             20    15      12   7         0

        public List<MachineCode> Generate(RiscVInstruction instruction)
        {
            List <MachineCode> machines = new List < MachineCode >();
            int value;
            if (Address.Labels.TryGetValue(instruction.Immediate, out int labelLineNumber))
            {
                value = Address.Labels[instruction.Immediate] - Address.CurrentAddress;
            }
            else if (int.TryParse(instruction.Immediate, out value))
            {
                // Operand is a numeric value
            }
            else
            if (!Address.Labels.ContainsKey(instruction.Label)) throw new Exception("Invalid Lable");

            string opcode = ((int)instruction.OpcodeBin).ToBinary(7);
            string rdBinary = Convert.ToString(int.Parse(instruction.Rd.Substring(1)), 2).PadLeft(5, '0');
            string func3 = ((int)instruction.Funct3).ToBinary(3);
            string rs1Binary = Convert.ToString(int.Parse(instruction.Rs1.Substring(1)), 2).PadLeft(5, '0');
            string imm = value.ToBinary(11);
            string imm2 = "";
            if (imm.Length >= 12)
            {
                imm2 = imm.Substring(0, imm.Length - 12);
                imm = imm.Substring(imm.Length - 12);
            }

            if (instruction.Opcode.Equals("la"))
            {
                if(string.IsNullOrEmpty(imm2)) imm2 = "0";
                machines.Add(new U_MachineCode().Generate(new RiscVInstruction { 
                    Instruction = $"auipc {instruction.Rd}, {imm2}",
                    InstructionType = InstructionType.U,
                    Opcode = "auipc",
                    Immediate = (Convert.ToInt32(imm2,2)+1).ToString() ,
                    Rd = instruction.Rd
                }));
            }

            machines.Add(new MachineCode($"{imm}{rs1Binary}{func3}{rdBinary}{opcode}", instruction.Instruction));

            return machines;
        }
    }
}
