using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Extensions;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Bitcoin.Net.Messages.Base.Attributes;
using Cait.Core.Extensions;
using Cait.Core.Interfaces.Net;
using System;
using System.IO;
using System.Linq;

namespace Cait.Bitcoin.Net.Messages
{
    /// <summary>
    /// https://en.bitcoin.it/wiki/Protocol_documentation#version
    /// </summary>
    [MessageHeaderType(MessageType.Version)]
    public class VersionMessage : IDatagramPayload
    {
        /// <summary>
        /// Identifies protocol version being used by the node
        /// </summary>
        public ProtocolVersion ProtocolVersion { get; private set; }

        /// <summary>
        /// bitfield of features to be enabled for this connection
        /// </summary>
        public ServiceFlag[] Services { get; private set; }

        /// <summary>
        /// standard UNIX timestamp in seconds
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// The network address of the node receiving this message
        /// </summary>
        public NetworkAddress RemoteNetworkAddress { get; private set; }

        /// <summary>
        /// The network address of the node emitting this message
        /// </summary>
        public NetworkAddress LocalNetworkAddress { get; private set; }

        /// <summary>
        /// Node random nonce, randomly generated every time a version packet is sent. This nonce is used to detect connections to self.
        /// </summary>
        public ulong Nonce { get; private set; }

        /// <summary>
        /// User Agent (0x00 if string is 0 bytes long)
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// The last block received by the emitting node.
        /// </summary>
        public int StartHeight { get; private set; }

        /// <summary>
        /// Whether the remote peer should announce relayed transactions or not, see BIP 0037
        /// </summary>
        public bool Relay { get; private set; }

        public VersionMessage(
            ProtocolVersion protocolVersion,
            ServiceFlag[] services,
            DateTime timeStamp,
            NetworkAddress remoteNetworkAddress,
            NetworkAddress localNetworkAddress,
            ulong nonce,
            string userAgent,
            int startHeight,
            bool relay) : base()
        {
            this.ProtocolVersion = protocolVersion;
            this.Services = services;
            this.TimeStamp = timeStamp;
            this.RemoteNetworkAddress = remoteNetworkAddress;
            this.LocalNetworkAddress = localNetworkAddress;
            this.Nonce = nonce;
            this.UserAgent = userAgent;
            this.StartHeight = startHeight;
            this.Relay = relay;
        }

        public VersionMessage(
            ProtocolVersion protocolVersion,
            ServiceFlag service,
            DateTime timeStamp,
            NetworkAddress remoteNetworkAddress,
            NetworkAddress localNetworkAddress,
            ulong nonce,
            string userAgent,
            int startHeight,
            bool relay) : base()
        {
            this.ProtocolVersion = protocolVersion;
            this.Services = new ServiceFlag[] { service };
            this.TimeStamp = timeStamp;
            this.RemoteNetworkAddress = remoteNetworkAddress;
            this.LocalNetworkAddress = localNetworkAddress;
            this.Nonce = nonce;
            this.UserAgent = userAgent;
            this.StartHeight = startHeight;
            this.Relay = relay;
        }

        public byte[] GetBytes(IDatagramHeader header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header), "Argument must not be null.");

            if (!(header is Message))
                throw new ArgumentException(nameof(header), "Argument must be a MessageHeader type.");

            Message messageHeader = header as Message;

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes((int)this.ProtocolVersion), 0, 4);

                int servicesBitwiseFlag = this.Services.Select(service => (Enum)service).ToArray().CreateFlagsBitfield();
                string servicesBitwiseBinary = Convert.ToString(servicesBitwiseFlag, 2);
                ms.Write(BitConverter.GetBytes((ulong)servicesBitwiseFlag), 0, 8);

                ms.Write(BitConverter.GetBytes((ulong)this.TimeStamp.AsUnixTimestamp()), 0, 8);

                byte[] remoteNetworkAddress = this.RemoteNetworkAddress.GetBytes(header);
                ms.Write(remoteNetworkAddress, 0, remoteNetworkAddress.Length);

                byte[] localNetworkAddress = this.LocalNetworkAddress.GetBytes(header);
                ms.Write(localNetworkAddress, 0, localNetworkAddress.Length);

                ms.Write(BitConverter.GetBytes(this.Nonce), 0, 8);

                byte[] userAgent = this.UserAgent.GetBytesVariableLength();
                ms.Write(userAgent, 0, userAgent.Length);

                ms.Write(BitConverter.GetBytes(this.StartHeight), 0, 4);

                if (this.ProtocolVersion >= ProtocolVersion.v0_10_0)
                {
                    ms.Write(BitConverter.GetBytes(this.Relay), 0, 1);
                }

                return ms.ToArray();
            }
        }
    }
}