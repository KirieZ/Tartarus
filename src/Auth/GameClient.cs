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
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		// Network Data
		public Socket ClSocket { get; set; }
		public byte[] Buffer { get; set; }
		public PacketStream Data { get; set; }
		public int PacketSize { get; set; }
		public int Offset { get; set; }
		
		// User Info
		public byte Permission { get; set; }
		public ushort LastServerId { get; set; }

		public GameClient(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
		}
	}
}
