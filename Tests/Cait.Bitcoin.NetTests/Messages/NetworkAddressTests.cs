using Cait.Bitcoin.Net.Messages;
using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.NetTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using static Cait.Bitcoin.NetTests.Helper;
using System.Diagnostics.CodeAnalysis;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    /// <summary>
    /// https://en.bitcoin.it/wiki/Protocol_documentation#addr
    /// </summary>

    [TestClass()]
    public class NetworkAddressTests
    {
        [TestMethod()]
        public void NetworkAddress_Build_Payload_Which_Includes_TimeStamp()
        {
            Message vackMessage = new Message(Magic.TestNet, MessageType.Verack);

            NetworkAddress networkAddress = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(5),
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            byte[] result = networkAddress.GetBytes(vackMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                @"
                    // Address:
                     E2 15 10 4D                                       // Mon Dec 20 21:50:10 EST 2010 (only when version is >= 31402)
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01   // IPv4: 10.0.0.1, IPv6: ::ffff:10.0.0.1 (IPv4-mapped IPv6 address)
                     20 8D ".AsSimpleHexString()
            );
        }

        [TestMethod()]
        public void NetworkAddress_Build_Payload_Which_Excludes_TimeStamp_Due_To_Message_Being_Version()
        {
            Message versionMessage = new Message(Magic.TestNet, MessageType.Version);

            NetworkAddress networkAddress = new NetworkAddress(
                new DateTime(2010, 12, 21, 2, 50, 10, DateTimeKind.Utc),
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            byte[] result = networkAddress.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                    @"
                    // Address:
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01   // IPv4: 10.0.0.1, IPv6: ::ffff:10.0.0.1 (IPv4-mapped IPv6 address)
                     20 8D".AsSimpleHexString()
                );
        }

        [TestMethod()]
        public void NetworkAddress_Build_Payload_Unroutable_Network_Address_A()
        {
            Message versionMessage = new Message(Magic.TestNet, MessageType.Version);

            NetworkAddress networkAddress = NetworkAddress.Unroutable(ServiceFlag.NODE_NETWORK);

            byte[] result = networkAddress.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                    @"
                    // Address:
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00   // IPv4: 0.0.0.0, IPv6: ::ffff:0.0.0.0 (IPv4-mapped IPv6 address)
                     00 00                                             // Port 0".AsSimpleHexString()
                );
        }

        [TestMethod()]
        public void NetworkAddress_Build_Payload_Unroutable_Network_Address_B()
        {
            Message versionMessage = new Message(Magic.TestNet, MessageType.Version);

            NetworkAddress networkAddress = NetworkAddress.Unroutable(new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_WITNESS });

            byte[] result = networkAddress.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                    @"
                    // Address:
                     09 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00   // IPv4: 0.0.0.0, IPv6: ::ffff:0.0.0.0 (IPv4-mapped IPv6 address)
                     00 00                                             // Port 0".AsSimpleHexString()
                );
        }

        [TestMethod()]
        public void NetworkAddress_Build_Payload_MessageIPv4()
        {
            Message versionMessage = new Message(Magic.TestNet, MessageType.Version);

            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 1, 2, 3, 4, }),
                8333
                );

            byte[] result = networkAddress.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                    @"
                    // Address:
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 01 02 03 04   // IPv4: 1.2.3.4, IPv6: ::ffff:1.2.3.4 (IPv4-mapped IPv6 address)
                     20 8D".AsSimpleHexString()
                );
        }

        [TestMethod()]
        public void NetworkAddress_Build_Version_MessageIPv6()
        {
            Message versionMessage = new Message(Magic.TestNet, MessageType.Version);

            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }),
                8333
                );

            byte[] result = networkAddress.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                    @"
                    // Address:
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10   // IPv6: 1:2:3:4:5:6:7:8:9:10:11:12:14:15:16
                     20 8D".AsSimpleHexString()
                );
        }

        #region IDatagramHeader.GetBytes argument tests

        [TestMethod]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_GetBytes_IDatagramHeader_Argument_Can_Not_Be_Null()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                new DateTime(2010, 12, 21, 2, 50, 10, DateTimeKind.Utc),
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            networkAddress.GetBytes(null);

            Assert.Fail("Expecting exception");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentException))]
        public void NetworkAddress_GetBytes_IDatagramHeader_Argument_Must_Be_Of_Type_MessageHeader()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                new DateTime(2010, 12, 21, 2, 50, 10, DateTimeKind.Utc),
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            networkAddress.GetBytes(new DummyIDatagramHeader());

            Assert.Fail("Expecting exception");
        }

        #endregion IDatagramHeader.GetBytes argument tests

        #region Constructor tests

        [TestMethod()]
        public void NetworkAddress_Constructor_A()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
        }

        [TestMethod()]
        public void NetworkAddress_Constructor_B()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK },
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
        }

        [TestMethod()]
        public void NetworkAddress_Constructor_C()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                ServiceFlag.NODE_NETWORK,
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
        }

        [TestMethod()]
        public void NetworkAddress_Constructor_D()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK },
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
        }

        #endregion Constructor tests

        #region Constructor null exception tests

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_A_ArgumentNullException_A()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                ServiceFlag.NODE_NETWORK,
                null,
                8333
                );
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_B_ArgumentNullException_A()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                null,
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_B_ArgumentNullException_B()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK },
                null,
                8333
                );
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_C_ArgumentNullException_A()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                ServiceFlag.NODE_NETWORK,
                null,
                8333
                );
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_D_ArgumentNullException_A()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                null,
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NetworkAddress_Constructor_D_ArgumentNullException_B()
        {
            NetworkAddress networkAddress = new NetworkAddress(
                DateTime.Now,
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK },
                null,
                8333
                );
            Assert.Fail("Expecting exception");
        }

        #endregion Constructor null exception tests

        #region Equals and HashCode overide

        [TestMethod()]
        public void NetworkAddress_Equals()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_A()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 11, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_B()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO, ServiceFlag.NODE_BLOOM },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_C()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO, ServiceFlag.NODE_BLOOM },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_D()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 2 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_E()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8332
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.IsFalse(a.Equals(b));    
        }

        [TestMethod()]
        public void NetworkAddress_Equals_Fails_F()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8332
                );
            NetworkAddress b = null;

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod()]
        public void NetworkAddress_GetHashCode()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 10 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 10 }),
                8333
                );

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod()]
        public void NetworkAddress_GetHashCode_Fails_A()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 11, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod()]
        public void NetworkAddress_GetHashCode_Fails_B()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO, ServiceFlag.NODE_BLOOM },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod()]
        public void NetworkAddress_GetHashCode_Fails_C()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 2 }),
                8333
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod()]
        public void NetworkAddress_GetHashCode_Fails_D()
        {
            NetworkAddress a = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8332
                );
            NetworkAddress b = new NetworkAddress(
                new DateTime(2010, 12, 20, 21, 50, 10, DateTimeKind.Utc).AddHours(9),
                new ServiceFlag[] { ServiceFlag.NODE_NETWORK, ServiceFlag.NODE_GETUTXO },
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        #endregion Equals and HashCode overide
    }
}