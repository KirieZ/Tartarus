// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game.Network
{
	/// <summary>
	/// Packets exchanged between Game and Auth servers
	/// </summary>
	public class AuthPackets
	{
		public static readonly AuthPackets Instance = new AuthPackets();
		
		private delegate void PacketAction(AuthServer server, PacketStream stream);

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
		public void PacketReceived(AuthServer server, PacketStream stream)
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
		/// Registers a server on Auth
		/// </summary>
		public void Register()
		{
			PacketStream stream = new PacketStream(0x1000);
			
			stream.WriteUInt16(Settings.Index);
			stream.WriteString(Settings.Name, 21);
			stream.WriteBool(false); // Adult Server
			stream.WriteString(Settings.Notice, 256);
			stream.WriteString(Settings.ServerIP, 16);
			stream.WriteInt32(Settings.Port);
			stream.WriteString(Settings.AcceptorKey, 10);
			stream.WriteByte(Settings.Permission);

			AuthManager.Instance.Send(stream);
		}

		/// <summary>
		/// Register result
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void AG_Result(AuthServer server, PacketStream stream)
		{
			ushort result = stream.ReadUInt16();

			Server.Instance.RegisterResult(result);
		}

		/// <summary>
		/// Informs data to validate an user connection
		/// </summary>
		/// <param name="server"></param>
		/// <param name="stream"></param>
		private void AG_UserJoin(AuthServer server, PacketStream stream)
		{
			string userId = stream.ReadString(61);
			ulong key = stream.ReadUInt64();

			Server.Instance.PendingUser(userId, key);
		}

		/// <summary>
		/// Result of join request
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="result"></param>
		internal void JoinResult(string userId, ushort result)
		{
			PacketStream stream = new PacketStream(0x1011);

			stream.WriteString(userId, 61);
			stream.WriteUInt16(result);

			AuthManager.Instance.Send(stream);
		}
	}
}
