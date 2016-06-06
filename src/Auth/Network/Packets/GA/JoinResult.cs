// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.GA
{
    /// <summary>
    /// Result from a player join request
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class JoinResult : PacketHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 61)]
        public String UserId;
        public UInt16 Result;
    }
}