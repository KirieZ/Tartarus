// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Auth.Network;
using Common;
using Common.Utilities;
using System;
using System.Data.Common;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Auth
{
	/// <summary>
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		static XDes Des = new XDes(Globals.DESKey);

		public NetworkData NetData { get; set; }
		
		// User Info
		public int AccountId { get; set; }
		public string UserId { get; set; }
		public byte Permission { get; set; }
		public ushort LastServerId { get; set; }

		public long Key { get; set; }

		public GameClient(Socket socket)
		{
			this.NetData = new NetworkData(socket);
		}

		#region Login
		/// <summary>
		/// Normal Login
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cryptedPass"></param>
		internal void Login(string userId, byte[] cryptedPass)
		{
			string userPass = Des.Decrypt(cryptedPass).Trim('\0');

			this.AccountId = -1;

			if (Settings.UseMD5)
			{
				using (MD5 md5Hash = MD5.Create())
				{
					byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(userPass));

					StringBuilder sBuilder = new StringBuilder();

					for (int i = 0; i < data.Length; i++)
					{
						sBuilder.Append(data[i].ToString("x2"));
					}

					userPass = sBuilder.ToString();
				}
			}

            using (DBManager dbManager = new DBManager(Databases.Auth))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand(0))
                {
                    dbManager.CreateInParameter(dbCmd, "id", System.Data.DbType.String, userId);
                    dbManager.CreateInParameter(dbCmd, "pass", System.Data.DbType.String, userPass);
                    try
                    {
                        dbCmd.Connection.Open();

                        using (DbDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.AccountId = (int)reader[0];
                                this.UserId = (string)reader[1];
                                this.Permission = (byte)reader[3];
                                this.LastServerId = Convert.ToUInt16((int)reader[4]);
                            }
                        }
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
                    finally { dbCmd.Connection.Close(); }
                }
            }

			if (this.AccountId >= 0)
			{
				ClientPackets.Instance.Result(this, 0); // Success
			}
			else
			{
				ClientPackets.Instance.Result(this, 1);  // Fail
			}
		}

		/// <summary>
		/// IMBC Login
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="otp"></param>
		internal void IMBCLogin(string userId, string otp)
		{
			int accId = 0;
			byte perm = 0;
			ushort svId = 0;

			using (DBManager dbManager = new DBManager(Databases.Auth))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand(1))
				{
					dbManager.CreateInParameter(dbCmd, "id", System.Data.DbType.String, userId);
					try
					{
						dbCmd.Connection.Open();

						using (DbDataReader reader = dbCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								accId = (int)reader[0];
								perm = (byte)reader[3];
								svId = Convert.ToUInt16((int)reader[4]);
							}
						}
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
					finally { dbCmd.Connection.Close(); }
				}
			}

			if (accId > 0)
			{ // Account exists, lets check otp
				string dbOtp = "";

				using (DBManager dbManager = new DBManager(Databases.Auth))
				{
					using (DbCommand dbCmd = dbManager.CreateCommand(2))
					{
						dbManager.CreateInParameter(dbCmd, "acc", System.Data.DbType.String, accId);
						try
						{
							dbCmd.Connection.Open();

							using (DbDataReader reader = dbCmd.ExecuteReader())
							{
								while (reader.Read())
								{
									dbOtp = (string)reader[1];
								}
							}
						}
						catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
						finally { dbCmd.Connection.Close(); }
					}
				}

				if (dbOtp.Length > 0 && dbOtp.Equals(otp))
				{ // Valid otp, remove entry
					using (DBManager dbManager = new DBManager(Databases.Auth))
					{
						using (DbCommand dbCmd = dbManager.CreateCommand(3))
						{
							dbManager.CreateInParameter(dbCmd, "acc", System.Data.DbType.String, accId);
							try
							{
								dbCmd.Connection.Open();
								dbCmd.ExecuteNonQuery();
							}
							catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
							finally { dbCmd.Connection.Close(); }
						}
					}

					this.AccountId = accId;
					this.UserId = userId;
					this.Permission = perm;
					this.LastServerId = svId;

					ClientPackets.Instance.Result(this, 0); // Success
					return;
				}
			}

			ClientPackets.Instance.Result(this, 1);  // Fail
			return;
		}
		#endregion

		internal void JoinServer(ushort index)
		{
			if (!Server.Instance.GameServers.ContainsKey(index))
			{
				ConsoleUtils.ShowWarning("User '{0}' trying to join invalid server {1}", this.UserId, index);
				return;
			}

			// Updates last_serverid
			using (DBManager dbManager = new DBManager(Databases.Auth))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand(4))
				{
					dbManager.CreateInParameter(dbCmd, "sid", System.Data.DbType.Int32, (int)index);
					dbManager.CreateInParameter(dbCmd, "acc", System.Data.DbType.Int32, this.AccountId);
					try
					{
						dbCmd.Connection.Open();
						dbCmd.ExecuteNonQuery();
					}
					catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
					finally { dbCmd.Connection.Close(); }
				}
			}

			Server.Instance.GameServers[index].UserJoin(this);
		}
	}
}
