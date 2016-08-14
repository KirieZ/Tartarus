// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.CS
{
    /// <summary>
    /// Checks if character name is in use
    /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class CheckCharacterName : PacketHeader
	{
  		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 19)]
		public String Name;
	}
}