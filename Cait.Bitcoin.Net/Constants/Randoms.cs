using System;

namespace Cait.Bitcoin.Net.Constants
{
    public class Randoms
    {
        public static Random Random = new Random();

        public static ulong GenerateNewRandomNonce()
        {
            return (ulong)(Randoms.Random.NextDouble() * ulong.MaxValue);
        }
    }
}