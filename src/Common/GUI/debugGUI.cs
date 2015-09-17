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
        /// Prints text to the messageConsole
        /// </summary>
        /// <param name="text">string to be printed</param>
		public void MessagePrint(string text) { this.Invoke(new MethodInvoker(delegate { messageConsole.AppendText(string.Concat(text, Environment.NewLine)); })); }

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
