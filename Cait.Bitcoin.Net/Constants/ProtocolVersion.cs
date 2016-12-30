namespace Cait.Bitcoin.Net.Constants
{
    public enum ProtocolVersion
    {
        #region Nicely formatted Versions

        /// <summary>
        /// Version 0.13.1 (70014)
        /// </summary>
        v0_13_1 = 70014,

        /// <summary>
        /// Version 0.13.0 (70013)
        /// </summary>
        v0_13_0 = 70013,

        /// <summary>
        /// Version 0.12.1 (70012)
        /// </summary>
        v0_12_1 = 70012,

        /// <summary>
        /// Version 0.12.0 (70011)
        /// </summary>
        v0_12_0 = 70011,

        /// <summary>
        /// Version 0.10.0 (70001)
        /// </summary>
        v0_10_0 = 70001,

        #endregion Nicely formatted Versions

        #region Taken from https://github.com/bitcoin/bitcoin/blob/master/src/versionbits.h

        PROTOCOL_VERSION = 70015,

        //! initial proto version, to be increased after version/verack negotiation
        INIT_PROTO_VERSION = 209,

        //! In this version, 'getheaders' was introduced.
        GETHEADERS_VERSION = 31800,

        //! disconnect from peers older than this proto version
        MIN_PEER_PROTO_VERSION = GETHEADERS_VERSION,

        //! nTime field added to CAddress, starting with this version,
        //! if possible, avoid requesting addresses nodes older than this
        CADDR_TIME_VERSION = 31402,

        //! BIP 0031, pong message, is enabled for all versions AFTER this one
        BIP0031_VERSION = 60000,

        //! "mempool" command, enhanced "getdata" behavior starts with this version
        MEMPOOL_GD_VERSION = 60002,

        //! "filter*" commands are disabled without NODE_BLOOM after and including this version
        NO_BLOOM_VERSION = 70011,

        //! "sendheaders" command and announcing blocks with headers starts with this version
        SENDHEADERS_VERSION = 70012,

        //! "feefilter" tells peers to filter invs to you by fee starts with this version
        FEEFILTER_VERSION = 70013,

        //! short-id-based block download starts with this version
        SHORT_IDS_BLOCKS_VERSION = 70014,

        //! not banning for invalid compact blocks starts with this version
        INVALID_CB_NO_BAN_VERSION = 70015

        #endregion Taken from https://github.com/bitcoin/bitcoin/blob/master/src/versionbits.h
    }
}