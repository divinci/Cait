using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cait.Bitcoin.Net.Constants.Tests
{
    [TestClass()]
    public class ServiceFlagsTests
    {
        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        public void ServiceFlags_ForVersion_Missing_Unit_Test_Dicovery()
        {
            bool passed = true;

            ProtocolVersion[] protocolVersions_With_ForVersion_Unit_Test = new ProtocolVersion[]
            {
                ProtocolVersion.v0_13_1
            };

            foreach (ProtocolVersion protocolVersion in Enum.GetValues(typeof(ProtocolVersion)).Cast<ProtocolVersion>())
            {
                if (protocolVersions_With_ForVersion_Unit_Test.Contains(protocolVersion))
                    continue;

                try
                {
                    ServiceFlags.ForVersion(protocolVersion);
                    passed = false;
                }
                catch { }
            }

            Assert.IsTrue(passed);
        }

        [TestMethod()]
        public void ServiceFlags_ForVersion_v0_13_1()
        {
            ServiceFlag[] defaultServicesForv0_13_1 = ServiceFlags.ForVersion(ProtocolVersion.v0_13_1);

            Assert.AreEqual(3, defaultServicesForv0_13_1.Length);

            Assert.AreEqual(ServiceFlag.NODE_NETWORK, defaultServicesForv0_13_1[0]);
            Assert.AreEqual(ServiceFlag.NODE_BLOOM, defaultServicesForv0_13_1[1]);
            Assert.AreEqual(ServiceFlag.NODE_WITNESS, defaultServicesForv0_13_1[2]);
        }
    }
}