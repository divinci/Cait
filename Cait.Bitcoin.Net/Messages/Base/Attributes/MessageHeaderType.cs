using Cait.Bitcoin.Net.Constants;
using System;

namespace Cait.Bitcoin.Net.Messages.Base.Attributes
{
    public class MessageHeaderType : Attribute
    {
        public MessageType MessageType { get; set; }

        public MessageHeaderType(MessageType messageType)
        {
            this.MessageType = messageType;
        }
    }
}