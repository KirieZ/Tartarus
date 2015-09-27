// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.Content;
using Game.Database;

namespace Game
{
	/// <summary>
	/// Server main class
	/// </summary>
	public class Server : ServerBase
	{
        static int sqlConType = 0;
        static string sqlConString = string.Empty;

		public static readonly Server Instance = new Server();

		private static Dictionary<string, byte[]> PendingUsers;

		/// <summary>
		/// Loads Configs and ConsoleCommands
		/// </summary>
		public override void Load()
		{
			#region Settings Load

			Config conf = new Config();
			bool error = false;

			if (!conf.Read("conf/game-server.opt"))
				error = true;
			if (!conf.Read("conf/inter-server.opt"))
				error = true;

			if (error)
			{
				ConsoleUtils.ShowError("Fix config errors and restart the server.");
				return;
			}

			Settings.Set(conf.Data);
			
			ConsoleUtils.ShowNotice("Config files loaded.");

			// Apply console filters aftere the notice that config files were loaded,
			// so if notice is disabled, it avoids the feel that configs were never loaded.
			if (conf.Data.ContainsKey("console_silent"))
			{
				int consoleSilent;
				if (Int32.TryParse(conf.Data["console_silent"], out consoleSilent))
					ConsoleUtils.SetDisplaySettings((ConsoleUtils.MsgType)consoleSilent);
				else
					ConsoleUtils.ShowError("Invalid 'console_silent' value. Defaulting to 0...");
			}

            sqlConType = Settings.SqlEngine;
            sqlConString = "Server=" + Settings.SqlGameIp + ";Database=" + Settings.SqlGameDatabase + ";UID=" + Settings.SqlGameUsername + ";PWD=" + Settings.SqlGamePassword + ";Connection Timeout=5;";

			#endregion
			
			ConsoleUtils.ShowHeader(Settings.WindowTitle);
			ConsoleCommands.Load(GetConsoleCommands());

            #region Content Load

            Arcadia.Initialize(sqlConType, sqlConString);

            #endregion
        }

		/// <summary>
		/// Server starting
		/// </summary>
		public override void Start()
		{
			PendingUsers = new Dictionary<string, byte[]>();

			AuthManager.Instance.Start();
		}

		/// <summary>
		/// Returns a dictionary with console commands
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, ConsoleCommands.Command> GetConsoleCommands()
		{
			Dictionary<string, ConsoleCommands.Command> cmdList = new Dictionary<string, ConsoleCommands.Command>();

			// TODO : Command Calls
			cmdList.Add("Debug.log", new ConsoleCommands.Command("i", null));//ConsoleHelper.Debug_LogToggle));
			cmdList.Add("Debug.log_section", new ConsoleCommands.Command("i", null));// ConsoleHelper.Debug_LogSetFilter));

			cmdList.Add("Debug.level", new ConsoleCommands.Command("i", null));
			cmdList.Add("Debug.trace", new ConsoleCommands.Command("i", null));
			
			cmdList.Add("Debug.folder", new ConsoleCommands.Command("s", null));
			cmdList.Add("Debug.logfileformat", new ConsoleCommands.Command("s", null));
			cmdList.Add("Debug.cprint_debug", new ConsoleCommands.Command("i", null));
			cmdList.Add("Debug.cprint_packet", new ConsoleCommands.Command("i", null));

			cmdList.Add("Debug.internal", new ConsoleCommands.Command("i", null));
			cmdList.Add("Debug.trace_internal", new ConsoleCommands.Command("i", null));

			cmdList.Add("Debug.packets", new ConsoleCommands.Command("i", null));
			//cmdList.Add("Debug.trace_packets", new ConsoleCommands.Command("
			cmdList.Add("Debug.trace_priority", new ConsoleCommands.Command("i", null));

			cmdList.Add("Windows.ShowDebug", new ConsoleCommands.Command("", ConsoleHelper.Windows_ShowDebug));

			cmdList.Add("Test", new ConsoleCommands.Command("s", Test));

			return cmdList;
		}

		private static void Test(object[] args)
		{
			ConsoleUtils.ShowDebug("Test");
			ConsoleUtils.ShowError("Test");
			ConsoleUtils.ShowFatalError("Test");
			ConsoleUtils.ShowInfo("Test");
			ConsoleUtils.ShowNotice("Test");
			ConsoleUtils.ShowSQL("Test");
			ConsoleUtils.ShowStatus("Test");
			ConsoleUtils.ShowWarning("Test");
		}

		/// <summary>
		/// Called when auth answers to server register
		/// </summary>
		/// <param name="result"></param>
		internal void RegisterResult(ushort result)
		{
			switch (result)
			{
				case 0: // Success
					ConsoleUtils.ShowInfo("Connection with Auth-Server stabilished.");
					break;

				case 1: // Duplicated Index
					ConsoleUtils.ShowWarning("Could not stabilish connection to Auth-Server, server index is already in use.");
					break;

				case 2: // Invalid Key
					ConsoleUtils.ShowWarning("Could not stabilish connection to Auth-Server, invalid acceptor key.");
					break;
			}
		}

		/// <summary>
		/// Adds a new pending user
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="key"></param>
		internal void PendingUser(string userId, byte[] key)
		{
			if (PendingUsers.ContainsKey(userId))
				PendingUsers[userId] = key;
			else
				PendingUsers.Add(userId, key);

			AuthPackets.Instance.JoinResult(userId, 0);
		}
	}
}
