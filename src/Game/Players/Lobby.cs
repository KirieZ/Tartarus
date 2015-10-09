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
								chara.ModelInfo.Race = (int)reader[11];
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
		/// Checks if this character name exists
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		internal static bool NameExists(string name)
		{
			bool exists = false;

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT char_id FROM Characters WHERE name = @name"))
				{
					dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, name);
					try
					{
						dbCmd.Connection.Open();
						using (DbDataReader reader = dbCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								exists = true;
							}
						}
					}
					// When an error occurs, say that name exists to block creation
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); exists = true; }
					finally { dbCmd.Connection.Close(); }
				}
			}

			return exists;
		}

		internal static bool Create(Player player, LobbyCharacterInfo charInfo)
		{
			// Ensures the name is available
			if (NameExists(charInfo.Name))
				return false;

			bool result = true;

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("INSERT INTO Characters (account_id, name, race, sex, texture_id, hair_id, face_id, body_id, hands_id, feet_id, skin_color, create_date) VALUES (@accId, @name, @race, @sex, @textureId, @hairId, @faceId, @bodyId, @handsId, @feetId, @skinColor, @createDate)"))
				{
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.Int32, player.AccountId);
					dbManager.CreateInParameter(dbCmd, "name", System.Data.DbType.String, charInfo.Name);
					dbManager.CreateInParameter(dbCmd, "race", System.Data.DbType.Int32, charInfo.ModelInfo.Race);
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
	}
}
