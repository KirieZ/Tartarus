using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Common;

namespace Game
{
	public partial class consoleGUI : Form
	{
        /// <summary>
        /// Provides a handle to report gui changes.
        /// </summary>
        public static consoleGUI Instance;

		public consoleGUI()
		{
			InitializeComponent();
            Instance = this;
		}

        private void consoleGUI_Load(object sender, EventArgs e)
        {
            // Example of adding a task to the thread-pool
            Threads.AddTask(Threads.Factory.StartNew(() => 
            {
                Print(string.Format("Loading commands from {0}", "<place-holder>"), 0);
                //ConsoleCommands.Load();
            }));

            // Run all tasks previously added to the Queue
            Threads.RunTasks();
        }

        /// <summary>
        /// Prints text to the console
        /// </summary>
        /// <param name="text">string to be printed</param>
        /// <param name="type">type of message being printed</param>
        /// <param name="color">color of message being printed</param>
        /// <TODO>implement styling with 'type'</TODO> 
        internal void Print(string text, int type)
        {
            this.Invoke(new MethodInvoker(delegate { console.AppendText(string.Concat(text, Environment.NewLine)); }));
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
                        /*ConsoleCommands.OnInputReceived(commandInput.Text);*/
                    }
                }
            }
        }
	}
}
