using Cait.Bitcoin.Net.Constants;
using Cait.Core.Interfaces.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cait.Bitcoin.Net.Messages.Base
{
    public class Message : IDatagramHeader
    {
        /// <summary>
        /// Magic value indicating message origin network, and used to seek to next message when stream state is unknown
        /// </summary>
        public Magic Magic { get; private set; }

        /// <summary>
        /// ASCII string identifying the packet content, NULL padded (non-NULL padding results in packet rejected)
        /// </summary>
        public MessageType Command { get; private set; }

        public Message(Magic magic, MessageType command)
        {
            this.Magic = magic;
            this.Command = command;
        }

        public byte[] GetBytes(IDatagramPayload payload)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Magic
                memoryStream.Write(BitConverter.GetBytes((uint)Magic), 0, 4);

                // Command
                byte[] commandBits = Encoding.UTF8.GetBytes(Command.ToString().ToLower());
                memoryStream.Write(commandBits, 0, commandBits.Length);
                byte[] commandNullPadding = new byte[12 - commandBits.Length];
                memoryStream.Write(commandNullPadding, 0, commandNullPadding.Length);

                // Payload length
                byte[] payloadBytes = new byte[0];
                int payloadLength = 0;
                if (payload != null)
                {
                    payloadBytes = payload.GetBytes(this);
                    payloadLength = payloadBytes.Length;
                }
                memoryStream.Write(BitConverter.GetBytes((uint)payloadLength), 0, 4);

                // Checksum
                SHA256 sha256 = SHA256.Create();
                byte[] checkSum = sha256.ComputeHash(sha256.ComputeHash(payloadBytes));
                memoryStream.Write(checkSum, 0, 4);

                memoryStream.Write(payloadBytes, 0, payloadBytes.Length);

                return memoryStream.ToArray();
            }
        }
    }
}