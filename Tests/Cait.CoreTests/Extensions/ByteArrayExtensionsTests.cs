using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Cait.Core.Extensions.Tests
{
    [TestClass()]
    public class ByteArrayExtensionsTests
    {
        [TestMethod()]
        public void ByteArray_ArrayEquals_SanityCheck_A()
        {
            byte[] a = new byte[] { 123, 2, 6, 5, 3, 55, 46 };
            byte[] b = new byte[] { 123, 2, 6, 5, 3, 55, 46 };

            Assert.IsTrue(a.ArrayEquals(b));
        }

        [TestMethod()]
        public void ByteArray_ArrayEquals_SanityCheck_B()
        {
            byte[] a = new byte[] { 123, 2, 6, 5, 3, 55, 46 };
            byte[] b = new byte[] { 123, 2, 6, 5, 3, 55 };

            Assert.IsFalse(a.ArrayEquals(b));
        }

        [TestMethod()]
        public void ByteArray_ArrayEquals_SanityCheck_C()
        {
            byte[] a = new byte[] { 123, 2, 6, 5, 3, 55, 46 };
            byte[] b = new byte[] { 123, 2, 6, 5, 3, 55, 47 };

            Assert.IsFalse(a.ArrayEquals(b));
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ByteArray_ArrayEquals_ArgumentNullException_A()
        {
            byte[] a = null;
            byte[] b = new byte[] { 123, 2, 6, 5, 3, 55, 46 };

            bool result = a.ArrayEquals(b);

            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ByteArray_ArrayEquals_ArgumentNullException_B()
        {
            byte[] a = new byte[] { 123, 2, 6, 5, 3, 55, 46 };
            byte[] b = null;

            bool result = a.ArrayEquals(b);

            Assert.Fail("Expecting exception");
        }
    }
}