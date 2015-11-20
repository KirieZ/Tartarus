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
    public enum JobDepth : byte
    {
        Basic = 1,
        First = 2,
        Second = 4,
    }

	public class Player : CreatureInfo
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

		#region Character Data
		public int CharacterId { get; set; }

		public string Name { get; set; }

		public int PartyId { get; set; }

		public int GuildId { get; set; }

		public Position Position { get; set; }

		public int Sex { get; set; }

		public int Level { get; set; }

		public int MaxReachedLevel { get; set; }

		public long Exp { get; set; }

		public long LastDecreasedExp { get; set; }

		public int Stamina { get; set; }

        public int MaxStamina { get; set; }

		public int Havoc { get; set; }

        public int MaxHavoc { get; set; }

		public short Job { get; set; }

		public byte JobDepth { get; set; }

		public int JobLevel { get; set; }

		public int Jp { get; set; }

		public int TotalJp { get; set; }

		public PreviousJobData[] PrevJobs { get; set; }

		public decimal ImmoralPoints { get; set; }

		public int Cha { get; set; }

		public int Pkc { get; set; }

		public int Dkc { get; set; }

		public HuntaholicData Huntaholic { get; set; }

		public long Gold { get; set; }

		public int Chaos { get; set; }

        public int MaxChaos { get; set; }

		public int HairId { get; set; }

		public int FaceId { get; set; }

		public int BodyId { get; set; }

		public int HandsId { get; set; }

		public int FeetId { get; set; }

		public int TextureId { get; set; }

		public BeltSlotData[] Belt { get; set; }

		public SummonData[] Summon { get; set; }

		public int MainSummon { get; set; }

		public int SubSummon { get; set; }

		public int RemainSummonTime { get; set; }

		public int Pet { get; set; }

		public int ChatBlockTime { get; set; }

		public int AdvChatCount { get; set; }

		public int GuildBlockTime { get; set; }

		public byte PkMode { get; set; }

		public string ClientInfo { get; set; }

		public PlayerStat Stats { get; set; }
		public PlayerStat BonusStats { get; set; }

		public PlayerAttribute Attributes { get; set; }
		public PlayerAttribute BonusAttributes { get; set; }

        public List<uint> Inventory { get; set; }
        public uint[] WearInfo { get; set; }
        public int StaminaRegen { get; internal set; }

        #endregion

        public Player(Socket socket)
			: base(ObjectType.Player)
		{
			this.NetData = new NetworkData(socket);

            this.MaxStamina = Globals.StartStamina;
            this.StaminaRegen = Globals.StartStaminaRec;

			this.Position = new Position();
			this.PrevJobs = new PreviousJobData[3] { new PreviousJobData(), new PreviousJobData(), new PreviousJobData() };
			this.Huntaholic = new HuntaholicData();
			this.Belt = new BeltSlotData[6] { new BeltSlotData(), new BeltSlotData(), new BeltSlotData(), new BeltSlotData(), new BeltSlotData(), new BeltSlotData() };
			this.Summon = new SummonData[6] { new SummonData(), new SummonData(), new SummonData(), new SummonData(), new SummonData(), new SummonData() };

			this.Stats = new PlayerStat();
			this.BonusStats = new PlayerStat();
            this.Attributes = new PlayerAttribute();
            this.BonusAttributes = new PlayerAttribute();

            this.Inventory = new List<uint>();
            this.WearInfo = new uint[(int)Wear.Max];
		}

		#region Lobby

		/// <summary>
		/// Gets user's character list and
		/// sends it to the client
		/// </summary>
		internal void GetCharacterList()
		{
			Lobby.GetCharacterList(this);
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

		/// <summary>
		/// Tries to create a new character
		/// </summary>
		/// <param name="charInfo"></param>
		internal void CreateCharacter(LobbyCharacterInfo charInfo)
		{
			if (Lobby.Create(this, charInfo))
			{
				ClientPackets.Instance.Result(this, 0x07D2, 0);
				return;
			}

			ClientPackets.Instance.Result(this, 0x07D2, 7); // Unknown
		}

		/// <summary>
		/// Tries to delete a character
		/// </summary>
		/// <param name="name"></param>
		internal void DeleteCharacter(string name)
		{
			if (Lobby.Delete(this, name))
			{
				ClientPackets.Instance.Result(this, 0x07D3, 0);
				return;
			}

			ClientPackets.Instance.Result(this, 0x07D2, 8); // DB Error
		}

		/// <summary>
		/// Tries to login to game world
		/// </summary>
		/// <param name="name"></param>
		/// <param name="race"></param>
		internal void Login(string name, byte race)
		{
			Lobby.Login(this, name, race);
		}

        #endregion

        #region Item
        public void Equip(Item item, bool sendUpdate = true)
        {
            Players.Inventory.Equip(this, item, sendUpdate);
        }

        public void Unequip(int position, bool sendUpdate = true)
        {
            Players.Inventory.Unequip(this, position, sendUpdate);
        }
        #endregion
    }
}