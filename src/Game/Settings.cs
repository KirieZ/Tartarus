// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	/// <summary>
	/// Contains Game-Server settings loaded from config files
	/// </summary>
	public static class Settings
	{
		// Game Server Settings
		public static String ServerIP;
		public static UInt16 Port;
		public static String AcceptorKey;
		public static UInt16 AuthServerPort;
		public static string AuthServerIP;
		private static int MaxConnections;
		
		// Server Info
		public static ushort Index;
		public static string Name;
		public static string Notice;

		// Database Settings
		public static String SqlIp;
		public static UInt16 SqlPort;
		public static String SqlDatabase;
		public static String SqlUsername;
		public static String SqlPassword;

		/// <summary>
		/// Types of Settings
		/// </summary>
		private enum DType
		{
			String,
			Bool,
			Byte,
			Int16,
			UInt16,
			Int32
		}

		/// <summary>
		/// Parses the values from the dictionary and
		/// add them to the variables
		/// </summary>
		/// <param name="settings">settings dictionary</param>
		public static void Set(Dictionary<string, string> settings)
		{
			// Server Settings
			ServerIP = (String) ParseSetting(ref settings, DType.String, "server_ip", "127.0.0.1");
			Port = (UInt16)ParseSetting(ref settings, DType.UInt16, "server_port", (UInt16) 6900);
			AcceptorKey = (String)ParseSetting(ref settings, DType.String, "acceptor_key", "secret");
			AuthServerIP = (String)ParseSetting(ref settings, DType.String, "auth_ip", "127.0.0.1");
			AuthServerPort = (UInt16)ParseSetting(ref settings, DType.UInt16, "auth_port", (UInt16)4444);
			MaxConnections = (Int32)ParseSetting(ref settings, DType.Int32, "max_connections", (Int32)10000);

			// Server Info
			Index = (UInt16)ParseSetting(ref settings, DType.UInt16, "server_index", (UInt16)1);
			Name = (String)ParseSetting(ref settings, DType.String, "server_name", "Tartarus");
			Notice = (String)ParseSetting(ref settings, DType.String, "notice_url", "http://127.0.0.1/notice.htm");

			// Loads default SQL Settings
			String defaultSqlHost = (String) ParseSetting(ref settings, DType.String, "sql.hostname", "127.0.0.1");
			UInt16 defaultSqlPort = (UInt16)ParseSetting(ref settings, DType.UInt16, "sql.port", (UInt16)3306);
			String defaultSqlUser = (String) ParseSetting(ref settings, DType.String, "sql.username", "rappelz");
			String defaultSqlPass = (String) ParseSetting(ref settings, DType.String, "sql.password", "rappelz");
			String defaultSqlDb = (String) ParseSetting(ref settings, DType.String, "sql.database", "rappelz");

			// Game Database Settings
			SqlIp = (String) ParseSetting(ref settings, DType.String, "sql.game_hostname", defaultSqlHost, true);
			SqlPort = (UInt16)ParseSetting(ref settings, DType.UInt16, "sql.game_port", defaultSqlPort, true);
			SqlUsername = (String) ParseSetting(ref settings, DType.String, "sql.game_username", defaultSqlUser, true);
			SqlPassword = (String) ParseSetting(ref settings, DType.String, "sql.game_password", defaultSqlPass, true);
			SqlDatabase = (String) ParseSetting(ref settings, DType.String, "sql.game_database", defaultSqlDb, true);
		}

		/// <summary>
		/// Parse a setting by converting to its value type
		/// and adding defaults when necessary
		/// </summary>
		/// <param name="settings">the settings dictionary</param>
		/// <param name="type">type of return</param>
		/// <param name="name">name of the setting</param>
		/// <param name="defaultValue">default value to be used when setting can't be</param>
		/// <returns></returns>
		private static object ParseSetting(ref Dictionary<string, string> settings, DType type, string name, object defaultValue, bool optional = false)
		{
			if (!settings.ContainsKey(name))
			{
				if (!optional)
				{
					ConsoleUtils.ShowWarning("Couldn't find config {0}", name);
				}
				return defaultValue;
			}

			switch (type)
			{
				case DType.Bool:
					return GetBool(settings[name]);

				case DType.Byte:
					return GetByte(settings[name], (byte)defaultValue);

				case DType.Int16:
					return GetInt16(settings[name], (short)defaultValue);

				case DType.UInt16:
					return GetUInt16(settings[name], (ushort)defaultValue);

				case DType.Int32:
					return GetInt32(settings[name], (int)defaultValue);

				case DType.String:
					return settings[name];

				default:
					return defaultValue;
			}
		}

		private static Boolean GetBool(string value)
		{
			if (value.Equals("1")) return true;
			else return false;
		}

		private static Int32 GetInt32(string value, Int32 defaultVal)
		{
			int val;
			if (Int32.TryParse(value, out val))
			{
				return val;
			}
			else
			{
				ConsoleUtils.ShowWarning("Couldn't parse value {0}, defaulting to {1}", value, defaultVal);
				return defaultVal;
			}
		}

		private static Int16 GetInt16(string value, Int16 defaultVal)
		{
			short val;
			if (Int16.TryParse(value, out val))
			{
				return val;
			}
			else
			{
				ConsoleUtils.ShowWarning("Couldn't parse value {0}, defaulting to {1}\n", value, defaultVal);
				return defaultVal;
			}
		}

		private static UInt16 GetUInt16(string value, UInt16 defaultVal)
		{
			ushort val;
			if (UInt16.TryParse(value, out val))
			{
				return val;
			}
			else
			{
				ConsoleUtils.ShowWarning("Couldn't parse value {0}, defaulting to {1}\n", value, defaultVal);
				return defaultVal;
			}
		}

		private static Byte GetByte(string value, Byte defaultVal)
		{
			byte val;
			if (Byte.TryParse(value, out val))
			{
				return val;
			}
			else
			{
				ConsoleUtils.ShowWarning("Couldn't parse value {0}, defaulting to {1}\n", value, defaultVal);
				return defaultVal;
			}
		}
	}
}
