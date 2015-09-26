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
namespace Auth
{
	/// <summary>
	/// Holds a GameClient data
	/// </summary>
	public class GameClient
	{
		static int sqlConType = 1;  //TODO : Load from configuration file
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
		public byte Permission;
		public ushort LastServerId;

		public GameClient(Socket socket)
		{
			this.ClSocket = socket;
			this.Buffer = new byte[Globals.MaxBuffer];
			this.Data = new PacketStream();
			this.InCipher = new XRC4Cipher(Globals.RC4Key);
			this.OutCipher = new XRC4Cipher(Globals.RC4Key);
		}

		internal static void UserLogin(GameClient client, string userId, byte[] cryptedPass)
		{
			string userPass = Des.Decrypt(cryptedPass).Trim('\0');

			client.AccountId = -1;

			using (DBManager dbManager = new DBManager(sqlConType, sqlConString))
			{
				using (DbConnection dbCon = dbManager.CreateConnection())
				{
					using (DbCommand dbCmd = dbCon.CreateCommand())
					{
						dbCmd.CommandText = "SELECT * FROM login WHERE userid = @id AND password = @pass";
						dbManager.CreateInParameter(dbCmd, "id", System.Data.DbType.String, userId);
						dbManager.CreateInParameter(dbCmd, "pass", System.Data.DbType.String, userPass);
						try
						{
							dbCon.Open();

							using (DbDataReader reader = dbCmd.ExecuteReader())
							{
								while (reader.Read())
								{
									client.AccountId = (int)reader[0];
									client.Permission = (byte)reader[3];
								}
							}
						}
						catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
						finally { dbCon.Close(); }
					}
				}
			}

			if (client.AccountId >= 0)
			{
				ClientPackets.Instance.Result(client, 0); // Success
			}
			else
			{
				ClientPackets.Instance.Result(client, 1);  // Fail
			}
		}
	}
}
