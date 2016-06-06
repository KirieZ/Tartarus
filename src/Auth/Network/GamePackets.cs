// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AG = Auth.Network.Packets.AG;
using GA = Auth.Network.Packets.GA;

namespace Auth.Network
{
	/// <summary>
	/// Packets exchanged by Auth and Game servers
	/// </summary>
	public class GamePackets
	{
		public static readonly GamePackets Instance = new GamePackets();
		
		private delegate void PacketAction(GameServer server, byte[] stream);

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
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="packet"></param>
		public void PacketReceived(GameServer server, PacketStream packet)
		{
            byte[] data = packet.ToArray();

            ConsoleUtils.HexDump(data, "Received from GameServer");

            // Is it a known packet ID
            if (!PacketsDb.ContainsKey(packet.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", packet.GetId());
				return;
			}

            // Calls this packet parsing function
            Task.Factory.StartNew(() => { PacketsDb[packet.GetId()].Invoke(server, data); });
		}

		/// <summary>
		/// The result of a join inform
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void GA_JoinResult(GameServer server, byte[] data)
		{
            GA.JoinResult joinResult = (GA.JoinResult)PacketManager.ToStructure(data, data.Length, typeof(GA.JoinResult));

            string userId = joinResult.UserId;
            ushort result = joinResult.Result;

			server.JoinResult(userId, result);
		}

		/// <summary>
		/// Register server data
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void GA_Register(GameServer server, byte[] data)
		{
            GA.Register register = (GA.Register)PacketManager.ToStructure(data, data.Length, typeof(GA.Register));

            ushort index = register.Index;
            server.Name = register.Name;
            server.AdultServer = register.IsAdultServer;
            server.NoticeUrl = register.ScreenshotUrl;
            server.IP = register.Ip;
            server.Port = (short)register.Port;
            string key = register.Key;
            server.Permission = register.Permission;

			Server.Instance.OnRegisterGameServer(index, server, key);
		}

		/// <summary>
		/// Sends the result of server register
		/// </summary>
		/// <param name="server"></param>
		/// <param name="result">0 = success; 1 = duplicated index</param>
		public void RegisterResult(GameServer server, ushort result)
		{
            AG.RegisterResult registerResult = new AG.RegisterResult();
            registerResult.Result = result;

            registerResult.CreateChecksum();
			GameManager.Instance.Send(server, PacketManager.ToArray(registerResult));
		}

		/// <summary>
		/// Informs that a user wants to join the server
		/// </summary>
		/// <param name="gameServer"></param>
		/// <param name="userId"></param>
		/// <param name="key"></param>
		internal void UserJoin(GameServer gameServer, GameClient client)
		{
            AG.UserJoin userJoin = new AG.UserJoin();
            userJoin.UserId = client.UserId;
            userJoin.Key = client.Key;
            userJoin.Permission = client.Permission;
            userJoin.AccountId = client.AccountId;

            userJoin.CreateChecksum();
			GameManager.Instance.Send(gameServer, PacketManager.ToArray(userJoin));
		}
	}
}
