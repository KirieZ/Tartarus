// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common.RC4;

namespace Common
{
	public class NetworkData
	{
		public Socket ClSocket { get; set; }
		public byte[] Buffer;
		public PacketStream Data { get; set; }
		public int PacketSize { get; set; }
		public int Offset { get; set; }
		public XRC4Cipher InCipher { get; set; }
		public XRC4Cipher OutCipher { get; set; }

		public NetworkData(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
			this.InCipher = new XRC4Cipher(Globals.RC4Key);
			this.OutCipher = new XRC4Cipher(Globals.RC4Key);
		}
	}
}
