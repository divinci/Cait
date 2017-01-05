using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cait.Bitcoin.Net.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void String_GetBytesVariableLength_SanityTest()
        {
            string valueToTest = "/Satoshi:0.7.2/";
            byte[] result = valueToTest.GetBytesVariableLength();

            string hex = BitConverter.ToString(result);

            Assert.AreEqual(
                hex.Replace("-", ""),
                    @"0F 2F 53 61 74 6F 73 68 69 3A 30 2E 37 2E 32 2F".Replace(Environment.NewLine, "").Replace(" ", ""));
        }
    }
}