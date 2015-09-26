// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Auth
{
	/// <summary>
	/// Server main class
	/// </summary>
	public class Server : ServerBase
	{
		public static readonly Server Instance = new Server();

		/// <summary>
		/// Stores GameServers connected to this AuthServer
		/// </summary>
		public Dictionary<ushort, GameServer> GameServers;

		/// <summary>
		/// Loads Configs and ConsoleCommands
		/// </summary>
		public override void Load()
		{
			ConsoleUtils.ShowHeader();

			#region Settings Load
			ConsoleUtils.ShowStatus("Loading config files...");
			Config conf = new Config();
			bool error = false;

			if (!conf.Read("conf/auth-server.opt"))
				error = true;
			if (!conf.Read("conf/inter-server.opt"))
				error = true;

			if (error)
			{
				ConsoleUtils.ShowNotice("Fix config errors and restart the server.");
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
			#endregion
		}

		/// <summary>
		/// Server starting
		/// </summary>
		public override void Start()
		{
			#region Internal StartUp
			// TODO : DB Test
			this.GameServers = new Dictionary<ushort, GameServer>();
			#endregion

			#region Listener StartUp
			GameManager.Instance.Start();
			ClientManager.Instance.Start();
			#endregion
		}

		/// <summary>
		/// Called to register a new GameServer to servers list
		/// </summary>
		/// <param name="gs"></param>
		public void OnRegisterGameServer(ushort index, GameServer gs, string key)
		{
			ConsoleUtils.ShowInfo("Game Server '{0}' ({1}:{2}; index:{3}) is trying to connect...", gs.Name, gs.IP, gs.Port, index);

			if (this.GameServers.ContainsKey(index))
			{ // Is index available?
				ConsoleUtils.ShowWarning("Failed to add server to server list, duplicated index '{0}'.", index);
				GamePackets.Instance.RegisterResult(gs, 1);
				return;
			}
			else if (!key.Equals(Settings.AcceptorKey))
			{ // Is acceptor key valid?
				ConsoleUtils.ShowWarning("Connection refused, invalid Acceptor Key. (Received: {0}; Expected: {1})", key, Settings.AcceptorKey);
				GamePackets.Instance.RegisterResult(gs, 2);
				return;
			}

			GameServers.Add(index, gs);
			ConsoleUtils.ShowInfo("Connection with Game Server '{0}' stabilished.", gs.Name);
			GamePackets.Instance.RegisterResult(gs, 0);
			
			return;
		}
	}
}
