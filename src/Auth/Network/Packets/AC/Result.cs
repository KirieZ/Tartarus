// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.AC
{
    /// <summary>
    /// Result of a Packet
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Result : PacketHeader
    {
        public UInt16 RequestMsgId;
        public UInt16 _Result;
        public Int32 Value;

        public Result()
        {
            ID = 0x2710;
            Size = (UInt32)Marshal.SizeOf(typeof(Result));
        }
    }
}