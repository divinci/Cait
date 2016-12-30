using Cait.Core.Interfaces.Net;

namespace Cait.Bitcoin.Net.Messages.Base
{
    public class MessagePayload : IDatagramPayload
    {
        private byte[] _payload { get; set; }

        public MessagePayload(byte[] payload)
        {
            this._payload = payload;
        }

        public byte[] GetBytes(IDatagramHeader header)
        {
            return _payload;
        }

        public static MessagePayload Empty { get { return new MessagePayload(new byte[0]); } }
    }
}