using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpRISCV.Core.Hex
{
    public class Compile(string path)
    {
        public string BinaryWrite()
        {
            string hex = BuildHexString();
            File.WriteAllText(path, hex);
            return hex;
        }

        public string BuildHexString() {
            List<byte> finalBytes = new List<byte>();

            List<byte> opcodes = new List<byte>();
            foreach (var instruction in RiscVAssembler.Instruction)
            {
                if (instruction.InstructionType == InstructionType.Lable)
                    continue;
                instruction.MachineCode().ForEach(machineCode =>
                    opcodes.AddRange(BitConverter.GetBytes(machineCode.Decimal))
                ); ;
            }

            List<byte> dataSectBytes = new List<byte>();
            dataSectBytes.AddRange(DataSection.DataDirective);

            int textSectionSize = opcodes.Count * 4;
            int dataSectionStartAddress = textSectionSize;
            StringBuilder hexFileContent = new StringBuilder();
            string headerRecord = GenerateHeaderRecord(dataSectionStartAddress);
            hexFileContent.AppendLine(headerRecord);

            hexFileContent.Append(GenerateHexRecords(opcodes, 0x00000000));
            hexFileContent.Append(GenerateHexRecords(dataSectBytes, dataSectionStartAddress));

            hexFileContent.AppendLine(":00000001FF");

            return hexFileContent.ToString();
        }

        private string GenerateHexRecords(List<byte> data, int startAddress)
        {
            StringBuilder hexRecords = new StringBuilder();
            int recordSize = 16;

            for (int i = 0; i < data.Count; i += recordSize)
            {
                StringBuilder record = new StringBuilder();
                int byteCount = Math.Min(recordSize, data.Count - i);
                string addressHex = (startAddress + i).ToString("X4");
                record.AppendFormat(":{0:X2}{1:X4}00", byteCount, addressHex);

                for (int j = 0; j < byteCount; j++)
                {
                    record.AppendFormat("{0:X2}", data[i + j]);
                }

                byte[] bytes = Encoding.ASCII.GetBytes(record.ToString());
                int checksum = 0;
                for (int j = 1; j < bytes.Length; j += 2)
                    checksum += Convert.ToInt32(Encoding.ASCII.GetString(new[] { bytes[j], bytes[j + 1] }), 16);
                checksum &= 0xFF;
                record.AppendFormat("{0:X2}", (byte)(0x100 - checksum));

                hexRecords.AppendLine(record.ToString());
            }

            return hexRecords.ToString();
        }
        private string GenerateHeaderRecord(int dataSectionStartAddress)
        {
            int byteCount = 2;
            int recordTypeHex = 04;
            int data = 0000;
            string checksum = CalculateChecksum(byteCount, dataSectionStartAddress, recordTypeHex, data);

            // Header record format: :020000040000FA
            return $":{byteCount:X2}{dataSectionStartAddress:X4}{recordTypeHex:X2}{data:X4}{checksum}";
        }
        private string CalculateChecksum(params int[] fields)
        {
            int sum = 0;
            foreach (int field in fields)
            {
                sum += field;
            }

            byte checksum = (byte)(0x100 - (sum & 0xFF));

            return checksum.ToString("X2");
        }
    }
}
