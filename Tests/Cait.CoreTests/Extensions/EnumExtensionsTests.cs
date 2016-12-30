using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cait.Core.Extensions.Tests
{
    [TestClass()]
    public class EnumExtensionsTests
    {
        [Flags]
        private enum TestingInt32Enum
        {
            Clear = 0,
            A = 1,
            B = 2,
            C = 4,
            D = 8,
            E = 16
        }

        [TestMethod()]
        public void CreateFlagsBitfieldTest_SanityTest()
        {
            Enum[] testFlagEnums = new Enum[]
            {
                TestingInt32Enum.A, TestingInt32Enum.C, TestingInt32Enum.E
            };

            int bitwiseFlagField = testFlagEnums.CreateFlagsBitfield();

            string bitwiseFlagFieldBinary = Convert.ToString(bitwiseFlagField, 2);

            Assert.AreEqual("10101", bitwiseFlagFieldBinary);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateFlagsBitfieldTest_Null_Throws_Exception()
        {
            Enum[] testFlagEnums = null;
            int bitwiseFlagField = testFlagEnums.CreateFlagsBitfield();
        }

        [TestMethod()]
        public void CreateFlagsBitfieldTest_Empty_Returns_Zero()
        {
            Enum[] testFlagEnums = new Enum[]{};
            int bitwiseFlagField = testFlagEnums.CreateFlagsBitfield();
            Assert.AreEqual(0, bitwiseFlagField);
        }
    }
}