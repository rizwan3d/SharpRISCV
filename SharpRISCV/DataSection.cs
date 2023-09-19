﻿namespace SharpRISCV
{
    public class DataSection
    {
        static List<byte> DataDirective = new List<byte>();

        public static void Add(byte[] byteArray)
        {
            //for (int i = 0; i < byteArray.Length; i += 2)
            //{
            //    byte[] set = new byte[4];

            //    // Copy up to 4 bytes from the input array to the set
            //    for (int j = 0; j < 4; j++)
            //    {
            //        if (i + j < byteArray.Length)
            //        {
            //            set[j] = byteArray[i + j];
            //        }
            //        else
            //        {
            //            set[j] = 0; // Fill with zeros if the end is reached
            //        }
            //    }

                DataDirective.AddRange(byteArray); // Add the set of bytes to the list
                DataDirective.Add(0);
            //}
        }

        public static Dictionary<string, string> HexDum 
        { 
            get
            {
                Dictionary<string, string> toReturn = new Dictionary<string, string>();                
                Address.SetAddress(Address.EntryDataAddress);
                byte[] byteArray = ReverseArrayInSetsOfFour(DataDirective.ToArray());
                string hexString = BitConverter.ToString(byteArray).Replace("-",""); // Convert to hexadecimal string
                foreach (string substring in hexString.SplitStringByLength(8))
                {
                    toReturn.Add(Address.GetAndIncreseAddress().ToString("X8"), substring.PadLeft(8, '0'));
                }
                return toReturn;
            } 
        }

        static byte[] ReverseArrayInSetsOfFour(byte[] byteArray)
        {
            int numSets = byteArray.Length / 4;
            byte[] reversedArray = new byte[byteArray.Length];

            for (int i = 0; i <= numSets; i++)
            {
                byte[] set = byteArray.Skip(i * 4).Take(4).ToArray();
                Array.Reverse(set); // Reverse the bytes within each set
                set.CopyTo(reversedArray, i * 4);
            }

            return reversedArray;
        }
    }
}
