// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game.Network.Packets.CS
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Point : IPacket
    {
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MoveRequest : PacketHeader
	{
        public uint Handle;
        public float X;
        public float Y;
        public uint CurrentTime;
        [MarshalAs(UnmanagedType.I1)]
        public bool SpeedSync;
        public ushort Count;
    }
}
