// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.CS
{
    /// <summary>
    /// Client Version (currently unused)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Version : PacketHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public String _Version;
    }
}