namespace Game
{
	partial class consoleGUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.console = new System.Windows.Forms.RichTextBox();
            this.commandInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(12, 12);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(707, 412);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // commandInput
            // 
            this.commandInput.Location = new System.Drawing.Point(12, 430);
            this.commandInput.Name = "commandInput";
            this.commandInput.Size = new System.Drawing.Size(707, 20);
            this.commandInput.TabIndex = 1;
            // 
            // consoleGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 464);
            this.Controls.Add(this.commandInput);
            this.Controls.Add(this.console);
            this.KeyPreview = true;
            this.Name = "consoleGUI";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.consoleGUI_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consoleGUI_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.TextBox commandInput;
	}
}