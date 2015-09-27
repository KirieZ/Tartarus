// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Auth
{
	/// <summary>
	/// Packets exchanged between Client and Auth
	/// </summary>
	public class ClientPackets
	{
		public static readonly ClientPackets Instance = new ClientPackets();
		
		private delegate void PacketAction(GameClient client, PacketStream stream);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public ClientPackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			PacketsDb.Add(0x2711, CA_Version);
			PacketsDb.Add(0x271A, CA_Account);
			PacketsDb.Add(0x271C, CA_IMBC_Account);
			PacketsDb.Add(0x2725, CA_ServerList);
			PacketsDb.Add(0x2727, CA_SelectServer);
			PacketsDb.Add(0x270F, CA_Unknown);
			#endregion

		}

		/// <summary>
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		public void PacketReceived(GameClient client, PacketStream stream)
		{
			// Is it a known packet ID
			if (!PacketsDb.ContainsKey(stream.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", stream.GetId());
				return;
			}

			// Calls this packet parsing function
			Task.Factory.StartNew(() => { PacketsDb[stream.GetId()].Invoke(client, stream); });
		}

		#region Client Packets
		private void CA_Unknown(GameClient client, PacketStream stream) { }

		/// <summary>
		/// Client Version
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CA_Version(GameClient client, PacketStream stream)
		{
			string version = stream.ReadString(20);
		}

		/// <summary>
		/// Login
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CA_Account(GameClient client, PacketStream stream)
		{
			string userId = stream.ReadString(61);
			byte[] cryptedPass = stream.ReadBytes(56);

			client.Login(userId, cryptedPass);
		}

		private void CA_IMBC_Account(GameClient client, PacketStream stream)
		{
			string userId = stream.ReadString(61);
			string otp = stream.ReadString(48);

			client.IMBCLogin(userId, otp);
		}

		/// <summary>
		/// Requests the server list
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CA_ServerList(GameClient client, PacketStream stream)
		{
			ServerList(client);
		}

		/// <summary>
		/// When client wants to join a server
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CA_SelectServer(GameClient client, PacketStream stream)
		{
			ushort serverId = stream.ReadUInt16();
			client.JoinServer(serverId);
		}
		#endregion

		#region Server Packets
		/// <summary>
		/// Login Result
		/// </summary>
		/// <param name="client"></param>
		/// <param name="result"></param>
		public void Result(GameClient client, ushort result)
		{
			PacketStream stream = new PacketStream(0x2710);

			stream.WriteUInt16(0x271A); // msg Id
			stream.WriteUInt16(result); // result
			stream.WriteInt32(0);

			ClientManager.Instance.Send(client, stream);
		}

		/// <summary>
		/// Sends data to allow user to connect to a game server
		/// </summary>
		/// <param name="client"></param>
		/// <param name="result"></param>
		/// <param name="otp"></param>
		/// <param name="pendingTime"></param>
		public void SelectServer(GameClient client, ushort result, byte[] otp, uint pendingTime)
		{
			PacketStream stream = new PacketStream(0x2728);

			stream.WriteUInt16(result);
			stream.WriteBytes(otp);
			stream.WriteUInt32(pendingTime);

			ClientManager.Instance.Send(client, stream);
		}

		/// <summary>
		/// Sends the list of servers
		/// </summary>
		/// <param name="client"></param>
		/// <param name="servers"></param>
		public void ServerList(GameClient client)
		{
			PacketStream stream = new PacketStream(0x2726);

			stream.WriteUInt16(client.LastServerId);
			stream.WriteUInt16((ushort)Server.Instance.GameServers.Count);
			foreach(ushort index in Server.Instance.GameServers.Keys)
			{
				GameServer gs = Server.Instance.GameServers[index];

				stream.WriteUInt16(index);
				stream.WriteString(gs.Name, 21);
				stream.WriteBool(gs.AdultServer);
				stream.WriteString(gs.NoticeUrl, 256);
				stream.WriteString(gs.IP, 16);
				stream.WriteInt32(gs.Port);
				stream.WriteUInt16(gs.UserRatio);
			}

			ClientManager.Instance.Send(client, stream);
		}
		#endregion
	}
}
