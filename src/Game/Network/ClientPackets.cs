// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.Content;

namespace Game.Network
{
	/// <summary>
	/// Packets exchanged between Client and Auth
	/// </summary>
	public class ClientPackets
	{
		public static readonly ClientPackets Instance = new ClientPackets();
		
		private delegate void PacketAction(Player client, PacketStream stream);

		private Dictionary<ushort, PacketAction> PacketsDb;

		public ClientPackets()
		{
			// Loads PacketsDb
			PacketsDb = new Dictionary<ushort, PacketAction>();

			#region Packets
			PacketsDb.Add(0x0032, CS_Version);
			PacketsDb.Add(0x07D5, CS_AccountWithAuth);
			PacketsDb.Add(0x1F40, CS_SystemSpecs);
			PacketsDb.Add(0x07D1, CS_CharacterList);
			PacketsDb.Add(0x07D2, CS_CreateCharacter);
			PacketsDb.Add(0x07D3, CS_DeleteCharacter);
			PacketsDb.Add(0x07D6, CS_CheckCharacterName);
			#endregion
		}

		
		/// <summary>
		/// Called whenever a packet is received from a game client
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		public void PacketReceived(Player client, PacketStream stream)
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
		#region Unused Packets
		/// <summary>
		/// SFrame version (not used officially)
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_Version(Player client, PacketStream stream)
		{
			//string version = stream.ReadString(20);
		}

		/// <summary>
		/// System specs (official name not found)
		/// not used
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_SystemSpecs(Player client, PacketStream stream) { }
		#endregion

		#region Lobby
		/// <summary>
		/// Connection from auth
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_AccountWithAuth(Player client, PacketStream stream)
		{
			string userId = stream.ReadString(61);
			ulong key = stream.ReadUInt64();

			Server.Instance.OnUserJoin(userId, key);
		}

		/// <summary>
		/// Request the character list
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CharacterList(Player client, PacketStream stream)
		{
			string account = stream.ReadString(61);
		}

		/// <summary>
		/// Checks if character name is in use
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CheckCharacterName(Player client, PacketStream stream)
		{
			string name = stream.ReadString(19);
		}

		/// <summary>
		/// Request to create a character
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CreateCharacter(Player client, PacketStream stream)
		{
			// Lobby_Character_Info
		}

		/// <summary>
		/// Requests to delete a character
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_DeleteCharacter(Player client, PacketStream stream)
		{
			string name = stream.ReadString(19);
			//string securityCode = stream.ReadString(19);
		}
		#endregion

		#endregion

		#region Server Packets
		/// <summary>
		/// Sends a response to a packet
		/// </summary>
		/// <param name="client"></param>
		/// <param name="packetId"></param>
		/// <param name="response"></param>
		/// <param name="value"></param>
		public void Response(Player client, ushort packetId, ushort response = 0, int value = 0)
		{
			PacketStream stream = new PacketStream(0x0000);
			
			stream.WriteUInt16(packetId);
			stream.WriteUInt16(response);
			stream.WriteInt32(value);

			ClientManager.Instance.Send(client, stream);
		}

		public void CharacterList(Player client)
		{
			//uint currentSvTime
			//ushort last_login_index
			//ushort count
			//Lobby_Character_Info[count];
		}
		#endregion
	}
}
