using System;
using System.Text;

public static class Ext
{
    public static void AddRange<T>(this IList<T> source, IEnumerable<T> newList)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (newList == null)
        {
            throw new ArgumentNullException(nameof(newList));
        }

        if (source is List<T> concreteList)
        {
            concreteList.AddRange(newList);
            return;
        }

        foreach (var element in newList)
        {
            source.Add(element);
        }
    }
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

    public static string ToUpperFirstLetter(this string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;
        // convert to char array of the string
        char[] letters = source.ToCharArray();
        // upper case the first char
        letters[0] = char.ToUpper(letters[0]);
        // return the array made of the new char array
        return new string(letters);
    }

    public static IEnumerable<Byte> ToBytes(this uint value)
    {
        return BitConverter.GetBytes(value);
    }

    public static IEnumerable<Byte> ToBytes(this ulong value)
    {
        return BitConverter.GetBytes(value);
    }

    public static IEnumerable<Byte> ToBytes(this ushort value)
    {
        return BitConverter.GetBytes(value);
    }

    public static string Between(this string value, string a, string b) 
    {
        int pFrom = value.IndexOf(a) + a.Length;
        int pTo = value.LastIndexOf(b);

       return value.Substring(pFrom, pTo - pFrom);
    }
}
