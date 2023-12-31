﻿using System;
using System.Collections.Generic;

namespace SharpRISCV.Core
{
    public static class Address
    {
        private static int currentAddress = 0;
        public static Dictionary<string, int> Labels = [];

        private static int InstructionSize = 4;

        public static void Reset() => currentAddress = 0;

        public static int EntryPoint
        {
            get
            {
                return Labels.ContainsKey("_start") ? Labels["_start"] : 0;
            }
        }
        public static string EntryPointHax { get { return $"0x{EntryPoint.ToString("X8")}"; } }

        public static int GetAndIncreseAddress()
        {
            int oldAddress = currentAddress;
            currentAddress += InstructionSize;
            return oldAddress;
        }

        public static int GetAndIncreseAddress(int by)
        {
            int oldAddress = currentAddress;
            currentAddress += by;
            return oldAddress;
        }

        public static int CurrentAddress { get { return currentAddress; } }

        public static int EntryDataAddress { get; private set; }
        public static int EntryBssAddress { get; private set; }

        public static void SetAddress(int address) => currentAddress = address;

        public static void StartDataAddress()
        {
            EntryDataAddress = currentAddress + InstructionSize;
        }

        public static void StartBssAddress()
        {
            EntryBssAddress = currentAddress;
        }

        internal static void Clear()
        {
            EntryDataAddress = 0;
            EntryBssAddress = 0;
            currentAddress = 0;
            EntryBssAddress = 0;
            Labels.Clear();
        }
    }
}
