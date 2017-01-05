using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Extensions;
using Cait.Bitcoin.Net.Messages.Base;
using Cait.Core.Extensions;
using Cait.Core.Interfaces.Net;
using System;
using System.Linq;
using System.IO;
using System.Net;

namespace Cait.Bitcoin.Net.Messages
{
    public class NetworkAddress : IDatagramPayload
    {
        public DateTime Time { get; private set; }

        public ServiceFlag[] Services { get; private set; }

        public IPAddress IPAddress { get; private set; }

        public ushort Port { get; private set; }

        public static NetworkAddress Unroutable(ServiceFlag serviceFlag)
        {
            return new NetworkAddress(serviceFlag, new IPAddress(new byte[] { 0, 0, 0, 0 }), 0);
        }
        public static NetworkAddress Unroutable(ServiceFlag[] serviceFlags)
        {
            return new NetworkAddress(serviceFlags, new IPAddress(new byte[] { 0, 0, 0, 0 }), 0);
        }

        public NetworkAddress(ServiceFlag service, IPAddress ipAddress, ushort port)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            this.Time = DateTime.Now;
            this.Services = new ServiceFlag[] { service };
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddress(ServiceFlag[] services, IPAddress ipAddress, ushort port)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            this.Time = DateTime.Now;
            this.Services = services;
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddress(DateTime time, ServiceFlag[] services, IPAddress ipAddress, ushort port)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            this.Time = time;
            this.Services = services;
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public NetworkAddress(DateTime time, ServiceFlag service, IPAddress ipAddress, ushort port)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            this.Time = time;
            this.Services = new ServiceFlag[] { service };
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public byte[] GetBytes(IDatagramHeader header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header), "Argument must not be null.");

            if (!(header is Message))
                throw new ArgumentException(nameof(header), "Argument must be a MessageHeader type.");

            Message messageHeader = header as Message;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (messageHeader.Command != MessageType.Version)
                {
                    int timeAsUnixTimestampRoundedUpToNearestSecond = (int)this.Time.AsUnixTimestamp();
                    memoryStream.Write(BitConverter.GetBytes(timeAsUnixTimestampRoundedUpToNearestSecond), 0, 4);
                }

                memoryStream.Write(BitConverter.GetBytes((ulong)this.Services.AsBitfield()), 0, 8);

                IPAddress ipv6MappedAddress = this.IPAddress.MapToIPv6();
                byte[] ipv6MappedAddressBytes = ipv6MappedAddress.GetAddressBytes();

                memoryStream.Write(ipv6MappedAddressBytes, 0, 16);

                byte[] portBytes = BitConverter.GetBytes(this.Port);
                Array.Reverse(portBytes);

                memoryStream.Write(portBytes, 0, 2);

                return memoryStream.ToArray();
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NetworkAddress))
                return false;

            NetworkAddress objNetworkAddress = obj as NetworkAddress;

            if (!objNetworkAddress.IPAddress.GetAddressBytes().ArrayEquals(this.IPAddress.GetAddressBytes()))
                return false;

            foreach (ServiceFlag serviceFlag in objNetworkAddress.Services)
            {
                if (!this.Services.Contains(serviceFlag))
                    return false;
            }

            foreach (ServiceFlag serviceFlag in this.Services)
            {
                if (!objNetworkAddress.Services.Contains(serviceFlag))
                    return false;
            }

            if (objNetworkAddress.Time != this.Time)
                return false;

            if (objNetworkAddress.Port != this.Port)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ this.IPAddress.GetHashCode();
                result = (result * 397) ^ this.Port.GetHashCode();
                foreach (ServiceFlag serviceFlag in this.Services)
                {
                    result = (result * 397) ^ serviceFlag.GetHashCode();
                }
                result = (result * 397) ^ this.Time.GetHashCode();
                return result;
            }
        }
    }
}