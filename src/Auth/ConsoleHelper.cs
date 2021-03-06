// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Forms;

namespace Auth
{
	/// <summary>
	/// Functions related to Auth-Server Console Commands
	/// </summary>
	public static class ConsoleHelper
	{
		/// <summary>
		/// Displays debug UI
		/// </summary>
		/// <param name="args"></param>
		internal static void Windows_ShowDebug(object[] args)
		{
			if (Common.GUI.debugGUI.Instance == null)
			{
				Common.GUI.consoleGUI.Instance.Invoke(new MethodInvoker(delegate
				{
					Common.GUI.debugGUI ui = new Common.GUI.debugGUI();
					ui.Show();
				}));
			}
			else
			{
				ConsoleUtils.ShowInfo("Debug GUI is already open.\n");
			}
		}

		/// <summary>
		/// Lists connected game-servers
		/// </summary>
		/// <param name="args"></param>
		internal static void ServerList(object[] args)
		{
			ConsoleUtils.ShowInfo("\tServer List");
			ConsoleUtils.ShowInfo("Index\tName\tIP:Port");
			
			foreach (GameServer gs in Server.Instance.GameServers.Values)
			{
				ConsoleUtils.ShowInfo("{0}\t{1}\t{2}:{3}", gs.Index, gs.Name, gs.IP, gs.Port);
			}

			return;
		}
	}
}
