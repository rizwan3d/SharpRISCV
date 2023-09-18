public static class Ext
{

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
