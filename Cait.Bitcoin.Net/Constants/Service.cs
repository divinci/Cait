using System;

namespace Cait.Bitcoin.Net.Constants
{
    [Flags]
    public enum Service : ulong
    {
        // Nothing
        NODE_NONE = 0,

        // NODE_NETWORK means that the node is capable of serving the block chain. It is currently
        // set by all Bitcoin Core nodes, and is unset by SPV clients or other peers that just want
        // network services but don't provide them.
        NODE_NETWORK = (1 << 0),

        // NODE_GETUTXO means the node is capable of responding to the getutxo protocol request.
        // Bitcoin Core does not support this but a patch set called Bitcoin XT does.
        // See BIP 64 for details on how this is implemented.
        NODE_GETUTXO = (1 << 1),

        // NODE_BLOOM means the node is capable and willing to handle bloom-filtered connections.
        // Bitcoin Core nodes used to support this by default, without advertising this bit,
        // but no longer do as of protocol version 70011 (= NO_BLOOM_VERSION)
        NODE_BLOOM = (1 << 2),

        // NODE_WITNESS indicates that a node can be asked for blocks and transactions including
        // witness data.
        NODE_WITNESS = (1 << 3),

        // NODE_XTHIN means the node supports Xtreme Thinblocks
        // If this is turned off then the node will not service nor make xthin requests
        NODE_XTHIN = (1 << 4),

        // Bits 24-31 are reserved for temporary experiments. Just pick a bit that
        // isn't getting used, or one not being used much, and notify the
        // bitcoin-development mailing list. Remember that service bits are just
        // unauthenticated advertisements, so your code must be robust against
        // collisions and other cases where nodes may be advertising a service they
        // do not actually support. Other service bits should be allocated via the
        // BIP process.
    }

    public static class DefaultServices
    {
        public static Service[] ForVersion(ProtocolVersion protocolVersion)
        {
            switch (protocolVersion)
            {
                case ProtocolVersion.v0_13_1:
                    return new Service[] { Service.NODE_NETWORK, Service.NODE_BLOOM, Service.NODE_WITNESS };

                default:
                    throw new NotImplementedException();
            }
        }
    }
}