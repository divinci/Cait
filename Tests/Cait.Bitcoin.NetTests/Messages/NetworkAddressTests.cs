using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Messages.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using static Cait.Bitcoin.NetTests.Helper;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    /// <summary>
    /// https://en.bitcoin.it/wiki/Protocol_documentation#addr
    /// </summary>

    [TestClass()]
    public class NetworkAddressTests
    {
        [TestMethod()]
        public void Build_And_Retrieve_NetworkAddress_Payload_Which_Includes_TimeStamp()
        {
            MessageHeader vackMessageHeader = new MessageHeader(Magic.TestNet, MessageType.Verack);

            NetworkAddressPayload networkAddress = new NetworkAddressPayload(
                new DateTime(2010, 12, 21, 2, 50, 10, DateTimeKind.Utc),
                Service.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            byte[] message = networkAddress.GetBytes(vackMessageHeader);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.SimpleHex(),
                @"
                    // Address:
                     E2 15 10 4D                                       // Mon Dec 20 21:50:10 EST 2010 (only when version is >= 31402)
                     01 00 00 00 00 00 00 00                           // 1 (NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01   // IPv4: 10.0.0.1, IPv6: ::ffff:10.0.0.1 (IPv4-mapped IPv6 address)
                     20 8D ".SimpleHex()
            );
        }

        [TestMethod()]
        public void Build_And_Retrieve_NetworkAddress_Payload_Which_Excludes_TimeStamp_Due_To_Message_Being_Version()
        {
            MessageHeader versionMessageHeader = new MessageHeader(Magic.TestNet, MessageType.Version);

            NetworkAddressPayload networkAddress = new NetworkAddressPayload(
                new DateTime(2010, 12, 21, 2, 50, 10, DateTimeKind.Utc),
                Service.NODE_NETWORK,
                new IPAddress(new byte[] { 10, 0, 0, 1 }),
                8333
                );

            byte[] message = networkAddress.GetBytes(versionMessageHeader);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.Replace("-", ""),
                    @"01 00 00 00 00 00 00 00
                      00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01
                      20 8D".Replace(Environment.NewLine, "").Replace(" ", ""));
        }

        [TestMethod()]
        public void Build_And_Retrieve_IPv4_NetworkAddress_Payload()
        {
            MessageHeader versionMessageHeader = new MessageHeader(Magic.TestNet, MessageType.Version);

            NetworkAddressPayload networkAddress = new NetworkAddressPayload(
                DateTime.Now,
                Service.NODE_NETWORK,
                new IPAddress(new byte[] { 01, 02, 03, 04 }),
                8333
                );

            byte[] message = networkAddress.GetBytes(versionMessageHeader);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.Replace("-", ""),
                    @"01 00 00 00 00 00 00 00
                      00 00 00 00 00 00 00 00 00 00 FF FF 01 02 03 04
                      20 8D".Replace(Environment.NewLine, "").Replace(" ", ""));
        }

        [TestMethod()]
        public void Build_And_Retrieve_IPv6_NetworkAddress_Payload()
        {
            MessageHeader versionMessageHeader = new MessageHeader(Magic.TestNet, MessageType.Version);

            NetworkAddressPayload networkAddress = new NetworkAddressPayload(
                DateTime.Now,
                Service.NODE_NETWORK,
                new IPAddress(new byte[] { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16 }),
                8333
                );

            byte[] message = networkAddress.GetBytes(versionMessageHeader);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.Replace("-", ""),
                    @"01 00 00 00 00 00 00 00
                      01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10
                      20 8D".Replace(Environment.NewLine, "").Replace(" ", ""));
        }
    }
}