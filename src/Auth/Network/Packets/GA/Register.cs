// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.GA
{
    /// <summary>
    /// Request from a Game-Server to be listed on Server List
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Register : PacketHeader
    {
        public UInt16 Index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public String Name;
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsAdultServer;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String ScreenshotUrl;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public String Ip;
        public Int32 Port;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public String Key;
        public Byte Permission;
    }
}