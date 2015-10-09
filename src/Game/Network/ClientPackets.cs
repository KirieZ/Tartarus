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
			PacketsDb.Add(0x07D1, CS_CharacterList);
			PacketsDb.Add(0x07D2, CS_CreateCharacter);
			PacketsDb.Add(0x07D3, CS_DeleteCharacter);
			PacketsDb.Add(0x07D5, CS_AccountWithAuth);
			PacketsDb.Add(0x07D6, CS_CheckCharacterName);
			PacketsDb.Add(0x1F40, CS_SystemSpecs);
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

			Server.Instance.OnUserJoin(client, userId, key);
		}

		/// <summary>
		/// Request the character list
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CharacterList(Player client, PacketStream stream)
		{
			//string userId = stream.ReadString(61);

			client.GetCharacterList();
		}

		/// <summary>
		/// Checks if character name is in use
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CheckCharacterName(Player client, PacketStream stream)
		{
			string name = stream.ReadString(19);

			client.CheckCharacterName(name);
		}

		/// <summary>
		/// Request to create a character
		/// </summary>
		/// <param name="client"></param>
		/// <param name="stream"></param>
		private void CS_CreateCharacter(Player client, PacketStream stream)
		{
			Players.LobbyCharacterInfo charInfo = new Players.LobbyCharacterInfo();

			charInfo.ModelInfo.Sex = stream.ReadInt32();
			charInfo.ModelInfo.Race = stream.ReadInt32();

			for (int i = 0; i < 5; i++)
				charInfo.ModelInfo.ModelId[i] = stream.ReadInt32();

			charInfo.ModelInfo.TextureId = stream.ReadInt32();

			for (int i = 0; i < 24; i++)
				charInfo.ModelInfo.WearInfo[i] = stream.ReadInt32();

			charInfo.Level = stream.ReadInt32();
			charInfo.Job = stream.ReadInt32();
			charInfo.JobLevel = stream.ReadInt32();
			charInfo.ExpPercentage = stream.ReadInt32();
			charInfo.Hp = stream.ReadInt32();
			charInfo.Mp = stream.ReadInt32();
			charInfo.Permission = stream.ReadInt32();
			charInfo.IsBanned = stream.ReadBool();
			charInfo.Name = stream.ReadString(19);
			charInfo.SkinColor = stream.ReadUInt32();
			charInfo.CreateTime = stream.ReadString(30);
			charInfo.DeleteTime = stream.ReadString(30);
			for (int i = 0; i < 24; i++)
				charInfo.WearItemEnhanceInfo[i] = stream.ReadInt32();
			for (int i = 0; i < 24; i++)
				charInfo.WearItemLevelInfo[i] = stream.ReadInt32();
			for (int i = 0; i < 24; i++)
				charInfo.WearItemElementalType[i] = stream.ReadByte();

			client.CreateCharacter(charInfo);
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
		/// Sends the result of a packet
		/// </summary>
		/// <param name="client"></param>
		/// <param name="packetId"></param>
		/// <param name="response"></param>
		/// <param name="value"></param>
		public void Result(Player client, ushort packetId, ushort response = 0, int value = 0)
		{
			PacketStream stream = new PacketStream(0x0000);
			
			stream.WriteUInt16(packetId);
			stream.WriteUInt16(response);
			stream.WriteInt32(value);

			ClientManager.Instance.Send(client, stream);
		}

		public void CharacterList(Player client, Players.LobbyCharacterInfo[] charList)
		{
			PacketStream stream = new PacketStream(0x07D4);

			stream.WriteUInt32(0); // currentSvTime
			stream.WriteUInt16(0); // last_login_index
			stream.WriteUInt16((ushort)charList.Length);
			for (int i = 0; i < charList.Length; i++)
			{
				stream.WriteInt32(charList[i].ModelInfo.Sex);
				stream.WriteInt32(charList[i].ModelInfo.Race);
				for (int j = 0; j < 5; j++)
					stream.WriteInt32(charList[i].ModelInfo.ModelId[j]);
				stream.WriteInt32(charList[i].ModelInfo.TextureId);
				for (int j = 0; j < 24; j++)
					stream.WriteInt32(charList[i].ModelInfo.WearInfo[j]);
				stream.WriteInt32(charList[i].Level);
				stream.WriteInt32(charList[i].Job);
				stream.WriteInt32(charList[i].JobLevel);
				stream.WriteInt32(charList[i].ExpPercentage);
				stream.WriteInt32(charList[i].Hp);
				stream.WriteInt32(charList[i].Mp);
				stream.WriteInt32(charList[i].Permission);
				stream.WriteBool(charList[i].IsBanned);
				stream.WriteString(charList[i].Name, 19);
				stream.WriteUInt32(charList[i].SkinColor);
				stream.WriteString(charList[i].CreateTime, 30);
				stream.WriteString(charList[i].DeleteTime, 30);
				for (int j = 0; j < 24; j++)
					stream.WriteInt32(charList[i].WearItemEnhanceInfo[j]);
				for (int j = 0; j < 24; j++)
					stream.WriteInt32(charList[i].WearItemLevelInfo[j]);
				for (int j = 0; j < 24; j++)
					stream.WriteByte(charList[i].WearItemElementalType[j]);
			}

			ClientManager.Instance.Send(client, stream);
		}
		#endregion
	}
}
