// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.AG
{
    /// <summary>
    /// Result from Game-Server register request
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RegisterResult : PacketHeader
    {
        public UInt16 Result;

        public RegisterResult()
        {
            ID = 0x1001;
            Size = (UInt32)Marshal.SizeOf(typeof(RegisterResult));
        }
    }
}