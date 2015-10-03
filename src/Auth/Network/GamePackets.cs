// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Auth.Network
{
	/// <summary>
	/// Packets exchanged by Auth and Game servers
	/// </summary>
	public class GamePackets
	{
		public static readonly GamePackets Instance = new GamePackets();
		
		private delegate void PacketAction(GameServer server, PacketStream stream);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public GamePackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			PacketsDb.Add(0x1000, GA_Register);
			PacketsDb.Add(0x1011, GA_JoinResult);
			#endregion

		}

		/// <summary>
		/// The result of a join inform
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void GA_JoinResult(GameServer server, PacketStream stream)
		{
			string userId = stream.ReadString(61);
			ushort result = stream.ReadUInt16();

			server.JoinResult(userId, result);
		}

		/// <summary>
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		public void PacketReceived(GameServer server, PacketStream stream)
		{
			// Is it a known packet ID
			if (!PacketsDb.ContainsKey(stream.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", stream.GetId());
				return;
			}

			// Calls this packet parsing function
			Task.Factory.StartNew(() => { PacketsDb[stream.GetId()].Invoke(server, stream); });
		}

		/// <summary>
		/// Register server data
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void GA_Register(GameServer server, PacketStream stream)
		{
			ushort index = stream.ReadUInt16();
			server.Name = stream.ReadString(21);
			server.AdultServer = stream.ReadBool();
			server.NoticeUrl = stream.ReadString(256);
			server.IP = stream.ReadString(16);
			server.Port = (short)stream.ReadInt32();
			string key = stream.ReadString(10);

			Server.Instance.OnRegisterGameServer(index, server, key);
		}

		/// <summary>
		/// Sends the result of server register
		/// </summary>
		/// <param name="server"></param>
		/// <param name="result">0 = success; 1 = duplicated index</param>
		public void RegisterResult(GameServer server, ushort result)
		{
			PacketStream stream = new PacketStream(0x1001);

			stream.WriteUInt16(result);

			GameManager.Instance.Send(server, stream);
		}

		/// <summary>
		/// Informs that a user wants to join the server
		/// </summary>
		/// <param name="gameServer"></param>
		/// <param name="userId"></param>
		/// <param name="key"></param>
		internal void UserJoin(GameServer gameServer, string userId, byte[] key)
		{
			PacketStream stream = new PacketStream(0x1010);

			stream.WriteString(userId, 61);
			stream.WriteBytes(key);

			GameManager.Instance.Send(gameServer, stream);
		}
	}
}
