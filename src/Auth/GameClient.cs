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

namespace Auth
{
	/// <summary>
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		static int sqlConType = 0;  //TODO : Load from configuration file
		static string sqlConString = "Server="+Settings.SqlIp+";Database="+Settings.SqlDatabase+";UID="+Settings.SqlUsername+";PWD="+Settings.SqlPassword+";Connection Timeout=5;";

		// Network Data
		public Socket ClSocket;
		public byte[] Buffer;
		public PacketStream Data;
		public int PacketSize;
		public int Offset;
		public XRC4Cipher InCipher;
		public XRC4Cipher OutCipher;

		// User Info
		public byte Permission;
		public ushort LastServerId;

		public GameClient(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
			this.InCipher = new XRC4Cipher(Globals.RC4Key);
			this.OutCipher = new XRC4Cipher(Globals.RC4Key);
		}

		internal static void UserLogin(GameClient client, string userId, byte[] cryptedPass)
		{
			// TODO : Login check

			ClientPackets.Instance.Result(client, 0); // Success
		}
	}
}
