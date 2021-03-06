// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Content;
using Common;
using System.Data.Common;
using Game.Players.Structures;
using Game.Network;
using Game.Content.Structures;

namespace Game.Players
{
	/// <summary>
	/// Handles Player's lobby activity
	/// </summary>
	public static class Lobby
	{
        private static object CreateLock = new object();

		/// <summary>
		/// Retrieves the character list
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		internal static void GetCharacterList(Player player)
		{
			List<Network.Packets.LobbyCharacterInfo> charList = new List<Network.Packets.LobbyCharacterInfo>();

            ushort lastLoginIndex = 0;

			using (DBManager dbManager = new DBManager(Databases.User))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand(0))
				{
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.String, player.AccountId);
					dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader reader = dbCmd.ExecuteReader())
                        {
                            ushort count = 0;
                            DateTime lastLoginTime = new DateTime(0);

                            while (reader.Read())
                            {
                                // Reads the character data
                                Network.Packets.LobbyCharacterInfo chara = new Network.Packets.LobbyCharacterInfo();
                                int charId = (int)reader[0];
                                chara.Name = (string)reader[2];
                                chara.ModelInfo.Race = (byte)reader[10];
                                chara.ModelInfo.Sex = (int)reader[11];
                                chara.ModelInfo.TextureId = (int)reader[45];
                                for (int i = 0; i < 5; i++)
                                    chara.ModelInfo.ModelId[i] = (int)reader[40 + i];

                                using (DBManager dbManager2 = new DBManager(Databases.User))
                                {
                                    using (DbCommand dbCmd2 = dbManager2.CreateCommand(6))
                                    {
                                        dbManager2.CreateInParameter(dbCmd2, "charId", System.Data.DbType.Int32, charId);

                                        try
                                        {
                                            dbCmd2.Connection.Open();

                                            using (DbDataReader reader2 = dbCmd2.ExecuteReader())
                                            {
                                                while (reader2.Read())
                                                {
                                                    chara.ModelInfo.WearInfo[(short)reader2[0]] = (int)reader2[1];
                                                    chara.WearItemEnhanceInfo[(short)reader2[0]] = (int)reader2[2];
                                                    chara.WearItemLevelInfo[(short)reader2[0]] = (int)reader2[3];
                                                    chara.WearItemElementalType[(short)reader2[0]] = (byte)(sbyte)reader2[4];
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            ConsoleUtils.ShowError("Failed to load character inventory. (Error: {0})", e.Message);
                                        }
                                        finally
                                        {
                                            dbCmd2.Connection.Close();
                                        }
                                    }
                                }
                                chara.Level = (int)reader[12];
                                chara.Job = (short)reader[20];
                                chara.JobLevel = (int)reader[22];
                                // TODO : chara.ExpPercentage = (int)
                                chara.Hp = (int)reader[16];
                                chara.Mp = (int)reader[17];
                                chara.Permission = player.Permission;
                                chara.IsBanned = false;
                                chara.SkinColor = (uint)reader[39];
                                chara.CreateTime = ((DateTime)reader[62]).ToString("yyyy/MM/dd");
                                chara.DeleteTime = "9999/12/31";
                                DateTime loginTime = (DateTime)reader[64];
                                if (loginTime > lastLoginTime)
                                {
                                    lastLoginTime = loginTime;
                                    lastLoginIndex = count;
                                }

                                // Adds data to char List
                                charList.Add(chara);

                                count++;
                            }
                        }
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
                    finally { dbCmd.Connection.Close(); }
				}
            }

