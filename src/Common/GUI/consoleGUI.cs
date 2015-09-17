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
			CancellationTokenSource cts = new CancellationTokenSource();

			// Use our factory to run a set of tasks. 
			Task t = Threads.Factory.StartNew(() =>
			{
				Server.Load();
			});
			Threads.Tasks.Add(t);

			await Task.WhenAll(Threads.Tasks.ToArray());
			cts.Dispose();
			Print("\n\nServer Loaded.", 0);
        }

        /// <summary>
        /// Prints text to the console
        /// </summary>
        /// <param name="text">string to be printed</param>
        /// <param name="type">type of message being printed</param>
        /// <param name="color">color of message being printed</param>
        /// <TODO>implement styling with 'type'</TODO> 
        internal void Print(string text, int type=0)
        {
            //this.Invoke(new MethodInvoker(delegate { console.AppendText(string.Concat(text, Environment.NewLine)); }));
			this.Invoke(new MethodInvoker(delegate { console.AppendText(text); }));
        }

        /// <summary>
        /// Clears all text in the console
        /// </summary>
        /// <TODO>Call this method from ConsoleCommands</TODO>
        internal void ConsoleClear() { this.Invoke(new MethodInvoker(delegate { console.Clear(); })); }

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
						Task t = Threads.Factory.StartNew(() =>
						{
							ConsoleCommands.OnInputReceived(commandInput.Text);
						});
						Threads.Tasks.Add(t);
					}
                }
            }
        }
	}
}
