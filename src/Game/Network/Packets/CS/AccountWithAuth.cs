// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.CS
{
    /// <summary>
    /// Connection from auth
    /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class AccountWithAuth : PacketHeader
	{
  		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 61)]
		public String Account;
  		public Int64 OneTimeKey;
	}
}