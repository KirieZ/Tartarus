// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.GA
{
    /// <summary>
    /// Request to Auth-Server to add this server to Server List
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
		
		public Register()
        {
            ID = 0x1000;
            Size = (UInt32)Marshal.SizeOf(typeof(Register));
        }
    }
}