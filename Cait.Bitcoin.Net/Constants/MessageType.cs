﻿namespace Cait.Bitcoin.Net.Constants
{
    public enum MessageType
    {
        Version,
        Verack,
        Addr,
        Inv,
        GetData,
        NotFound,
        GetBlocks,
        GetHeaders,
        Tx,
        Block,
        Headers,
        GetAddr,
        MemPool,
        Ping,
        Pong,
        Reject,
        FilterLoad,
        FilterAdd,
        FilterClear,
        MerkleBlock,
        Alert,
        SendHeaders,
        FeeFilter,
        SendCmpct,
        CmpctBlock,
        GetBlockTxn,
        BlockTxn
    }
}