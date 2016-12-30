using Cait.Core.Extensions;
using Cait.Bitcoin.Net.Messages.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cait.Bitcoin.NetTests;
using System;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    [TestClass()]
    public class MessageHeaderTests
    {
        [TestMethod()]
        public void Build_And_Retrieve_Verack_Payload_Against_Documented_Example()
        {
            MessageHeader testMessage = new MessageHeader(
                Constants.Magic.Main,
                Constants.MessageType.Verack);

            byte[] message = testMessage.GetBytes(MessagePayload.Empty);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.SimpleHex(),
                @"
                // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#verack
                // Message header:
                    F9 BE B4 D9                          // Main network magic bytes
                    76 65 72 61  63 6B 00 00 00 00 00 00 // 'verack' command
                    00 00 00 00                          // Payload is 0 bytes long
                    5D F6 E0 E2                          // Checksum".SimpleHex());
        }

        [TestMethod()]
        public void Build_And_Retrieve_Version_Payload_Against_Documented_Example()
        {
            MessageHeader testMessage = new MessageHeader(
                Constants.Magic.Main,
                Constants.MessageType.Version);

            string testPayload = @"
            // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#version
            // Version message:
                 62 EA 00 00                                                                    // 60002(protocol version 60002)
                 01 00 00 00 00 00 00 00                                                        // 1(NODE_NETWORK services)
                 11 B2 D0 50 00 00 00 00                                                        // Tue Dec 18 10:12:33 PST 2012
                 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Recipient address info -see Network Address
                 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00  // Sender address info -see Network Address
                 3B 2E B3 5D 8C E6 17 65                                                        // Node ID
                 0F 2F 53 61 74 6F 73 68 69 3A 30 2E 37 2E 32 2F                                // '/Satoshi:0.7.2/' sub - version string (string is 15 bytes long)
                 C0 3E 03 00                                                                    // Last block sending node has is block #212672".SimpleHex();

            MessagePayload messagePayload = new MessagePayload(testPayload.HexStringToByteArray());

            byte[] message = testMessage.GetBytes(messagePayload);
            string messageHexDump = BitConverter.ToString(message);

            Assert.AreEqual(
                messageHexDump.SimpleHex(),
                @"
                    // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#version
                    // Message Header
                    F9 BE B4 D9                             // Main network magic bytes
                    76 65 72 73 69 6F 6E 00 00 00 00 00     // 'version' command
                    64 00 00 00                             // Payload is 100 bytes long
                    3B 64 8D 5A                             // payload checksum".SimpleHex());
        }
    }
}