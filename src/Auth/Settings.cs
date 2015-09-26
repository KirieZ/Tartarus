// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
	/// <summary>
	/// Contains Auth-Server settings loaded from config files
	/// </summary>
	public static class Settings
	{
		// Auth Server Settings
		public static String ServerIP;
		public static Int16 Port;
		public static String AcceptorKey;
		public static Int16 GameServerPort;

		// Database Settings
		public static Int32 SqlEngine;

		public static String SqlIp;
		public static Int16 SqlPort;
		public static String SqlDatabase;
		public static String SqlUsername;
		public static String SqlPassword;

		// Other Settings
		public static Boolean LoginDebug;

		/// <summary>
		/// Types of Settings
		/// </summary>
		private enum DType
		{
			String,
			Bool,
			Byte,
			Int16,
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
			Port = (Int16)ParseSetting(ref settings, DType.Int16, "server_port", (Int16) 8841);
			AcceptorKey = (String)ParseSetting(ref settings, DType.String, "acceptor_key", "secret");
			GameServerPort = (Int16)ParseSetting(ref settings, DType.Int16, "gameserver_port", (Int16)4444);

			// Loads default SQL Settings
			String defaultSqlHost = (String) ParseSetting(ref settings, DType.String, "sql.hostname", "127.0.0.1");
			Int16 defaultSqlPort = (Int16)ParseSetting(ref settings, DType.Int16, "sql.port", (Int16)3306);
			String defaultSqlUser = (String) ParseSetting(ref settings, DType.String, "sql.username", "rappelz");
			String defaultSqlPass = (String) ParseSetting(ref settings, DType.String, "sql.password", "rappelz");
			String defaultSqlDb = (String) ParseSetting(ref settings, DType.String, "sql.database", "rappelz");

			// Database Engine
			SqlEngine = (Int32)ParseSetting(ref settings, DType.Int32, "sql.engine", 1);

			// Auth Database Settings
			SqlIp = (String) ParseSetting(ref settings, DType.String, "sql.auth_hostname", defaultSqlHost, true);
			SqlPort = (Int16)ParseSetting(ref settings, DType.Int16, "sql.auth_port", defaultSqlPort, true);
			SqlUsername = (String) ParseSetting(ref settings, DType.String, "sql.auth_username", defaultSqlUser, true);
			SqlPassword = (String) ParseSetting(ref settings, DType.String, "sql.auth_password", defaultSqlPass, true);
			SqlDatabase = (String) ParseSetting(ref settings, DType.String, "sql.auth_database", defaultSqlDb, true);

			// Other Settings
			LoginDebug = (Boolean)ParseSetting(ref settings, DType.Bool, "login_debug", false);
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
