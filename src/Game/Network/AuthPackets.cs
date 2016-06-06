// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AG = Game.Network.Packets.AG;
using GA = Game.Network.Packets.GA;

namespace Game.Network
{
	/// <summary>
	/// Packets exchanged between Game and Auth servers
	/// </summary>
	public class AuthPackets
	{
		public static readonly AuthPackets Instance = new AuthPackets();
		
		private delegate void PacketAction(AuthServer server, byte[] data);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public AuthPackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			PacketsDb.Add(0x1001, AG_Result);
			PacketsDb.Add(0x1010, AG_UserJoin);
			#endregion

		}

		/// <summary>
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		public void PacketReceived(AuthServer server, PacketStream packet)
		{
            byte[] data = packet.ToArray();
            ConsoleUtils.HexDump(data, "Received from AuthServer");

            // Is it a known packet ID
            if (!PacketsDb.ContainsKey(packet.GetId()))
			{
				ConsoleUtils.ShowWarning("Unknown packet Id: {0}", packet.GetId());
				return;
			}

			// Calls this packet parsing function
			Task.Factory.StartNew(() => { PacketsDb[packet.GetId()].Invoke(server, data); });
		}

		private void AG_Result(AuthServer server, byte[] data)
		{
            AG.RegisterResult registerResult = (AG.RegisterResult)PacketManager.ToStructure(data, data.Length, typeof(AG.RegisterResult));
            
			Server.Instance.RegisterResult(registerResult.Result);
		}
		private void AG_UserJoin(AuthServer server, byte[] data)
		{
            AG.UserJoin userJoin = (AG.UserJoin)PacketManager.ToStructure(data, data.Length, typeof(AG.UserJoin));

			Server.Instance.PendingUser(userJoin.UserId, userJoin.Key, userJoin.Permission, userJoin.AccountId);
		}

        /// <summary>
		/// Registers a server on Auth
		/// </summary>
		public void Register()
        {
            GA.Register register = new GA.Register();
            register.Index = Settings.Index;
            register.Name = Settings.Name;
            register.IsAdultServer = false;
            register.ScreenshotUrl = Settings.Notice;
            register.Ip = Settings.ServerIP;
            register.Port = Settings.Port;
            register.Key = Settings.AcceptorKey;
            register.Permission = Settings.Permission;

            register.CreateChecksum();
            AuthManager.Instance.Send(PacketManager.ToArray(register));
        }

        /// <summary>
        /// Result of join request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="result"></param>
        internal void JoinResult(string userId, ushort result)
		{
            GA.JoinResult joinResult = new GA.JoinResult();
            joinResult.UserId = userId;
            joinResult.Result = result;

            joinResult.CreateChecksum();
			AuthManager.Instance.Send(PacketManager.ToArray(joinResult));
		}
	}
}
