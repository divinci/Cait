using Cait.Bitcoin.Net.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cait.Bitcoin.Net.Extensions.Tests
{
    [TestClass()]
    public class ServiceFlagExtensionsTests
    {
        [TestMethod()]
        public void ServiceFlags_AsBitfield_SanityTest()
        {
            ServiceFlag[] services = new ServiceFlag[3] {
                ServiceFlag.NODE_NETWORK,
                ServiceFlag.NODE_BLOOM,
                ServiceFlag.NODE_WITNESS
            };

            int servicesBitField = services.AsBitfield();

            Assert.AreEqual(13, servicesBitField);
        }
    }
}