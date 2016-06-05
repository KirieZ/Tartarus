// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using CA = Auth.Network.Packets.CA;
using AC = Auth.Network.Packets.AC;

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
		/// Result of a packet
		/// </summary>
		/// <param name="client"></param>
		/// <param name="code"></param>
		public void Result(GameClient client, ushort code)
		{
            AC.Result result = new Packets.AC.Result();
            result.RequestMsgId = 0x271A;
            result._Result = code;
            result.Value = 0;

            result.CreateChecksum();
			ClientManager.Instance.Send(client, PacketManager.ToArray(result));
		}

		/// <summary>
		/// Sends data to allow user to connect to a game server
		/// </summary>
		/// <param name="client"></param>
		/// <param name="result"></param>
		/// <param name="otp"></param>
		/// <param name="pendingTime"></param>
		public void SelectServer(GameClient client, ushort result, ulong otp, uint pendingTime)
		{
            AC.SelectServer selectServer = new AC.SelectServer();
            selectServer.Result = result;
            selectServer.Otp = otp;
            selectServer.PendingTime = pendingTime;

            selectServer.CreateChecksum();
			ClientManager.Instance.Send(client, PacketManager.ToArray(selectServer));
		}

        /// <summary>
        /// Sends the list of servers
        /// </summary>
        /// <param name="client"></param>
        /// <param name="servers"></param>
        public void ServerList(GameClient client)
        {
            AC.ServerList serverList = new AC.ServerList();
            AC.ServerInfo serverInfo = new AC.ServerInfo();

            ushort count = 0;
            int offset;
            byte[] bServerList = new byte[0];
            int size = PacketManager.GetSize(serverInfo);

            foreach (ushort index in Server.Instance.GameServers.Keys)
            {
                GameServer gs = Server.Instance.GameServers[index];

                if (gs.Permission > client.Permission) // Insufficient permission, skip
                    continue;

                serverInfo.Index = index;
                serverInfo.Name = gs.Name;
                serverInfo.IsAdultServer = gs.AdultServer;
                serverInfo.ScreenshotUrl = gs.NoticeUrl;
                serverInfo.Ip = gs.IP;
                serverInfo.Port = gs.Port;
                serverInfo.UserRatio = gs.UserRatio;

                offset = bServerList.Length;
                Array.Resize(ref bServerList, bServerList.Length + size);
                Buffer.BlockCopy(PacketManager.ToArray(serverInfo), 0, bServerList, offset, size);

                ++count;
            }

            serverList.LastLoginServerId = client.LastServerId;
            serverList.Count = count;

            serverList.Size = (uint)(PacketManager.GetSize(serverList) + bServerList.Length);
            serverList.Checksum = PacketManager.GetChecksum(serverList.Size, serverList.ID);

            byte[] buffer = PacketManager.ToArray(serverList);
            offset = buffer.Length;
            Array.Resize(ref buffer, offset + bServerList.Length);
            Buffer.BlockCopy(bServerList, 0, buffer, offset, bServerList.Length);
            
            ClientManager.Instance.Send(client, buffer);
		}
		#endregion
	}
}
