// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.CS
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PutoffItem : PacketHeader
    {
        public Byte Position;
        public UInt32 TargetHandle;
    }
}