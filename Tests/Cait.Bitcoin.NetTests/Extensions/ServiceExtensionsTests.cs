using Cait.Bitcoin.Net.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cait.Bitcoin.Net.Extensions.Tests
{
    [TestClass()]
    public class ServiceExtensionsTests
    {
        [TestMethod()]
        public void AsBitfield_SanityTest()
        {
            Service[] services = new Service[3] {
                Service.NODE_NETWORK,
                Service.NODE_BLOOM,
                Service.NODE_WITNESS
            };

            int servicesBitField = services.AsBitfield();

            Assert.AreEqual(13, servicesBitField);
        }
    }
}