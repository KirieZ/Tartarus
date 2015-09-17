// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
	public static class ConsoleCommands
	{
		public delegate void CommandAction(object[] args);

		/// <summary>
		/// Describes a Console Command
		/// </summary>
		public class Command
		{
			/// <summary>
			/// The command function
			/// </summary>
			public CommandAction Action { get; private set; }
			/// <summary>
			/// A regex for command arguments
			/// </summary>
			public string Args { get; private set; }

			/// <summary>
			/// Creates a new Console Command
			/// </summary>
			/// <param name="args">the argument types</param>
			/// <param name="act">command action</param>
			public Command(string args, CommandAction act)
			{
				this.Action = act;
				StringBuilder argRegex = new StringBuilder();

				// Check each argument and build into regex string
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i].Equals('i'))		// Int
						argRegex.Append(" (.*?)");
					else if (args[i].Equals('s'))	// String
						argRegex.Append(" \"(.*?)\"");
				}

				this.Args = argRegex.ToString().TrimStart(' ');
			}
		}

		private static Dictionary<string, Command> Commands = new Dictionary<string, Command>();

		/// <summary>
		/// Called to set the commands list
		/// </summary>
		/// <param name="cmds"></param>
		public static void Load(Dictionary<string, Command> cmds)
		{
			Commands = cmds;
		}

		/// <summary>
		/// Called when a input is received by the console
		/// </summary>
		/// <param name="input">the input string</param>
		public static void OnInputReceived(string input)
		{
			string[] cmdData = input.Split(new char[] {' '}, 2);

			// Ensure that the command exists
			if (!Commands.ContainsKey(cmdData[0]))
			{
				// TODO : Message
				//ConsoleUtils.Write(ConsoleMsgType.Error, "Command {0} not found.\n", cmdData[0]);
				return;
			}

			// Retrieve parameters
			object[] parameters;

			if (cmdData.Length > 1)
			{
				Match args = Regex.Match(cmdData[1] + " ", Commands[cmdData[0]].Args + " ");

				// Adds the parameters for the command
				parameters = new object[args.Groups.Count - 1];
				for (int i = 0; i < parameters.Length; i++)
					parameters[i] = args.Groups[i + 1].Value;

			}
			else
			{
				parameters = new object[0];
			}

			// Do command
			Commands[cmdData[0]].Action(parameters);
		}
	}
}