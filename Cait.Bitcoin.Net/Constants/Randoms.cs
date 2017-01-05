using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
