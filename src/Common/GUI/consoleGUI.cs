using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.GUI
{
	public partial class consoleGUI : Form
	{
        /// <summary>
        /// Provides a handle to report gui changes.
        /// </summary>
        public static consoleGUI Instance;

		/// <summary>
		/// Reference to the server which this gui is related to
		/// </summary>
		private static ServerBase Server;

		public consoleGUI(ServerBase serverBase)
		{
			InitializeComponent();
            Instance = this;
			Server = serverBase;
		}

		private async void consoleGUI_Load(object sender, EventArgs e)
		{
            await Task.Run(() => { Server.Load(); });
            ConsoleUtils.ShowStatus("Server load complete.");
            commandInput.Focus();
        }

        /// <summary>
        /// Print a single message to the console
        /// </summary>
        /// <param name="message">Message class containing message info</param>
        internal void Print(ConsoleMessage message)
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

                    console.Select(console.Text.Length, 0);
                    console.SelectionBackColor = message.BackColor;
                    console.SelectionColor = message.ForeColor;
                    if (message.Bold) { console.SelectionFont = new Font(console.Font, FontStyle.Bold); }
                    else if (message.Italic) { console.SelectionFont = new Font(console.Font, FontStyle.Italic); }
					else { console.SelectionFont = new Font(console.Font, FontStyle.Regular); }
                    if (message.InsertNewLine)
                    {
                        for (int i = 0; i < message.NewLineCount; i++)
                        {
                            assembledText += Environment.NewLine;
                        }
                    }
                    console.AppendText(assembledText);
                }
            }));
        }

        /// <summary>
        /// Prints a collection of messages to the console
        /// </summary>
        /// <param name="messages">Messages to be printed</param>
        internal void Print(List<ConsoleMessage> messages)
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

                        console.Select(console.Text.Length, 0);
                        console.SelectionBackColor = message.BackColor;
                        console.SelectionColor = message.ForeColor;
                        if (message.Bold) { console.SelectionFont = new Font(console.Font, FontStyle.Bold); }
						else if (message.Italic) { console.SelectionFont = new Font(console.Font, FontStyle.Italic); }
						else { console.SelectionFont = new Font(console.Font, FontStyle.Regular); }
                        if (message.InsertNewLine)
                        {
                            for (int i = 0; i < message.NewLineCount; i++)
                            {
                                assembledText += Environment.NewLine;
                            }
                        }
                        console.AppendText(assembledText);
                    }
                }));
 
            }
        }

        /// <summary>
        /// Clears all text in the console
        /// </summary>
        /// <TODO>Call this method from ConsoleCommands</TODO>
        internal void ConsoleClear() { this.Invoke(new MethodInvoker(delegate { console.Clear(); })); }

        internal void InputClear() { this.Invoke(new MethodInvoker(delegate { commandInput.Clear(); })); }

        /// <summary>
        /// Occurs when a key is pressed on the consoleGUI
        /// </summary>
        private void consoleGUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (commandInput.Focused)
            {
                if (commandInput.Text.Length > 0)
                {
					if (e.KeyCode == Keys.Enter)
					{
                        Task.Factory.StartNew(() => { ConsoleCommands.OnInputReceived(commandInput.Text); InputClear(); });
					}
                }
            }
        }
	}
}
