// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using Common.Utilities;
using System.Net.Sockets;

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
		public NetworkData NetData { get; set; }

		// User Info
		public int AccountId { get; set; }
		public string UserId { get; set; }
		public byte Permission { get; set; }
		public ushort LastServerId { get; set; }

		public byte[] Key { get; set; }

		public GameClient(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}
	}
}
