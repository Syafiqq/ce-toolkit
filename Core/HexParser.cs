using System;
using Org.BouncyCastle.Utilities.Encoders;

namespace Core
{
    public static class HexParser
    {
        public static long HexStringToLong(string hex)
        {
            var s = '+';
            if (hex[0] == '-' || hex[0] == '+')
            {
                s = hex[0];
                hex = hex.Remove(0, 1);
            }

            var y = Hex.Decode($"{"".PadRight(16 - hex.Length, '0')}{hex}");
            if (BitConverter.IsLittleEndian)
                Array.Reverse(y);
            var y1 = BitConverter.ToInt64(y, 0);
            if (s == '-')
                y1 = 0 - y1;
            return y1;
        }
    }
}