// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
	/// <summary>
	/// Class use to load config files and store in a Dictionary
	/// </summary>
	public class Config
	{
		public Dictionary<string, string> Data;

		public Config()
		{
			Data = new Dictionary<string, string>();
		}

		/// <summary>
		/// Reads a config file.
		/// </summary>
		/// <param name="filename">the file directory</param>
		/// <returns></returns>
		public bool Read(string filename)
		{
			if (!File.Exists(filename)) {
				//ConsoleUtils.Write(ConsoleMsgType.Error, "Config file note found '{0}'... Skipping...\n", filename);
				return false;
			}

			try
			{
				string[] confs = File.ReadAllLines(filename);

				for (int i = 0; i < confs.Length; i++)
				{
					confs[i] = confs[i].TrimStart(' ');
					if (confs[i].StartsWith("//")) continue;
					else if (confs[i].Trim(' ') == "") continue;

					string[] confInfo = confs[i].Split(new char[] {':'}, 2);
					if (Data.ContainsKey(confInfo[0]))
					{
						Data[confInfo[0]] = confInfo[1].TrimEnd(' ').TrimStart(' ');
					}
					else
					{
						Data.Add(confInfo[0], confInfo[1].TrimEnd(' ').TrimStart(' '));
					}
				}
			}
			catch (Exception e)
			{
				//ConsoleUtils.Write(ConsoleMsgType.Error, "Failed to read config file at '{0}'", filename);
				//ConsoleUtils.Write(ConsoleMsgType.Error, "Error: {0}", e.Message);
				return false;
			}
			return true;
		}
	}
}
