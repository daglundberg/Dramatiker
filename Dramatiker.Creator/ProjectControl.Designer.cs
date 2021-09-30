
namespace Dramatiker.SetCreator
{
	partial class ProjectControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.addAudioFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.createFadeInEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createFadeOutEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createCrossFadeEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new System.Drawing.Point(260, 27);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(171, 364);
			this.listBox1.TabIndex = 2;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(112, 26);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAudioFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.createFadeInEventToolStripMenuItem,
            this.createFadeOutEventToolStripMenuItem,
            this.createCrossFadeEventToolStripMenuItem,
            this.toolStripSeparator2,
            this.playbackToolStripMenuItem,
            this.exportToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
			this.toolStripMenuItem1.Text = "Project";
			// 
			// addAudioFileToolStripMenuItem
			// 
			this.addAudioFileToolStripMenuItem.Name = "addAudioFileToolStripMenuItem";
			this.addAudioFileToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.addAudioFileToolStripMenuItem.Text = "Add Audio File...";
			this.addAudioFileToolStripMenuItem.Click += new System.EventHandler(this.addAudioFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
			// 
			// createFadeInEventToolStripMenuItem
			// 
			this.createFadeInEventToolStripMenuItem.Name = "createFadeInEventToolStripMenuItem";
			this.createFadeInEventToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.createFadeInEventToolStripMenuItem.Text = "Create Fade In Event";
			this.createFadeInEventToolStripMenuItem.Click += new System.EventHandler(this.createFadeInEventToolStripMenuItem_Click);
			// 
			// createFadeOutEventToolStripMenuItem
			// 
			this.createFadeOutEventToolStripMenuItem.Name = "createFadeOutEventToolStripMenuItem";
			this.createFadeOutEventToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.createFadeOutEventToolStripMenuItem.Text = "Create Fade Out Event";
			this.createFadeOutEventToolStripMenuItem.Click += new System.EventHandler(this.createFadeOutEventToolStripMenuItem_Click);
			// 
			// createCrossFadeEventToolStripMenuItem
			// 
			this.createCrossFadeEventToolStripMenuItem.Name = "createCrossFadeEventToolStripMenuItem";
			this.createCrossFadeEventToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.createCrossFadeEventToolStripMenuItem.Text = "Create Cross-Fade Event";
			this.createCrossFadeEventToolStripMenuItem.Click += new System.EventHandler(this.createCrossFadeEventToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
			// 
			// playbackToolStripMenuItem
			// 
			this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
			this.playbackToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.playbackToolStripMenuItem.Text = "Playback";
			this.playbackToolStripMenuItem.Click += new System.EventHandler(this.playbackToolStripMenuItem_Click);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 27);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(251, 367);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "Events:";
			// 
			// ProjectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.listBox1);
			this.Margin = new System.Windows.Forms.Padding(3, 60, 3, 3);
			this.Name = "ProjectControl";
			this.Size = new System.Drawing.Size(444, 397);
			this.Load += new System.EventHandler(this.ProjectControl_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem addAudioFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createFadeInEventToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createFadeOutEventToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createCrossFadeEventToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}
