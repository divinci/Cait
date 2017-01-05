using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cait.Bitcoin.Net.Extensions.Tests
{
    [TestClass()]
    public class IntegerExtensionsTests
    {
        [TestMethod()]
        public void Byte_GetBytesVariableLength_Under_The_253_Threshold()
        {
            byte numberToTest = 139;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("8B", resultHexDump);
        }

        [TestMethod()]
        public void Byte_GetBytesVariableLength_For_Max_Byte()
        {
            byte numberToTest = byte.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-FF-00", resultHexDump);
        }

        [TestMethod()]
        public void Short_GetBytesVariableLength_For_Max_Short()
        {
            short numberToTest = short.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Int_GetBytesVariableLength_For_Max_Int()
        {
            int numberToTest = int.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FE-FF-FF-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Long_GetBytesVariableLength_For_Max_Long()
        {
            long numberToTest = long.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FF-FF-FF-FF-FF-FF-FF-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Short_GetBytesVariableLength()
        {
            short numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }

        [TestMethod()]
        public void Int_GetBytesVariableLength()
        {
            int numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }

        [TestMethod()]
        public void Long_GetBytesVariableLength()
        {
            long numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }
    }
}