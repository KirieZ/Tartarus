// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.AC
{
    /// <summary>
    /// Server selection result.
    /// Contains data used to allow user to join the game server
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SelectServer : PacketHeader
    {
        public UInt16 Result;
        public UInt64 Otp;
        public UInt32 PendingTime;

        public SelectServer()
        {
            ID = 0x2728;
            Size = (UInt32)Marshal.SizeOf(typeof(SelectServer));
        }
    }
}