using Microsoft.VisualStudio.TestTools.UnitTesting;

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