// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.CA
{
    /// <summary>
    /// Contains User Credentials for a login attempt
    /// by Username and Password
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Login : PacketHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 61)]
        public String UserId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public Byte[] Password = new Byte[56];
    }
}