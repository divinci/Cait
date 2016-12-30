using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cait.Bitcoin.Net.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cait.Bitcoin.Net.Constants.Tests
{
    [TestClass()]
    public class DefaultServicesTests
    {
        [TestMethod()]
        public void DefaultForVersionTest()
        {
            Service[] defaultServicesForv0_13_1 = DefaultServices.ForVersion(ProtocolVersion.v0_13_1);

            Assert.AreEqual(3, defaultServicesForv0_13_1.Length);

            Assert.AreEqual(Service.NODE_NETWORK, defaultServicesForv0_13_1[0]);
            Assert.AreEqual(Service.NODE_BLOOM, defaultServicesForv0_13_1[1]);
            Assert.AreEqual(Service.NODE_WITNESS, defaultServicesForv0_13_1[2]);
        }
    }
}