// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.AG
{
    /// <summary>
    /// Result from Game-Server register request
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RegisterResult : PacketHeader
    {
        public UInt16 Result;
    }
}