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

namespace Game.Players
{
	/// <summary>
	/// Handles Player's lobby activity
	/// </summary>
	public static class Lobby
	{
		static int sqlConType = Settings.SqlEngine;
		static string sqlConString = "Server=" + Settings.SqlUserIp + ";Database=" + Settings.SqlUserDatabase + ";UID=" + Settings.SqlUserUsername + ";PWD=" + Settings.SqlUserPassword + ";Connection Timeout=5;";

		/// <summary>
		/// Retrieves the character list
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		internal static LobbyCharacterInfo[] GetCharacterList(Player player)
		{
			List<LobbyCharacterInfo> charList = new List<LobbyCharacterInfo>();

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now LIMIT 5"))
				{
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.String, player.AccountId);
					dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
					try
					{
						dbCmd.Connection.Open();

						using (DbDataReader reader = dbCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								// Reads the character data
								LobbyCharacterInfo chara = new LobbyCharacterInfo();
								chara.Name = (string)reader[3];
								chara.ModelInfo.Race = (byte)reader[11];
								chara.ModelInfo.Sex = (int)reader[12];
								chara.ModelInfo.TextureId = (int)reader[46];
								for (int i = 0; i < 5; i++)
									chara.ModelInfo.ModelId[i] = (int)reader[41 + i];
								// TODO : chara.ModelInfo.Wear

								chara.Level = (int)reader[13];
								chara.Job = (short)reader[21];
								chara.JobLevel = (int)reader[23];
								// TODO : chara.ExpPercentage = (int)
								chara.Hp = (int)reader[17];
								chara.Mp = (int)reader[18];
								chara.Permission = player.Permission;
								chara.IsBanned = false;
								chara.SkinColor = (uint)reader[40];
								chara.CreateTime = ((DateTime)reader[64]).ToString("yyyy/MM/dd");
								chara.DeleteTime = "9999/12/31";
								// TODO : chara.WearItemEnhanceInfo
								// TODO : chara.WearItemLevelInfo =
								// TODO : chara.WearItemElementalType =

								// Add data to char List
								charList.Add(chara);
							}
						}
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
					finally { dbCmd.Connection.Close(); }
				}
			}

			return charList.ToArray();
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

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT char_id FROM Characters WHERE name = @name AND delete_date > @now"))
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
		internal static bool Create(Player player, LobbyCharacterInfo charInfo)
		{
			// Ensures the name is available
			if (!CheckCharacterName(charInfo.Name))
				return false;

			bool result = true;

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("INSERT INTO Characters (account_id, name, race, sex, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color, create_date) VALUES (@accId, @name, @race, @sex, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor, @createDate)"))
				{
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.Int32, player.AccountId);
					dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, charInfo.Name);
					dbManager.CreateInParameter(dbCmd, "race", System.Data.DbType.Byte, (byte)charInfo.ModelInfo.Race);
					dbManager.CreateInParameter(dbCmd, "sex", System.Data.DbType.Int32, charInfo.ModelInfo.Sex);
					dbManager.CreateInParameter(dbCmd, "textureId", System.Data.DbType.Int32, charInfo.ModelInfo.TextureId);
					dbManager.CreateInParameter(dbCmd, "hairId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[0]);
					dbManager.CreateInParameter(dbCmd, "faceId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[1]);
					dbManager.CreateInParameter(dbCmd, "bodyId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[2]);
					dbManager.CreateInParameter(dbCmd, "handsId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[3]);
					dbManager.CreateInParameter(dbCmd, "feetId", System.Data.DbType.Int32, charInfo.ModelInfo.ModelId[4]);
					dbManager.CreateInParameter(dbCmd, "skinColor", System.Data.DbType.UInt32, charInfo.SkinColor);
					dbManager.CreateInParameter(dbCmd, "createDate", System.Data.DbType.DateTime, DateTime.UtcNow);
					
					try
					{
						dbCmd.Connection.Open();
						dbCmd.ExecuteNonQuery();
						// TODO : Retrieve character ID
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); result = false; }
					finally { dbCmd.Connection.Close(); }
				}
			}

			return result;
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

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				string query = "";
				// Checks if character must be kept when deleting (change delete_date instead of removing the entry)
				if (Settings.KeepDeletedCharacters)
					query = "UPDATE Characters SET delete_date = @now WHERE account_id = @accId AND name = @name";
				else
					query = "DELETE FROM Characters WHERE account_id = @accId AND name = @name";

				using (DbCommand dbCmd = dbManager.CreateCommand(query))
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
			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now AND name = @name"))
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
							if(reader.Read())
							{
								player.CharacterId = (int)reader[off++]; // 0
								off++; // AccountId // 1
								off++; // slot // 2
								player.Name = (string)reader[off++]; // 3
								player.PartyId = (int)reader[off++]; // 4
								player.GuildId = (int)reader[off++]; // 5
								off++; // PrevGuildId // 6
								player.Position.X = (float)(int)reader[off++]; // 7
								player.Position.Y = (float)(int)reader[off++]; // 8
								player.Position.Z = (float)(int)reader[off++]; // 9
								player.Position.Layer = (byte)reader[off++]; // 10
								player.Race = (byte)reader[off++]; // 11
								player.Sex = (int)reader[off++]; // 12
								player.Level = (int)reader[off++]; // 13
								player.MaxReachedLevel = (int)reader[off++]; // 14
								player.Exp = (long)reader[off++]; // 15
								player.LastDecreasedExp = (long)reader[off++]; // 16
								player.Hp = (int)reader[off++]; // 17
								player.Mp = (int)reader[off++]; // 18
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

					// TODO : PlaceHolder Data, must be replaced with real one
					ClientPackets.send_Login(player);
				}
			}
		}
	}
}
