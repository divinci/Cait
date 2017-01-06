using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.NetTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cait.Bitcoin.Net.Messages.Tests
{
    [TestClass()]
    public class VerackMessageTests
    {
        [TestMethod()]
        public void VerackMessage_Build()
        {
            Message verackMessage = new Message(
                Constants.Magic.Main,
                Constants.MessageType.Verack);

            byte[] result = verackMessage.GetBytes(MessagePayload.Empty);

            Assert.AreEqual(
                result.AsSimpleHexString(),
                @"
                // Taken from https://en.bitcoin.it/wiki/Protocol_documentation#verack
                // Message header:
                    F9 BE B4 D9                          // Main network magic bytes
                    76 65 72 61  63 6B 00 00 00 00 00 00 // 'verack' command
                    00 00 00 00                          // Payload is 0 bytes long
                    5D F6 E0 E2                          // Checksum".AsSimpleHexString());
        }
    }
}