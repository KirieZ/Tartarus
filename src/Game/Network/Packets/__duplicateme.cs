// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Packet1 : PacketHeader
    {
        public Packet1()
        {
            ID = 0;
            Size = (UInt32)Marshal.SizeOf(typeof(Packet1));
        }
    }
}