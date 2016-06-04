// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.CA
{
    /// <summary>
    /// User's request to join a Game-Server
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SelectServer : PacketHeader
    {
        public UInt16 ServerId;
    }
}