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

			ConsoleUtils.ShowHeader(Settings.WindowTitle);

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

			ConsoleCommands.Load(GetConsoleCommands());
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
		/// Returns a dictionary with console commands
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, ConsoleCommands.Command> GetConsoleCommands()
		{
			Dictionary<string, ConsoleCommands.Command> cmdList = new Dictionary<string, ConsoleCommands.Command>();

			cmdList.Add("servers", new ConsoleCommands.Command("", ConsoleHelper.ServerList));
			cmdList.Add("Windows.ShowDebug", new ConsoleCommands.Command("", ConsoleHelper.Windows_ShowDebug));

			return cmdList;
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

			gs.Index = index;
			GameServers.Add(index, gs);
			ConsoleUtils.ShowInfo("Connection with Game Server '{0}' stabilished.", gs.Name);
			GamePackets.Instance.RegisterResult(gs, 0);
			
			return;
		}

		/// <summary>
		/// Called when a Game-Server disconnects from Auth
		/// </summary>
		/// <param name="gs"></param>
		internal void OnGameServerDisconnects(GameServer gs)
		{
			ConsoleUtils.ShowInfo("Game-Server '{0}' disconnected.", gs.Name);
			if (!GameServers.ContainsKey(gs.Index))
			{
				ConsoleUtils.ShowError("Failed to remove Game-Server from server list, invalid index {0}", gs.Index);
				return;
			}

			GameServers.Remove(gs.Index);
			gs.ClSocket.Close();
		}
	}
}
