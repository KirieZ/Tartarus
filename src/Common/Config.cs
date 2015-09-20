// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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
				ConsoleUtils.ShowError("Config file not found at '{0}'. Skipping...\n", filename);
				return false;
			}
			
			try
			{
				string file = File.ReadAllText(filename);

				string blockComments = @"/\*(.*?)\*/";
				string lineComments = @"//(.*?)\r?\n";
				string strings = @"""((\\[^\n]|[^""\n])*)""";
				string verbatimStrings = @"@(""[^""]*"")+";

				// Remove comments
				file = Regex.Replace(file,
					blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
					me =>
					{
						if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
							return me.Value.StartsWith("//") ? Environment.NewLine : "";
						// Keep the literal strings
						return me.Value;
					},
					RegexOptions.Singleline
				);

				// Get configs
				MatchCollection confs = Regex.Matches(file, "(.*?):(.*)", RegexOptions.Multiline);

				for (int i = 0; i < confs.Count; i++)
				{
					string confName = confs[i].Groups[1].Value.Trim(' ');
					string confValue = confs[i].Groups[2].Value.Trim(' ').Trim('"');

					if (confName.Equals("import"))
					{ // import keyword - imports another config file
						Read(confValue);
					}
					else
					{
						if (Data.ContainsKey(confName)) // Repeated key, overwrite
							Data[confName] = confValue;
						else // New key, add
							Data.Add(confName, confValue);
					}
				}
			}
			catch (Exception e)
			{
				ConsoleUtils.ShowError("Failed to read config file at '{0}'", filename);
				ConsoleUtils.ShowError("Error: {0}", e.Message);
				return false;
			}
			return true;
		}
	}
}
