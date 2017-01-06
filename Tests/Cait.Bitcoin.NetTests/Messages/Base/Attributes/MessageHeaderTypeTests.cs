using Cait.Bitcoin.Net.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cait.Bitcoin.Net.Messages.Base.Attributes.Tests
{
    [TestClass()]
    public class MessageHeaderTypeTests
    {
        [TestMethod()]
        public void MessageHeaderType_Constructor()
        {
            MessageHeaderType messageHeaderType = new MessageHeaderType(MessageType.CmpctBlock);
            Assert.AreEqual(MessageType.CmpctBlock, messageHeaderType.MessageType);
        }
    }
}