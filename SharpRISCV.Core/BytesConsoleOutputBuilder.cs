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
            hexEditorOutput.Append(' ', remainingSpaces * 4 + (remainingSpaces > 0 ? 2 : 0));

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