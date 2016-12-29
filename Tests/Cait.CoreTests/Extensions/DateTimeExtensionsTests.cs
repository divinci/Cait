using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cait.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cait.Core.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void DateTimeToUnixTimestampTest_SanityCheck()
        {
            /*
             Example taken from https://en.wikipedia.org/wiki/Unix_time
            */

            double expected = 1482098738;
            DateTime input = new DateTime(2016, 12, 18, 22, 05, 38);

            double result = input.DateTimeToUnixTimestamp();
            Assert.AreEqual(expected, result);
        }
    }
}