using System;
using System.Collections.Generic;
using System.Text;

public static class Ext
{
    public static string HexToString(this string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException("Hexadecimal string length must be even.");
        }

        byte[] bytes = new byte[hexString.Length / 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            string hexByte = hexString.Substring(i * 2, 2);

            if (hexByte == "00")
            {
                bytes[i] = (byte)'.';
            }
            else
            {
                bytes[i] = Convert.ToByte(hexByte, 16);
            }
        }

        return Encoding.UTF8.GetString(bytes);
    }
    public static List<string> SplitStringByLength(this string input, int length)
    {
        List<string> substrings = [];

        for (int i = 0; i < input.Length; i += length)
        {
            int remainingLength = Math.Min(length, input.Length - i);
            substrings.Add(input.Substring(i, remainingLength));
        }

        return substrings;
    }
    public static string[] SplitStingByNewLine(this string str)
    {
        // Split the input string into lines based on "\r\n" and "\n" and remove empty lines.
        return str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }
    public static string ToBinary(this int number, int bitsLength = 32)
    {
        return NumberToBinary(number, bitsLength);
    }

    public static string NumberToBinary(int number, int bitsLength = 32)
    {
        string result = Convert.ToString(number, 2).PadLeft(bitsLength, '0');

        return result;
    }

    public static int FromBinaryToInt(this string binary)
    {
        return BinaryToInt(binary);
    }

    public static int BinaryToInt(string binary)
    {
        return Convert.ToInt32(binary, 2);
    }

    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

}
