namespace SharpRISCV
{
    public static class Address
    {
        private static int currentAddress = 0;
        public static Dictionary<string, int> Labels = new Dictionary<string, int>();

        public static void Reset() => currentAddress = 0;

        public static int GetAndIncreseAddress()
        {
            int oldAddress = currentAddress;
            currentAddress += 4;
            return oldAddress;
        }

        public static int CurrentAddress { get { return currentAddress; } }

    }
}
