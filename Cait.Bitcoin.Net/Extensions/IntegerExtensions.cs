using System;

namespace Cait.Bitcoin.Net.Extensions
{
    public static class IntegerExtensions
    {
        public static byte[] GetBytesVariableLength(this byte value)
        {
            return GetBytesVariableLength((long)value);
        }

        public static byte[] GetBytesVariableLength(this short value)
        {
            return GetBytesVariableLength((long)value);
        }

        public static byte[] GetBytesVariableLength(this int value)
        {
            return GetBytesVariableLength((long)value);
        }

        public static byte[] GetBytesVariableLength(this long value)
        {
            // Thanks guys https://bitcointalk.org/index.php?topic=32849.msg410480#msg410480

            if (value < 253)
                return new byte[1] { (byte)value };

            byte[] encoding = BitConverter.GetBytes(value);

            if (value <= short.MaxValue)
                return new byte[3] { 253, encoding[0], encoding[1] };

            if (value <= int.MaxValue)
                return new byte[5] { 254, encoding[0], encoding[1], encoding[2], encoding[3] };

            return new byte[9] { 255, encoding[0], encoding[1], encoding[2], encoding[3], encoding[4], encoding[5], encoding[6], encoding[7] };
        }
    }
}