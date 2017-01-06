using System;

namespace Cait.Core.Extensions
{
    public static class ByteArrayExtensions
    {
        public static bool ArrayEquals(this byte[] thisByteArray, byte[] comparison)
        {
            if (thisByteArray == null)
                throw new ArgumentNullException(nameof(thisByteArray));

            if (comparison == null)
                throw new ArgumentNullException(nameof(comparison));

            if (thisByteArray.Length != comparison.Length)
                return false;

            for (int i = 0; i < thisByteArray.Length; i++)
            {
                if (thisByteArray[i] != comparison[i])
                    return false;
            }

            return true;
        }
    }
}