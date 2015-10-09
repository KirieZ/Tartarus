using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Game;
using System.Net.Sockets;
using Game.Network;
using Game.Players;
using Game.Players.Structures;

namespace Game.Content
{
	public partial class Player
	{
		static int sqlConType = Settings.SqlEngine;
        static string sqlConString = "Server=" + Settings.SqlUserIp + ";Database=" + Settings.SqlUserDatabase + ";UID=" + Settings.SqlUserUsername + ";PWD=" + Settings.SqlUserPassword + ";Connection Timeout=5;";

		/// <summary>
		/// Connection data
		/// </summary>
		public NetworkData NetData { get; set; }

		/// <summary>
		/// User Account ID
		/// </summary>
		public int AccountId { get; set; }
		/// <summary>
		/// User Permission Level
		/// </summary>
		public byte Permission { get; set; }
		
		public Player(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}

		/// <summary>
		/// Gets user's character list and
		/// sends it to the client
		/// </summary>
		internal void GetCharacterList()
		{
			ClientPackets.Instance.CharacterList(this, Lobby.GetCharacterList(this));
		}

		/// <summary>
		/// Checks if character name is valid
		/// </summary>
		/// <param name="name"></param>
		internal void CheckCharacterName(string name)
		{
			if (!Lobby.CheckCharacterName(name))
				ClientPackets.Instance.Result(this, 0x07D6, 9);
			else
				ClientPackets.Instance.Result(this, 0x07D6, 0);
		}

		internal void CreateCharacter(LobbyCharacterInfo charInfo)
		{
			if (Lobby.Create(this, charInfo))
			{
				ClientPackets.Instance.Result(this, 0x07D2, 0);
				return;
			}

			ClientPackets.Instance.Result(this, 0x07D2, 7); // Unknown
		}

		internal void DeleteCharacter(string name)
		{
			if (Lobby.Delete(this, name))
			{
				ClientPackets.Instance.Result(this, 0x07D3, 0);
				return;
			}

			ClientPackets.Instance.Result(this, 0x07D2, 8); // DB Error
		}
	}
}