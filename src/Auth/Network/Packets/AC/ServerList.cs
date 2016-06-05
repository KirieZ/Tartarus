// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Auth.Network.Packets.AC
{
    /// <summary>
    /// List of available game-servers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ServerList : PacketHeader
    {
        public UInt16 LastLoginServerId;
        public UInt16 Count;

        public ServerList()
        {
            ID = 0x2726;
            Size = (UInt32)Marshal.SizeOf(typeof(ServerList));
        }
    }

    /// <summary>
    /// Contains information of a game-server
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ServerInfo : IPacket
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
        public UInt16 UserRatio;
    }
}