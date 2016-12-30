using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Extensions;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Core.Extensions;
using Cait.Core.Interfaces.Net;
using System;
using System.IO;
using System.Net;

namespace Cait.Bitcoin.Net.Messages
{
    public class NetworkAddressPayload : IDatagramPayload
    {
        public DateTime Time { get; private set; }

        public Service[] Services { get; private set; }

        public IPAddress IPAddress { get; private set; }

        public ushort Port { get; private set; }

        public NetworkAddressPayload(Service services, IPAddress ipAddress, ushort port)
        {
            this.Time = DateTime.Now;
            this.Services = new Service[] { services };
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddressPayload(Service[] services, IPAddress ipAddress, ushort port)
        {
            this.Time = DateTime.Now;
            this.Services = services;
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddressPayload(DateTime time, Service[] services, IPAddress ipAddress, ushort port)
        {
            this.Time = time;
            this.Services = services;
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddressPayload(DateTime time, Service services, IPAddress ipAddress, ushort port)
        {
            this.Time = time;
            this.Services = new Service[] { services };
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public byte[] GetBytes(IDatagramHeader header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header), "Argument must not be null.");

            if (!(header is MessageHeader))
                throw new ArgumentNullException(nameof(header), "Argument must be a MessageHeader type.");

            MessageHeader messageHeader = header as MessageHeader;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (messageHeader.Command != MessageType.Version)
                {
                    int timeAsUnixTimestampRoundedUpToNearestSecond = (int)this.Time.AsUnixTimestamp();
                    memoryStream.Write(BitConverter.GetBytes(timeAsUnixTimestampRoundedUpToNearestSecond), 0, 4);
                }

                memoryStream.Write(BitConverter.GetBytes((ulong)this.Services.AsBitfield()), 0, 8);
                if (this.IPAddress == IPAddress.Any)
                {
                    memoryStream.Write(new byte[16], 0, 16);
                }
                else
                {
                    IPAddress ipv6MappedAddress = this.IPAddress.MapToIPv6();
                    byte[] ipv6MappedAddressBytes = ipv6MappedAddress.GetAddressBytes();

                    memoryStream.Write(ipv6MappedAddressBytes, 0, 16);
                }

                byte[] portBytes = BitConverter.GetBytes(this.Port);
                Array.Reverse(portBytes);

                memoryStream.Write(portBytes, 0, 2);

                return memoryStream.ToArray();
            }
        }
    }
}