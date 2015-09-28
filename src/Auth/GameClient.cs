// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.RC4;
using System.Data.Common;
using Common.Utilities;
using System.Security.Cryptography;
namespace Auth
{
	/// <summary>
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		static int sqlConType = Settings.SqlEngine;
		static string sqlConString = "Server="+Settings.SqlIp+";Database="+Settings.SqlDatabase+";UID="+Settings.SqlUsername+";PWD="+Settings.SqlPassword+";Connection Timeout=5;";
		static XDes Des = new XDes(Globals.DESKey);

		// Network Data
		public Socket ClSocket;
		public byte[] Buffer;
		public PacketStream Data;
		public int PacketSize;
		public int Offset;
		public XRC4Cipher InCipher;
		public XRC4Cipher OutCipher;

		// User Info
		public int AccountId;
		public string UserId;
		public byte Permission;
		public ushort LastServerId;

		public byte[] Key;

		public GameClient(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
			this.InCipher = new XRC4Cipher(Globals.RC4Key);
			this.OutCipher = new XRC4Cipher(Globals.RC4Key);
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

            using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
            {
                using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM login WHERE userid = @id AND password = @pass"))
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

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM login WHERE userid = @id"))
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

				using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
				{
					using (DbCommand dbCmd = dbManager.CreateCommand("SELECT * FROM otp WHERE account_id = @acc"))
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
					using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
					{
						using (DbCommand dbCmd = dbManager.CreateCommand("DELETE FROM otp WHERE account_id = @acc"))
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
			Server.Instance.GameServers[index].UserJoin(this);
		}
	}
}
