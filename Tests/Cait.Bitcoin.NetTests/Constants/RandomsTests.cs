using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cait.Bitcoin.Net.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cait.Bitcoin.Net.Constants.Tests
{
    [TestClass()]
    public class RandomsTests
    {
        [TestMethod()]
        public void Randoms_GenerateNewRandomNonce_SanityTest()
        {
            ulong result = Randoms.GenerateNewRandomNonce();
            Assert.IsInstanceOfType(result, typeof(ulong));
        }
    }
}