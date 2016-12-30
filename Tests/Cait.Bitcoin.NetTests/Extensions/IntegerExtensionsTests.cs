using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cait.Bitcoin.Net.Extensions.Tests
{
    [TestClass()]
    public class IntegerExtensionsTests
    {
        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_From_A_Byte_Under_The_253_Threshold()
        {
            byte numberToTest = 139;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("8B", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_For_Max_Byte()
        {
            byte numberToTest = byte.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-FF-00", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_For_Max_Short()
        {
            short numberToTest = short.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_For_Max_Int()
        {
            int numberToTest = int.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FE-FF-FF-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_For_Max_Long()
        {
            long numberToTest = long.MaxValue;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FF-FF-FF-FF-FF-FF-FF-FF-7F", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_From_A_Short()
        {
            short numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_From_A_Int32()
        {
            int numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }

        [TestMethod()]
        public void Get_A_Variable_Length_Byte_Array_From_A_Int64()
        {
            long numberToTest = 500;
            byte[] result = numberToTest.GetBytesVariableLength();
            string resultHexDump = BitConverter.ToString(result);
            Assert.AreEqual("FD-F4-01", resultHexDump);
        }
    }
}