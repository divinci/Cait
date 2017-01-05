using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.NetTests;
using Cait.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    [TestClass()]
    public class VersionMessageTests
    {
        [TestMethod()]
        public void VersionMessage_Build()
        {
            DateTime timeStamp = new DateTime(2012, 12, 18, 10, 12, 33, DateTimeKind.Utc).AddHours(8);

            Message versionMessage = new Message(Magic.Main, MessageType.Version);

            VersionMessage versionMessagePayload = new VersionMessage(
                ProtocolVersion.MEMPOOL_GD_VERSION,
                ServiceFlag.NODE_NETWORK,
                timeStamp,
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),
                7284544412836900411,
                "/Satoshi:0.7.2/",
                212672,
                true);

            byte[] result = versionMessage.GetBytes(versionMessagePayload);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                @"
                    // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#version
                    
                    // Message Header:
                     F9 BE B4 D9                                // Main network magic bytes
                     76 65 72 73 69 6F 6E 00 00 00 00 00        // 'version' command
                     64 00 00 00                                // Payload is 100 bytes long
                     3B 64 8D 5A                                // payload checksum

                    // Version message:
                     62 EA 00 00                                                                    // 60002(protocol version 60002)
                     01 00 00 00 00 00 00 00                                                        // 1 (NODE_NETWORK services)
                     11 B2 D0 50 00 00 00 00                                                        // Tue Dec 18 10:12:33 PST 2012
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Recipient address info - see Network Address
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Sender address info - see Network Address
                     3B 2E B3 5D 8C E6 17 65                                                        // Node ID
                     0F 2F 53 61 74 6F 73 68 69 3A 30 2E 37 2E 32 2F                                // '/Satoshi:0.7.2/' sub - version string(string is 15 bytes long)
                     C0 3E 03 00                                                                    // Last block sending node has is block #212672"
                        .AsSimpleHexString()
            );
        }

        [TestMethod()]
        public void VersionMessage_Build_Payload()
        {
            DateTime timeStamp = new DateTime(2012, 12, 18, 10, 12, 33, DateTimeKind.Utc).AddHours(8);

            Message versionMessage = new Message(Magic.Main, MessageType.Version);

            VersionMessage versionMessagePayload = new VersionMessage(
                ProtocolVersion.MEMPOOL_GD_VERSION,
                ServiceFlag.NODE_NETWORK,
                timeStamp,
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),
                7284544412836900411,
                "/Satoshi:0.7.2/",
                212672,
                true);

            byte[] result = versionMessagePayload.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                @"
                    // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#version
                    
                    // Message Header:
                    // F9 BE B4 D9                                // Main network magic bytes
                    // 76 65 72 73 69 6F 6E 00 00 00 00 00        // 'version' command
                    // 64 00 00 00                                // Payload is 100 bytes long
                    // 3B 64 8D 5A                                // payload checksum

                    // Version message:
                     62 EA 00 00                                                                    // 60002(protocol version 60002)
                     01 00 00 00 00 00 00 00                                                        // 1 (NODE_NETWORK services)
                     11 B2 D0 50 00 00 00 00                                                        // Tue Dec 18 10:12:33 PST 2012
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Recipient address info - see Network Address
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Sender address info - see Network Address
                     3B 2E B3 5D 8C E6 17 65                                                        // Node ID
                     0F 2F 53 61 74 6F 73 68 69 3A 30 2E 37 2E 32 2F                                // '/Satoshi:0.7.2/' sub - version string(string is 15 bytes long)
                     C0 3E 03 00                                                                    // Last block sending node has is block #212672"
                        .AsSimpleHexString()
            );
        }

        [TestMethod()]
        public void VersionMessage_Build_Payload_Containing_Relay_Flag_If_Protocol_Greater_70001()
        {
            DateTime timeStamp = new DateTime(2012, 12, 18, 10, 12, 33, DateTimeKind.Utc).AddHours(8);

            Message versionMessage = new Message(Magic.Main, MessageType.Version);

            VersionMessage versionMessagePayload = new VersionMessage(
                ProtocolVersion.v0_10_0,
                ServiceFlag.NODE_NETWORK,
                timeStamp,
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),
                new NetworkAddress(ServiceFlag.NODE_NETWORK, IPAddress.Any, 0),7284544412836900411,
                "/Satoshi:0.7.2/",
                212672,
                true);

            byte[] result = versionMessagePayload.GetBytes(versionMessage);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                @"
                    // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#version
                    
                    // Message Header:
                    // F9 BE B4 D9                                // Main network magic bytes
                    // 76 65 72 73 69 6F 6E 00 00 00 00 00        // 'version' command
                    // 64 00 00 00                                // Payload is 100 bytes long
                    // 3B 64 8D 5A                                // payload checksum

                    // Version message:
                     71 11 01 00                                                                    // 70001 (protocol version 60002)
                     01 00 00 00 00 00 00 00                                                        // 1 (NODE_NETWORK services)
                     11 B2 D0 50 00 00 00 00                                                        // Tue Dec 18 10:12:33 PST 2012
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Recipient address info - see Network Address
                     01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Sender address info - see Network Address
                     3B 2E B3 5D 8C E6 17 65                                                        // Node ID
                     0F 2F 53 61 74 6F 73 68 69 3A 30 2E 37 2E 32 2F                                // '/Satoshi:0.7.2/' sub - version string(string is 15 bytes long)
                     C0 3E 03 00                                                                    // Last block sending node has is block #212672
                     01                                                                             // Relay"
                        .AsSimpleHexString()
            );
        }

        [TestMethod]
        public void VersionMessage_Constructor_A()
        {
            DateTime timeStamp = DateTime.Now;
            VersionMessage versionMessagePayload = new VersionMessage(
                ProtocolVersion.v0_10_0,
                ServiceFlag.NODE_GETUTXO,
                timeStamp,
                new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 1),
                new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 2),
                7284544412836900421,
                "/Satoshi:0.7.2/",
                212672,
                true);

            Assert.AreEqual(ProtocolVersion.v0_10_0, versionMessagePayload.ProtocolVersion);
            Assert.AreEqual(versionMessagePayload.Services.Length, 1);
            Assert.AreEqual(ServiceFlag.NODE_GETUTXO, versionMessagePayload.Services[0]);
            Assert.AreEqual(timeStamp, versionMessagePayload.TimeStamp);
            Assert.AreEqual(new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 1), versionMessagePayload.RemoteNetworkAddress);
            Assert.AreEqual(new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 2), versionMessagePayload.LocalNetworkAddress);
            Assert.AreEqual((ulong)7284544412836900421, versionMessagePayload.Nonce);
            Assert.AreEqual("/Satoshi:0.7.2/", versionMessagePayload.UserAgent);
            Assert.AreEqual(212672, versionMessagePayload.StartHeight);
            Assert.AreEqual(true, versionMessagePayload.Relay);
        }

        [TestMethod]
        public void VersionMessage_Constructor_B()
        {
            DateTime timeStamp = DateTime.Now;
            VersionMessage versionMessagePayload = new VersionMessage(
                ProtocolVersion.v0_10_0,
                new ServiceFlag[] { ServiceFlag.NODE_XTHIN },
                timeStamp,
                new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 1),
                new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 2),
                7284544412836900411,
                "/Satoshi:0.7.2/",
                212672,
                true);

            Assert.AreEqual(ProtocolVersion.v0_10_0, versionMessagePayload.ProtocolVersion);
            Assert.AreEqual(versionMessagePayload.Services.Length, 1);
            Assert.AreEqual(ServiceFlag.NODE_XTHIN, versionMessagePayload.Services[0]);
            Assert.AreEqual(timeStamp, versionMessagePayload.TimeStamp);
            Assert.AreEqual(new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 1), versionMessagePayload.RemoteNetworkAddress);
            Assert.AreEqual(new NetworkAddress(timeStamp.AddHours(4), ServiceFlag.NODE_NETWORK, IPAddress.Any, 2), versionMessagePayload.LocalNetworkAddress);
            Assert.AreEqual((ulong)7284544412836900411, versionMessagePayload.Nonce);
            Assert.AreEqual("/Satoshi:0.7.2/", versionMessagePayload.UserAgent);
            Assert.AreEqual(212672, versionMessagePayload.StartHeight);
            Assert.AreEqual(true, versionMessagePayload.Relay);
        }

        #region IDatagramHeader.GetBytes argument tests

        [TestMethod]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VersionMessage_GetBytes_IDatagramHeader_Argument_Can_Not_Be_Null()
        {
            VersionMessage versionMessage = new VersionMessage(
                ProtocolVersion.MEMPOOL_GD_VERSION,
                ServiceFlag.NODE_NETWORK,
                DateTime.Now,
                new NetworkAddress(ServiceFlag.NODE_NONE, IPAddress.Any, 0),
                new NetworkAddress(ServiceFlag.NODE_NONE, IPAddress.Any, 0),
                2024707532345282954,
                "Satoshi:0.7.2",
                212672,
                true);

            versionMessage.GetBytes(null);
            Assert.Fail("Expecting exception");
        }

        [TestMethod]
        [ExcludeFromCodeCoverage()]
        [ExpectedException(typeof(ArgumentException))]
        public void VersionMessage_GetBytes_IDatagramHeader_Argument_Must_Be_Of_Type_MessageHeader()
        {
            VersionMessage versionMessage = new VersionMessage(
                ProtocolVersion.MEMPOOL_GD_VERSION,
                ServiceFlag.NODE_NETWORK,
                DateTime.Now,
                new NetworkAddress(ServiceFlag.NODE_NONE, IPAddress.Any, 0),
                new NetworkAddress(ServiceFlag.NODE_NONE, IPAddress.Any, 0),
                2024707532345282954,
                "/Satoshi:0.7.2/",
                212672,
                true);

            versionMessage.GetBytes(new DummyIDatagramHeader());
            Assert.Fail("Expecting exception");
        }

        #endregion IDatagramHeader.GetBytes argument tests
    }
}