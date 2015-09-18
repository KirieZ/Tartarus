using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.GUI
{
    public partial class debugGUI : Form
    {
		/// <summary>
		/// Provides a handle to report gui changes.
		/// </summary>
		public static debugGUI Instance;

        public debugGUI()
        {
            InitializeComponent();
			Instance = this;
        }

        /// <summary>
		/// Print a single message to the console
		/// </summary>
		/// <param name="message">Message class containing message info</param>
		internal void MessagePrint(ConsoleMessage message)
		{
			string assembledText = string.Empty;

			this.Invoke(new MethodInvoker(delegate
			{
				if (message.TextBlocks.Length > 0)
				{
					for (int i = 0; i < message.TextBlocks.Length; i++)
					{
						assembledText += message.TextBlocks[i];
					}

					messageConsole.Select(messageConsole.Text.Length, 0);
					messageConsole.SelectionBackColor = message.BackColor;
					messageConsole.SelectionColor = message.ForeColor;
					if (message.Bold) { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Bold); }
					else if (message.Italic) { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Italic); }
					else { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Regular); }
					if (message.InsertNewLine)
					{
						for (int i = 0; i < message.NewLineCount; i++)
						{
							assembledText += Environment.NewLine;
						}
					}
					messageConsole.AppendText(assembledText);
				}
			}));
		}

		/// <summary>
		/// Prints a collection of messages to the console
		/// </summary>
		/// <param name="messages">Messages to be printed</param>
		internal void MessagePrint(List<ConsoleMessage> messages)
		{
			foreach (ConsoleMessage message in messages)
			{
				this.Invoke(new MethodInvoker(delegate
				{
					string assembledText = string.Empty;

					if (message.TextBlocks.Length > 0)
					{
						for (int i = 0; i < message.TextBlocks.Length; i++)
						{
							assembledText += message.TextBlocks[i];
						}

						messageConsole.Select(messageConsole.Text.Length, 0);
						messageConsole.SelectionBackColor = message.BackColor;
						messageConsole.SelectionColor = message.ForeColor;
						if (message.Bold) { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Bold); }
						else if (message.Italic) { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Italic); }
						else { messageConsole.SelectionFont = new Font(messageConsole.Font, FontStyle.Regular); }
						if (message.InsertNewLine)
						{
							for (int i = 0; i < message.NewLineCount; i++)
							{
								assembledText += Environment.NewLine;
							}
						}
						messageConsole.AppendText(assembledText);
					}
				}));

			}
		}

        /// <summary>
        /// Clears all messages in the messageConsole
        /// </summary>
		public void MessagesClear() { this.Invoke(new MethodInvoker(delegate { messageConsole.Clear(); })); }

        /// <summary>
        /// Prints text to the packetConsole
        /// </summary>
        /// <param name="text">string to be printed</param>
		public void PacketPrint(string text) { this.Invoke(new MethodInvoker(delegate { packetConsole.AppendText(string.Concat(text, Environment.NewLine)); })); }

        /// <summary>
        /// Clears all messages in the packetConsole
        /// </summary>
		public void PacketsClear() { this.Invoke(new MethodInvoker(delegate { packetConsole.Clear(); })); }

		/// <summary>
		/// When form closes, clean up instance.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void debugGUI_FormClosing(object sender, FormClosingEventArgs e)
		{
			Instance = null;
		}
    }
}
