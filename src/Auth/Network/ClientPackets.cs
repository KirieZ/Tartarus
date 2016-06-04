// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using CA = Auth.Network.Packets.CA;

namespace Auth.Network
{
	/// <summary>
	/// Packets exchanged between Client and Auth
	/// </summary>
	public class ClientPackets
	{
		public static readonly ClientPackets Instance = new ClientPackets();
		
		private delegate void PacketAction(GameClient client, byte[] data);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public ClientPackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			PacketsDb.Add(0x2711, CA_Version);
			PacketsDb.Add(0x271A, CA_Login);
			PacketsDb.Add(0x271C, CA_ImbcLogin);
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
		public void PacketReceived(GameClient client, PacketStream packet)
		{
            byte[] data = packet.ToArray();
            ConsoleUtils.HexDump(data, "Received from Client");

            // Is it a known packet ID
            if (!PacketsDb.ContainsKey(packet.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", packet.GetId());
                return;
			}

            // Calls this packet parsing function
            Task.Factory.StartNew(() => { PacketsDb[packet.GetId()].Invoke(client, data); });
		}

		#region Client Packets
		private void CA_Unknown(GameClient client, byte[] data) { }
		private void CA_Version(GameClient client, byte[] data)
		{
            //CA.Version version = (CA.Version) PacketManager.ToStructure(data, data.Length, typeof(CA.Version));
        }
        private void CA_Login(GameClient client, byte[] data)
		{
            CA.Login login = (CA.Login)PacketManager.ToStructure(data, data.Length, typeof(CA.Login));
            client.Login(login.UserId, login.Password);
		}
        private void CA_ImbcLogin(GameClient client, byte[] data)
		{
            CA.ImbcLogin login = (CA.ImbcLogin)PacketManager.ToStructure(data, data.Length, typeof(CA.ImbcLogin));
            client.IMBCLogin(login.UserId, login.OTP);
		}
        private void CA_ServerList(GameClient client, byte[] data)
		{
            // Requests the server list (Empty packet)
            ServerList(client);
		}
		private void CA_SelectServer(GameClient client, byte[] data)
		{
            CA.SelectServer select = (CA.SelectServer)PacketManager.ToStructure(data, data.Length, typeof(CA.SelectServer));
			client.JoinServer(select.ServerId);
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
			stream.WriteUInt16(0);
			ushort count = 0;

			foreach(ushort index in Server.Instance.GameServers.Keys)
			{
				GameServer gs = Server.Instance.GameServers[index];
				
				if (gs.Permission > client.Permission) // Insufficient permission, skip
					continue;

				count++;

				stream.WriteUInt16(index);
				stream.WriteString(gs.Name, 21);
				stream.WriteBool(gs.AdultServer);
				stream.WriteString(gs.NoticeUrl, 256);
				stream.WriteString(gs.IP, 16);
				stream.WriteInt32(gs.Port);
				stream.WriteUInt16(gs.UserRatio);
			}

			// Writes real server count
			stream.WriteAt(BitConverter.GetBytes(count), Globals.HeaderLength + 2, 2);

			ClientManager.Instance.Send(client, stream);
		}
		#endregion
	}
}
