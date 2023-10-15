using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRISCV.Core
{
    public class BytesConsoleOutputBuilder
    {
        public static string Build(List<byte> bytes)
        {
            int bytesPerLine = 16;
            int byteCount = 0;
            StringBuilder hexEditorOutput = new StringBuilder();
            hexEditorOutput.Append("            0  1  2  3  4  4  6  7  8  9  A  B  C  D  E  F").AppendLine();
            foreach (byte b in bytes)
            {
                if (byteCount % bytesPerLine == 0)
                {
                    if (byteCount > 0)
                    {
                        hexEditorOutput.Append("  ");
                        for (int i = 0; i < bytesPerLine; i++)
                        {
                            int index = byteCount - bytesPerLine + i;
                            if (index < bytes.Count)
                            {
                                hexEditorOutput.Append(bytes[index] >= 32 && bytes[index] <= 126 ? (char)bytes[index] : '.');
                            }
                        }
                        hexEditorOutput.AppendLine();
                    }

                    hexEditorOutput.AppendFormat("{0:X8}   ", byteCount);
                }

                hexEditorOutput.AppendFormat("{0:X2} ", b);

                byteCount++;
            }

            int remainingSpaces = bytesPerLine - (byteCount % bytesPerLine);
            hexEditorOutput.Append(' ', remainingSpaces * 3);

            hexEditorOutput.Append("  ");
            for (int i = 0; i < bytesPerLine; i++)
            {
                int index = byteCount - bytesPerLine + i;
                if (index < bytes.Count)
                {
                    hexEditorOutput.Append(bytes[index] >= 32 && bytes[index] <= 126 ? (char)bytes[index] : '.');
                }
            }

            return hexEditorOutput.ToString();
        }
    }
}