            ClientPackets.Instance.CharacterList(player, charList.ToArray(), lastLoginIndex);
		}

		/// <summary>
		/// Checks if this character name is valid for a new character
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		internal static bool CheckCharacterName(string name)
		{
			bool valid = true;

			// Check for invalid characters
			if (name.IndexOfAny(Settings.ForbiddenCharacters) >= 0)
				return false;

			using (DBManager dbManager = new DBManager(Databases.User))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand(1))
				{
					dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, name);
					dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
					try
					{
						dbCmd.Connection.Open();
						using (DbDataReader reader = dbCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								valid = false;
							}
						}
					}
					// When an error occurs, say that name exists to block creation
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); valid = false; }
					finally { dbCmd.Connection.Close(); }
				}
			}

			return valid;
		}

		/// <summary>
		/// Creates a new Character to this account
		/// </summary>
		/// <param name="player"></param>
		/// <param name="charInfo"></param>
		/// <returns></returns>
		internal static bool Create(Player player, Network.Packets.LobbyCharacterInfo charInfo)
		{
            // Ensures the name is available
            if (!CheckCharacterName(charInfo.Name))
				return false;

            lock (CreateLock)
            {
                bool result = true;
                using (DBManager dbManager = new DBManager(Databases.User))
                {
                    using (DbCommand dbCmd = dbManager.CreateCommand(2))
                    {
                        int charId;
                        short job = 0;
                        int startWeapon = 0;
                        int startClothes = 0;
                        int startBag = 490001;
                        Position startPos = new Position();

                        switch (charInfo.ModelInfo.Race)
                        {
                            case 3: // Gaia
                                job = 100;
                                startPos.X = 164474;
                                startPos.Y = 52932;

                                startWeapon = 112100; //Trainee's Small Axe
                                if (charInfo.ModelInfo.WearInfo[2] == 601)
                                    startClothes = 220100;
                                else
                                    startClothes = 220109;
                                break;

                            case 4: // Deva
                                job = 200;
                                startPos.X = 164335;
                                startPos.Y = 49510;

                                startWeapon = 106100; // Beginner's Mace
                                if (charInfo.ModelInfo.WearInfo[2] == 601)
                                    startClothes = 240100;
                                else
                                    startClothes = 240109;
                                break;

                            case 5: // Asura
                                job = 300;
                                startPos.X = 168356;
                                startPos.Y = 55399;

                                startWeapon = 103100; // Beginner's Dirk
                                if (charInfo.ModelInfo.WearInfo[2] == 601)
                                    startClothes = 230100;
                                else
                                    startClothes = 230109;
                                break;
                        }

                        dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.Int32, player.AccountId);
                        dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, charInfo.Name);
                        dbManager.CreateInParameter(dbCmd, "race", System.Data.DbType.Byte, (byte)charInfo.ModelInfo.Race);
                        dbManager.CreateInParameter(dbCmd, "sex", System.Data.DbType.Int32, charInfo.ModelInfo.Sex);
                        dbManager.CreateInParameter(dbCmd, "job", System.Data.DbType.Int16, job);
                        dbManager.CreateInParameter(dbCmd, "level", System.Data.DbType.Int32, 1);
                        dbManager.CreateInParameter(dbCmd, "x", System.Data.DbType.Single, startPos.X);
                        dbManager.CreateInParameter(dbCmd, "y", System.Data.DbType.Single, startPos.Y);
                        dbManager.CreateInParameter(dbCmd, "textureId", System.Data.DbType.Int32, charInfo.ModelInfo.TextureId);
                        dbManager.CreateInParameter(dbCmd, "hairId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[0]);
                        dbManager.CreateInParameter(dbCmd, "faceId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[1]);
                        dbManager.CreateInParameter(dbCmd, "bodyId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[2]);
                        dbManager.CreateInParameter(dbCmd, "handsId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[3]);
                        dbManager.CreateInParameter(dbCmd, "feetId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[4]);
                        dbManager.CreateInParameter(dbCmd, "skinColor", System.Data.DbType.UInt32, charInfo.SkinColor);

                        try
                        {
                            dbCmd.Connection.Open();
                            charId = Convert.ToInt32(dbCmd.ExecuteScalar());

                            Inventory.InsertItem(charId, startWeapon, true);
                            Inventory.InsertItem(charId, startClothes, true);
                            Inventory.InsertItem(charId, startBag, true);
                        }
                        catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); result = false; }
                        finally { dbCmd.Connection.Close(); }
                    }
                }
                return result;
            }
        }

		/// <summary>
		/// Deletes a character from this account
		/// </summary>
		/// <param name="player"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		internal static bool Delete(Player player, string name)
		{
			bool result = true;

			using (DBManager dbManager = new DBManager(Databases.User))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand((Settings.KeepDeletedCharacters ? 3 : 4)))
				{
					if (Settings.KeepDeletedCharacters)
						dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.Int32, player.AccountId);
					dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, name);

					try
					{
						dbCmd.Connection.Open();
						dbCmd.ExecuteNonQuery();
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); result = false; }
					finally { dbCmd.Connection.Close(); }
				}
			}

			return result;
		}

		/// <summary>
		/// Loads character data into player
		/// and sends to client
		/// </summary>
		/// <param name="player"></param>
		/// <param name="name"></param>
		/// <param name="race"></param>
		internal static void Login(Player player, string name, byte race)
		{
            using (DBManager dbManager = new DBManager(Databases.User))
            {
                #region Character Info load
                using (DbCommand dbCmd = dbManager.CreateCommand(5))
                {
                    dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.String, player.AccountId);
                    dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
                    dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, name);

                    int off = 0;

                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader reader = dbCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                player.CharacterId = (int)reader[off++]; // 0
                                off++; // AccountId // 1
                                player.Name = (string)reader[off++]; // 3
                                player.PartyId = (int)reader[off++]; // 4
                                player.GuildId = (int)reader[off++]; // 5
                                off++; // PrevGuildId // 6
                                // 7~10
                                player.Position = new Position((int)reader[off++], (int)reader[off++], (int)reader[off++], (byte)reader[off++]);
                                player.Race = (byte)reader[off++]; // 11
                                player.Sex = (int)reader[off++]; // 12
                                player.Level = (int)reader[off++]; // 13
                                player.MaxReachedLevel = (int)reader[off++]; // 14
                                player.Exp = (long)reader[off++]; // 15
                                player.LastDecreasedExp = (long)reader[off++]; // 16
                                player.Hp = (int)reader[off++]; // 17
                                player.Mp = (short)(int)reader[off++]; // 18 // TODO :FIX
                                player.Stamina = (int)reader[off++]; // 19
                                player.Havoc = (int)reader[off++]; // 20
                                player.Job = (short)reader[off++]; // 21
                                player.JobDepth = (byte)reader[off++]; // 22
                                player.JobLevel = (int)reader[off++]; // 23
                                player.Jp = (int)reader[off++]; // 24
                                player.TotalJp = (int)reader[off++]; // 25
                                for (int i = 0; i < 3; i++)
                                { // Previous Jobs
                                    player.PrevJobs[i].Id = (int)reader[off++]; // 26, 28, 30
                                    player.PrevJobs[i].Level = (int)reader[off++]; // 27, 29, 31
                                }
                                player.ImmoralPoints = (decimal)reader[off++]; // 32
                                player.Cha = (int)reader[off++]; // 33
                                player.Pkc = (int)reader[off++]; // 34
                                player.Dkc = (int)reader[off++]; // 35
                                player.Huntaholic.Points = (int)reader[off++]; // 36
                                player.Huntaholic.EnterCount = (int)reader[off++]; // 37
                                player.Gold = (long)reader[off++]; // 38
                                player.Chaos = (int)reader[off++]; // 39
                                player.SkinColor = (uint)reader[off++]; // 40
                                player.HairId = (int)reader[off++]; // 41
                                player.FaceId = (int)reader[off++]; // 42
                                player.BodyId = (int)reader[off++]; // 43
                                player.HandsId = (int)reader[off++]; // 44
                                player.FeetId = (int)reader[off++]; // 45
                                player.TextureId = (int)reader[off++]; // 46
                                for (int i = 0; i < 6; i++)
                                    player.Belt[i].Id = (long)reader[off++]; // 47, 48, 49, 50, 51, 52
                                for (int i = 0; i < 6; i++)
                                    player.Summon[i].Id = (int)reader[off++]; // 53, 54, 55, 56, 57, 58
                                player.MainSummon = (int)reader[off++]; // 59
                                player.SubSummon = (int)reader[off++]; // 60
                                player.RemainSummonTime = (int)reader[off++]; // 61
                                player.Pet = (int)reader[off++]; // 62
                                off++; // Create Date // 63
                                off++; // Delete Date // 64
                                off++; // Login Time // 65
                                off++; // Login count // 66
                                off++; // Logout Time // 67
                                off++; // Play Time // 68
                                player.ChatBlockTime = (int)reader[off++]; // 69
                                player.AdvChatCount = (int)reader[off++]; // 70
                                off++; // NameChanged // 71
                                off++; // Auto User // 72
                                player.GuildBlockTime = (int)reader[off++]; // 73
                                player.PkMode = (byte)reader[off++]; // 74
                                off++; // Otp Value // 75
                                off++; // Otp Date // 76
                                player.ClientInfo = (string)reader[off++]; // 77
                            }
                        }
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message + " (Offset: " + off + ")"); }
                    finally { dbCmd.Connection.Close(); }
                }

                #endregion

                #region Update login_time
                using (DbCommand dbCmd = dbManager.CreateCommand(7))
                {
                    dbManager.CreateInParameter(dbCmd, "cid", System.Data.DbType.Int32, player.CharacterId);
                    try
                    {
                        dbCmd.Connection.Open();
                        dbCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError("Failed to Update LoginTime. Error {0}", ex.Message); }
                    finally { dbCmd.Connection.Close(); }
                }
                #endregion

                player.Stats.Load(player);
                ClientPackets.Instance.StatInfo(player, player.Stats, player.Attributes, false);
                ClientPackets.Instance.StatInfo(player, player.BonusStats, player.BonusAttributes, true);

                //ClientPackets.send_Login_pre1(player);

                // Load Inventory
                Inventory.Load(player);

                //ClientPackets.send_Login_pre1(player);

                ClientPackets.send_Login_pre2(player);

                ClientPackets.Instance.LoginResult(player);

                ClientPackets.Instance.StatInfo(player, player.Stats, player.Attributes, false);
                ClientPackets.Instance.StatInfo(player, player.BonusStats, player.BonusAttributes, true);

                ClientPackets.Instance.InventoryList(player, player.InventoryHandles);
                ClientPackets.Instance.EquipSummon(player, player.Summon, false);

                ClientPackets.Instance.WearInfo(player, player.WearInfo);
                ClientPackets.Instance.GoldUpdate(player, player.Gold, player.Chaos);
                ClientPackets.Instance.Property(player, "chaos", player.Chaos, true);
                ClientPackets.Instance.LevelUpdate(player, player.Level, player.JobLevel);
                ClientPackets.Instance.ExpUpdate(player, player.Exp, player.Jp);

                ClientPackets.Instance.Property(player, "job", player.Job, true);
                ClientPackets.Instance.Property(player, "job_level", player.JobLevel, true);
                ClientPackets.Instance.Property(player, "job_0", player.PrevJobs[0].Id, true);
                ClientPackets.Instance.Property(player, "jlv_0", player.PrevJobs[0].Level, true);
                ClientPackets.Instance.Property(player, "job_1", player.PrevJobs[1].Id, true);
                ClientPackets.Instance.Property(player, "jlv_1", player.PrevJobs[1].Level, true);
                ClientPackets.Instance.Property(player, "job_2", player.PrevJobs[2].Id, true);
                ClientPackets.Instance.Property(player, "jlv_2", player.PrevJobs[2].Level, true);

                ClientPackets.Instance.BeltSlotInfo(player, player.Belt);

                ClientPackets.send_Login_pre3(player);

                ClientPackets.Instance.Property(player, "huntaholic_ent", player.Huntaholic.EnterCount, true);
                ClientPackets.Instance.Property(player, "dk_count", player.Dkc, true);
                ClientPackets.Instance.Property(player, "pk_count", player.Pkc, true);
                ClientPackets.Instance.Property(player, "immoral", player.ImmoralPoints, true);
                ClientPackets.Instance.Property(player, "stamina", player.Stamina, true);
                ClientPackets.Instance.Property(player, "max_stamina", player.MaxStamina, true);
                ClientPackets.Instance.Property(player, "channel", player.Cha, true);

                ClientPackets.Instance.StatusChange(player, player.Handle, 0);

                ClientPackets.Instance.QuestList(player); // TODO : Incomplete packet

                ClientPackets.Instance.Chat(player, "@FRIEND", 0x8C, "FLIST|");
                ClientPackets.Instance.Chat(player, "@FRIEND", 0x8C, "DLIST|");

                // TODO : what is this used for?
                ClientPackets.Instance.Property(player, "playtime", 0, true);
                ClientPackets.Instance.Property(player, "playtime_limit1", 0, true);
                ClientPackets.Instance.Property(player, "playtime_limit2", 0, true);

                // TODO : Get new location by position
                ClientPackets.Instance.ChangeLocation(player, 0, 100302);
                // TODO : Get proper weather info and region
                ClientPackets.Instance.WeatherInfo(player, 100302, 1);

                ClientPackets.send_Login_pre4(player);

                ClientPackets.Instance.Property(player, "client_info", player.ClientInfo, false);
                ClientPackets.Instance.Property(player, "stamina_regen", player.StaminaRegen, true);
            }

            // TODO : PlaceHolder Data, must be replaced with real one
            //ClientPackets.send_Login(player);
        }
	}
}
