// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.RC4;
using System.Data.Common;
using Common.Utilities;

namespace Game.Network
{
	/// <summary>
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		//static int sqlConType = Settings.SqlEngine;
		//static string sqlConString = "Server="+Settings.SqlIp+";Database="+Settings.SqlDatabase+";UID="+Settings.SqlUsername+";PWD="+Settings.SqlPassword+";Connection Timeout=5;";
		static XDes Des = new XDes(Globals.DESKey);

		// Network Data
		public Socket ClSocket;
		public byte[] Buffer;
		public PacketStream Data;
		public int PacketSize;
		public int Offset;
		public XRC4Cipher InCipher;
		public XRC4Cipher OutCipher;

		// User Info
		public int AccountId;
		public string UserId;
		public byte Permission;
		public ushort LastServerId;

		public byte[] Key;

		public GameClient(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
			this.InCipher = new XRC4Cipher(Globals.RC4Key);
			this.OutCipher = new XRC4Cipher(Globals.RC4Key);
		}
	}
}
