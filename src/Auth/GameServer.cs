// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Security.Cryptography;
using Auth.Network;

namespace Auth
{
	/// <summary>
	/// Holds a GameServer data
	/// </summary>
	public class GameServer
	{
		private Dictionary<string, GameClient> PendingClients;

		public NetworkData NetData { get; set; }

		// Server Info
		public ushort Index { get; set; }
		public string Name { get; set; }
		public string IP { get; set; }
		public int Port { get; set; }
		public string NoticeUrl { get; set; }
		public bool AdultServer { get; set; }
		public ushort UserRatio { get; set; }
		public byte Permission { get; set; }

		public GameServer(Socket socket)
		{
			this.NetData = new NetworkData(socket);
			this.PendingClients = new Dictionary<string, GameClient>();
		}

		/// <summary>
		/// Generates join key and informs the game-server
		/// </summary>
		/// <param name="gameClient"></param>
		internal void UserJoin(GameClient gameClient)
		{
			// Checks if user can join
			// This if avoids hack attempts (joining a server that isn't listed)
			if (gameClient.Permission < this.Permission)
				return;

			// Generates a join key
			byte[] key = new byte[8];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(key);

			gameClient.Key = BitConverter.ToInt64(key, 0);
			
			// Stores the client
			if (this.PendingClients.ContainsKey(gameClient.UserId))
				this.PendingClients.Add(gameClient.UserId, gameClient);
			else
				this.PendingClients[gameClient.UserId] = gameClient;

			// Sends the request to Game-server
			GamePackets.Instance.UserJoin(this, gameClient);
		}

		/// <summary>
		/// Handles the result from game server for the join request
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="result"></param>
		internal void JoinResult(string userId, ushort result)
		{
			GameClient gc = this.PendingClients[userId];
			this.PendingClients.Remove(userId);

			if (result == 1)
			{ // Already logged in
				// TODO : find the result code
				ClientPackets.Instance.SelectServer(gc, 1, gc.Key, 10);
				return;
			}

			ClientPackets.Instance.SelectServer(gc, result, gc.Key, 10);
		}
	}
}
