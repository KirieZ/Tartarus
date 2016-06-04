// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.CA
{
    /// <summary>
    /// Contains Client Version (Currently Unused)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Version : PacketHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public String _Version;
    }
}