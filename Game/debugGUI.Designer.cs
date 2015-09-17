namespace Game
{
    partial class debugGUI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.messageTab = new System.Windows.Forms.TabPage();
            this.packetTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageConsole = new System.Windows.Forms.RichTextBox();
            this.packetConsole = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.messageTab.SuspendLayout();
            this.packetTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.messageTab);
            this.tabControl1.Controls.Add(this.packetTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 342);
            this.tabControl1.TabIndex = 0;
            // 
            // messageTab
            // 
            this.messageTab.Controls.Add(this.messageConsole);
            this.messageTab.Location = new System.Drawing.Point(4, 22);
            this.messageTab.Name = "messageTab";
            this.messageTab.Padding = new System.Windows.Forms.Padding(3);
            this.messageTab.Size = new System.Drawing.Size(560, 316);
            this.messageTab.TabIndex = 0;
            this.messageTab.Text = "Messages";
            this.messageTab.UseVisualStyleBackColor = true;
            // 
            // packetTab
            // 
            this.packetTab.Controls.Add(this.packetConsole);
            this.packetTab.Location = new System.Drawing.Point(4, 22);
            this.packetTab.Name = "packetTab";
            this.packetTab.Padding = new System.Windows.Forms.Padding(3);
            this.packetTab.Size = new System.Drawing.Size(560, 316);
            this.packetTab.TabIndex = 1;
            this.packetTab.Text = "Packets";
            this.packetTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(592, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // messageConsole
            // 
            this.messageConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageConsole.Location = new System.Drawing.Point(3, 3);
            this.messageConsole.Name = "messageConsole";
            this.messageConsole.ReadOnly = true;
            this.messageConsole.Size = new System.Drawing.Size(554, 310);
            this.messageConsole.TabIndex = 0;
            this.messageConsole.Text = "";
            // 
            // packetConsole
            // 
            this.packetConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetConsole.Location = new System.Drawing.Point(3, 3);
            this.packetConsole.Name = "packetConsole";
            this.packetConsole.ReadOnly = true;
            this.packetConsole.Size = new System.Drawing.Size(554, 310);
            this.packetConsole.TabIndex = 0;
            this.packetConsole.Text = "";
            // 
            // debugGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 381);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "debugGUI";
            this.Text = "Debug";
            this.tabControl1.ResumeLayout(false);
            this.messageTab.ResumeLayout(false);
            this.packetTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage messageTab;
        private System.Windows.Forms.TabPage packetTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.RichTextBox messageConsole;
        private System.Windows.Forms.RichTextBox packetConsole;
    }
}