// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.AG
{
    /// <summary>
    /// Request to Game-Server to allow user join it
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class UserJoin : PacketHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 61)]
        public String UserId;
        public UInt64 Key;
        public Byte Permission;
        public Int32 AccountId;
    }
}