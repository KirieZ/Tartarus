using Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Game;
using System.Net.Sockets;
using Game.Network;
using Game.Players;

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
			List<LobbyCharacterInfo> charList = new List<LobbyCharacterInfo>();

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM Characters WHERE account_id = @accId AND delete_date > @now LIMIT 5"))
				{
					dbManager.CreateInParameter(dbCmd, "accId", System.Data.DbType.String, this.AccountId);
					dbManager.CreateInParameter(dbCmd, "now", System.Data.DbType.DateTime, DateTime.UtcNow);
					try
					{
						dbCmd.Connection.Open();

						using (DbDataReader reader = dbCmd.ExecuteReader())
						{
							while (reader.Read())
							{
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
								chara.Permission = this.Permission;
								chara.IsBanned = false;
								chara.SkinColor = (uint)reader[40];
								chara.CreateTime = ((DateTime)reader[64]).ToString("yyyy/MM/dd");
								chara.DeleteTime = "9999/12/31";
								// TODO : chara.WearItemEnhanceInfo
								// TODO : chara.WearItemLevelInfo =
								// TODO : chara.WearItemElementalType =

								charList.Add(chara);
							}
						}
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
					finally { dbCmd.Connection.Close(); }
				}
			}

			ClientPackets.Instance.CharacterList(this, charList.ToArray());
		}
	}
}