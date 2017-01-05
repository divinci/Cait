using Cait.Bitcoin.Net.Constants;
using Cait.Bitcoin.Net.Extensions;
using Cait.Bitcoin.Net.Messages.Base.Attributes;
using Cait.Core.Interfaces.Net;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cait.Bitcoin.Net.Messages
{
    /// <summary>
    /// https://en.bitcoin.it/wiki/Protocol_documentation#version
    /// </summary>
    [MessageHeaderType(MessageType.Addr)]
    public class AddressMessagePayload : IDatagramPayload
    {
        public List<NetworkAddress> NetworkAddresses { get; private set; }

        public AddressMessagePayload(List<NetworkAddress> networkAddresses)
        {
            if (networkAddresses == null)
                throw new ArgumentNullException(nameof(networkAddresses));

            this.NetworkAddresses = networkAddresses;
        }

        public byte[] GetBytes(IDatagramHeader header)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] networkAddressListCountAsVariableIntBytes = this.NetworkAddresses.Count.GetBytesVariableLength();
                memoryStream.Write(networkAddressListCountAsVariableIntBytes, 0, networkAddressListCountAsVariableIntBytes.Length);
                foreach (NetworkAddress networkAddress in this.NetworkAddresses)
                {
                    byte[] networkAddressBytes = networkAddress.GetBytes(header);
                    memoryStream.Write(networkAddressBytes, 0, networkAddressBytes.Length);
                }

                return memoryStream.ToArray();
            }
        }
    }
}