using Cait.Core.Interfaces.Net;
using System;
using System.IO;

namespace Cait.Bitcoin.Net.Messages.Base
{
    public abstract class Message : IDatagram
    {
        public MessageHeader Header = null;
        private MessagePayload Payload = null;

        public IDatagramHeader DatagramHeader
        {
            get
            {
                return Header;
            }
        }

        public IDatagramPayload DatagramPayload
        {
            get
            {
                return Payload;
            }
        }

        public byte[] GetBytes()
        {
            if (Header == null)
                throw new ArgumentNullException(nameof(Header), "Header can not be null");

            if (Payload == null)
                throw new ArgumentNullException(nameof(Header), "Payload can not be null");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] bytes = Header.GetBytes(Payload);
                memoryStream.Write(bytes, 0, bytes.Length);

                bytes = Payload.GetBytes(Header);
                memoryStream.Write(bytes, 0, bytes.Length);

                return memoryStream.ToArray();
            }
        }
    }
}