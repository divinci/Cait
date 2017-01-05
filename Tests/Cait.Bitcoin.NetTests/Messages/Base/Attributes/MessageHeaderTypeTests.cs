using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cait.Bitcoin.Net.Messages.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cait.Bitcoin.Net.Constants;

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