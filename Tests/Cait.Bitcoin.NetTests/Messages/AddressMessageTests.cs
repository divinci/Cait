using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.NetTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage()]
    public class AddressMessageTests
    {
        [TestMethod()]
        public void AddressMessage_ConstructorTest_A()
        {
            AddressMessagePayload addressMessage = new AddressMessagePayload(
                new List<NetworkAddress>());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddressMessage_ConstructorTest_A_ArgumentNullException_A()
        {
            AddressMessagePayload addressMessage = new AddressMessagePayload(null);
        }

        [TestMethod()]
        public void AddressMessage_Build()
        {
            Message addressMessage = new Message(Constants.Magic.Main, Constants.MessageType.Addr);

            List<NetworkAddress> networkAddresses = new List<NetworkAddress>();
            networkAddresses.Add(
                new NetworkAddress(
                    new DateTime(2010, 12, 20, 21, 50, 10).AddHours(5),
                    Constants.ServiceFlag.NODE_NETWORK,
                    new IPAddress(new byte[] { 10, 0, 0, 1 }),
                    8333));

            AddressMessagePayload addressMessagePayload = new AddressMessagePayload(networkAddresses);

            byte[] result = addressMessage.GetBytes(addressMessagePayload);

            Assert.AreEqual(
                @"
                    // Message Header:
                     F9 BE B4 D9                                        // Main network magic bytes
                     61 64 64 72  00 00 00 00 00 00 00 00               // 'addr'
                     1F 00 00 00                                        // payload is 31 bytes long
                     ED 52 39 9B                                        // checksum of payload

                    // Payload:
                     01                                                 // 1 address in this message

                    // Address:
                     E2 15 10 4D                                        // Mon Dec 20 21:50:10 EST 2010(only when version is >= 31402)
                     01 00 00 00 00 00 00 00                            // 1(NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01    // IPv4: 10.0.0.1, IPv6: ::ffff:10.0.0.1(IPv4 - mapped IPv6 address)
                     20 8D                                              // port 8333".AsSimpleHexString(),
                result.AsSimpleHexString());
        }

        [TestMethod()]
        public void AddressMessage_Build_Payload()
        {
            Message addressMessage = new Message(Constants.Magic.Main, Constants.MessageType.Addr);

            List<NetworkAddress> networkAddresses = new List<NetworkAddress>();
            networkAddresses.Add(
                new NetworkAddress(
                    new DateTime(2010, 12, 20, 21, 50, 10).AddHours(5),
                    Constants.ServiceFlag.NODE_NETWORK,
                    new IPAddress(new byte[] { 10, 0, 0, 1 }),
                    8333));

            AddressMessagePayload addressMessagePayload = new AddressMessagePayload(networkAddresses);

            byte[] result = addressMessagePayload.GetBytes(addressMessage);

            Assert.AreEqual(
                @"
                    // Message Header:
                  // F9 BE B4 D9                                        // Main network magic bytes
                  // 61 64 64 72  00 00 00 00 00 00 00 00               // 'addr'
                  // 1F 00 00 00                                        // payload is 31 bytes long
                  // ED 52 39 9B                                        // checksum of payload

                    // Payload:
                     01                                                 // 1 address in this message

                    // Address:
                     E2 15 10 4D                                        // Mon Dec 20 21:50:10 EST 2010(only when version is >= 31402)
                     01 00 00 00 00 00 00 00                            // 1(NODE_NETWORK service - see version message)
                     00 00 00 00 00 00 00 00 00 00 FF FF 0A 00 00 01    // IPv4: 10.0.0.1, IPv6: ::ffff:10.0.0.1(IPv4 - mapped IPv6 address)
                     20 8D                                              // port 8333".AsSimpleHexString(),
                result.AsSimpleHexString());
        }
    }
}