using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cait.Core.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void String_HexStringToByteArrayTest()
        {
            string hexString = "019E3779B97F4A";
            byte[] result = hexString.HexStringToByteArray();
            Assert.IsTrue(new byte[]
            {
                1,
                158,
                55,
                121,
                185,
                127,
                74
            }.ArrayEquals(result));
        }
    }
}