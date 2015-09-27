// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Auth
{
	/// <summary>
	/// Holds a GameServer data
	/// </summary>
	public class GameServer
	{
		// Network Data
		public Socket ClSocket { get; set; }
		public byte[] Buffer { get; set; }
		public PacketStream Data { get; set; }
		public int PacketSize { get; set; }
		public int Offset { get; set; }

		// Server Info
		public ushort Index { get; set; }
		public string Name { get; set; }
		public string IP { get; set; }
		public short Port { get; set; }
		public string NoticeUrl { get; set; }
		public bool AdultServer { get; set; }
		public ushort UserRatio { get; set; }
		
		public GameServer(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
		}
	}
}
