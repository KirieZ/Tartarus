// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Runtime.InteropServices;

namespace Game.Network.Packets.CS
{
    /// <summary>
    /// Request to create a character
    /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class CreateCharacter : PacketHeader
	{
        public LobbyCharacterInfo Info;
	}
}