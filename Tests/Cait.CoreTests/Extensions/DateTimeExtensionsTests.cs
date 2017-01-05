using Cait.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cait.Core.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void DateTime_DateTimeToUnixTimestamp_SanityCheck()
        {
            /*
             Example taken from https://en.wikipedia.org/wiki/Unix_time
            */

            double expected = 1482098738;
            DateTime input = new DateTime(2016, 12, 18, 22, 05, 38);

            double result = input.AsUnixTimestamp();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Int_FromUnixTimestampTest_SanityCheck()
        {
            int input = 1482098738;
            DateTime expected = new DateTime(2016, 12, 18, 22, 05, 38);

            DateTime result = input.FromUnixTimestamp();
            Assert.AreEqual(expected, result);
        }
    }
}