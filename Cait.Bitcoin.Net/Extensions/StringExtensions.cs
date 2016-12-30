using Cait.Bitcoin.Net.Constants;
using System;

namespace Cait.Bitcoin.Net.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytesVariableLength(this string value)
        {
            byte[] valueBytes = Encodings.DefaultStringEncoding.GetBytes(value);
            byte[] valueLengthAsVariable = value.Length.GetBytesVariableLength();

            byte[] returnValue = new byte[valueLengthAsVariable.Length + valueBytes.Length];
            Array.Copy(valueLengthAsVariable, returnValue, valueLengthAsVariable.Length);
            Array.Copy(valueBytes, 0, returnValue, valueLengthAsVariable.Length, valueBytes.Length);

            return returnValue;
        }
    }
}