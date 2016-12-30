using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.NetTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    [TestClass()]
    public class VersionMessageTests
    {
        [TestMethod()]
        public void GetBytesTest()
        {
            /* Wire capture with 0.13.1 @ 2016-12-30 for MainNet

            Frame 50235: 192 bytes on wire (1536 bits), 192 bytes captured (1536 bits) on interface 0
            Ethernet II, Src: Microsof_00:09:20 (00:22:48:00:09:20), Dst: 12:34:56:78:9a:bc (12:34:56:78:9a:bc)
            Internet Protocol Version 4, Src: 10.1.2.4, Dst: 208.89.206.2
            Transmission Control Protocol, Src Port: 8333, Dst Port: 38589, Seq: 1, Ack: 124, Len: 126
            Bitcoin protocol
                Packet magic: 0xf9beb4d9
                Command name: version
                Payload Length: 102
                Payload checksum: 0x946e269f
                Version message
                    Protocol version: 70014
                    Node services: 0x000000000000000d
                        .... .... .... .... .... .... .... ...1 = Network node: Set
                    Node timestamp: Dec 30, 2016 16:53:37.000000000 Coordinated Universal Time
                    Address as receiving node
                        Node services: 0x0000000000000000
                            .... .... .... .... .... .... .... ...0 = Network node: Not set
                        Node address: ::ffff:208.89.206.2
                        Node port: 38589
                    Address of emmitting node
                        Node services: 0x000000000000000d
                            .... .... .... .... .... .... .... ...1 = Network node: Set
                        Node address: ::
                        Node port: 0
                    Random nonce: 0x1c1934c5f77f918a
                    User agent
                        Count: 16
                        String value: /Satoshi:0.13.1/
                    Block start height: 378478
                    Relay flag: 1

            =

            f9beb4d976657273696f6e000000000066000000946e269f7e1101000d000000000000001191665800000000000000000000000000000000000000000000ffffd059ce0296bd0d000000000000000000000000000000000000000000000000008a917ff7c534191c102f5361746f7368693a302e31332e312f6ec6050001

            */
            DateTime timeStamp = new DateTime(2016, 12, 30, 16, 53, 37, DateTimeKind.Utc);
            MessageHeader versionMessageHeader = new MessageHeader(Magic.Main, MessageType.Version);
            VersionMessagePayload versionMessagePayload = new VersionMessagePayload(
                ProtocolVersion.v0_13_1,
                DefaultServices.ForVersion(ProtocolVersion.v0_13_1),
                timeStamp,
                new NetworkAddressPayload(Service.NODE_NONE, new IPAddress(new byte[] { 208, 89, 206, 2 }), 38589),
                new NetworkAddressPayload(DefaultServices.ForVersion(ProtocolVersion.v0_13_1), IPAddress.Any, 0),
                2024707532345282954,
                "/Satoshi:0.13.1/",
                378478,
                true);

            byte[] payloadData = versionMessagePayload.GetBytes(versionMessageHeader);
            string payloadDataHex = BitConverter.ToString(payloadData).SimpleHex();
            string expectedDataHex = "7e1101000d000000000000001191665800000000000000000000000000000000000000000000ffffd059ce0296bd0d000000000000000000000000000000000000000000000000008a917ff7c534191c102f5361746f7368693a302e31332e312f6ec6050001".SimpleHex();
            Assert.AreEqual(payloadDataHex, expectedDataHex);
        }
    }
}