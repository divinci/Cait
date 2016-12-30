using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Extensions;
using Cait.Bitcoin.Net.Messages.Base;
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
    public class VersionMessagePayload : IDatagramPayload
    {
        /// <summary>
        /// Identifies protocol version being used by the node
        /// </summary>
        public ProtocolVersion ProtocolVersion { get; private set; }

        /// <summary>
        /// bitfield of features to be enabled for this connection
        /// </summary>
        public Service[] Services { get; private set; }

        /// <summary>
        /// standard UNIX timestamp in seconds
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// The network address of the node receiving this message
        /// </summary>
        public NetworkAddressPayload RemoteNetworkAddress { get; private set; }

        /// <summary>
        /// The network address of the node emitting this message
        /// </summary>
        public NetworkAddressPayload LocalNetworkAddress { get; private set; }

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

        public VersionMessagePayload(
            ProtocolVersion protocolVersion,
            Service[] services,
            DateTime timeStamp,
            NetworkAddressPayload remoteNetworkAddress,
            NetworkAddressPayload localNetworkAddress,
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

        public byte[] GetBytes(IDatagramHeader header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header), "Argument must not be null.");

            if (!(header is MessageHeader))
                throw new ArgumentNullException(nameof(header), "Argument must be a MessageHeader type.");

            MessageHeader messageHeader = header as MessageHeader;

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

                ms.Write(BitConverter.GetBytes(this.Relay), 0, 1);

                return ms.ToArray();
            }
        }
    }
}