// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System.Net.Sockets;

namespace Game
{
	/// <summary>
	/// Holds a AuthServer data
	/// </summary>
	public class AuthServer
	{
		// Network Data
		public NetworkData NetData { get; set; }

		// Server Info
		public string Key { get; set; }
		public string IP { get; set; }
		public short Port { get; set; }

		public AuthServer(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}
	}
}
