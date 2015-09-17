// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Game
{
	/// <summary>
	/// Server main class
	/// </summary>
	public class Server : ServerBase
	{
		public static readonly Server Instance = new Server();

		/// <summary>
		/// Loads Configs and ConsoleCommands
		/// </summary>
		public override void Load()
		{
			ConsoleUtils.ShowHeader();

			ConsoleUtils.ShowStatus("Loading Console Commands... \n");
			ConsoleCommands.Load(GetConsoleCommands());
			ConsoleUtils.ShowStatus("Console Commands Loaded\n");
		}

		/// <summary>
		/// Server starting
		/// </summary>
		public override void Start()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a dictionary with console commands
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, ConsoleCommands.Command> GetConsoleCommands()
		{
			Dictionary<string, ConsoleCommands.Command> cmdList = new Dictionary<string, ConsoleCommands.Command>();

			// TODO : Command Calls
			cmdList.Add("Debug.log", new ConsoleCommands.Command("i", null));
			cmdList.Add("Debug.log_section", new ConsoleCommands.Command("i", null));

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

			return cmdList;
		}
	}
}
