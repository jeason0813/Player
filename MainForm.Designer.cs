namespace Player
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblPos = new System.Windows.Forms.Label();
            this.trkPos = new System.Windows.Forms.TrackBar();
            this.trkVol = new System.Windows.Forms.TrackBar();
            this.lblTag = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.chkRepeatAll = new System.Windows.Forms.CheckBox();
            this.chkRepeatTrack = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleVisualsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagTimer = new System.Windows.Forms.Timer(this.components);
            this.lstPlaylist = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstPlaylistContentMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPlaylistDialog = new System.Windows.Forms.OpenFileDialog();
            this.savePlaylistDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVol)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.lstPlaylistContentMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblPos
            // 
            this.lblPos.Location = new System.Drawing.Point(12, 46);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(78, 23);
            this.lblPos.TabIndex = 13;
            this.lblPos.Text = "-/-";
            this.lblPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trkPos
            // 
            this.trkPos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkPos.AutoSize = false;
            this.trkPos.Enabled = false;
            this.trkPos.Location = new System.Drawing.Point(96, 44);
            this.trkPos.Name = "trkPos";
            this.trkPos.Size = new System.Drawing.Size(193, 25);
            this.trkPos.TabIndex = 14;
            this.trkPos.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trkPos, "Position");
            this.trkPos.Scroll += new System.EventHandler(this.trkPos_Scroll);
            this.trkPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trkPos_MouseDown);
            this.trkPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trkPos_MouseUp);
            // 
            // trkVol
            // 
            this.trkVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trkVol.AutoSize = false;
            this.trkVol.Location = new System.Drawing.Point(295, 44);
            this.trkVol.Maximum = 10000;
            this.trkVol.Name = "trkVol";
            this.trkVol.Size = new System.Drawing.Size(81, 25);
            this.trkVol.TabIndex = 15;
            this.trkVol.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trkVol, "Volume");
            this.trkVol.Value = 10000;
            this.trkVol.Scroll += new System.EventHandler(this.trkVol_Scroll);
            // 
            // lblTag
            // 
            this.lblTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTag.Location = new System.Drawing.Point(12, 26);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(364, 17);
            this.lblTag.TabIndex = 16;
            this.lblTag.Text = "Idle";
            // 
            // chkRandom
            // 
            this.chkRandom.Image = global::Player.Properties.Resources.help;
            this.chkRandom.Location = new System.Drawing.Point(262, 72);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(35, 24);
            this.chkRandom.TabIndex = 24;
            this.toolTip.SetToolTip(this.chkRandom, "Random");
            this.chkRandom.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Image = global::Player.Properties.Resources.control_start_blue;
            this.btnPrevious.Location = new System.Drawing.Point(96, 73);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(38, 23);
            this.btnPrevious.TabIndex = 23;
            this.toolTip.SetToolTip(this.btnPrevious, "Previous");
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::Player.Properties.Resources.control_end_blue;
            this.btnNext.Location = new System.Drawing.Point(136, 73);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(38, 23);
            this.btnNext.TabIndex = 22;
            this.toolTip.SetToolTip(this.btnNext, "Next");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // chkRepeatAll
            // 
            this.chkRepeatAll.Image = global::Player.Properties.Resources.control_repeat;
            this.chkRepeatAll.Location = new System.Drawing.Point(221, 72);
            this.chkRepeatAll.Name = "chkRepeatAll";
            this.chkRepeatAll.Size = new System.Drawing.Size(35, 24);
            this.chkRepeatAll.TabIndex = 21;
            this.toolTip.SetToolTip(this.chkRepeatAll, "Repeat All");
            this.chkRepeatAll.UseVisualStyleBackColor = true;
            // 
            // chkRepeatTrack
            // 
            this.chkRepeatTrack.Image = global::Player.Properties.Resources.control_repeat_blue;
            this.chkRepeatTrack.Location = new System.Drawing.Point(180, 72);
            this.chkRepeatTrack.Name = "chkRepeatTrack";
            this.chkRepeatTrack.Size = new System.Drawing.Size(35, 24);
            this.chkRepeatTrack.TabIndex = 17;
            this.toolTip.SetToolTip(this.chkRepeatTrack, "Repeat Track");
            this.chkRepeatTrack.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBrowse,
            this.settingsToolStripMenuItem,
            this.toggleVisualsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(388, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.addUrlToolStripMenuItem,
            this.toolStripSeparator1,
            this.openPlaylistToolStripMenuItem,
            this.savePlaylistToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(37, 20);
            this.btnBrowse.Text = "File";
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Image = global::Player.Properties.Resources.cd_go;
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addFilesToolStripMenuItem.Text = "Add Files";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.addFilesToolStripMenuItem_Click);
            // 
            // addUrlToolStripMenuItem
            // 
            this.addUrlToolStripMenuItem.Image = global::Player.Properties.Resources.world_go;
            this.addUrlToolStripMenuItem.Name = "addUrlToolStripMenuItem";
            this.addUrlToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addUrlToolStripMenuItem.Text = "Add URL";
            this.addUrlToolStripMenuItem.Click += new System.EventHandler(this.addUrlToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // openPlaylistToolStripMenuItem
            // 
            this.openPlaylistToolStripMenuItem.Image = global::Player.Properties.Resources.table_go;
            this.openPlaylistToolStripMenuItem.Name = "openPlaylistToolStripMenuItem";
            this.openPlaylistToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openPlaylistToolStripMenuItem.Text = "Open Playlist";
            this.openPlaylistToolStripMenuItem.Click += new System.EventHandler(this.openPlaylistToolStripMenuItem_Click);
            // 
            // savePlaylistToolStripMenuItem
            // 
            this.savePlaylistToolStripMenuItem.Image = global::Player.Properties.Resources.table_save;
            this.savePlaylistToolStripMenuItem.Name = "savePlaylistToolStripMenuItem";
            this.savePlaylistToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.savePlaylistToolStripMenuItem.Text = "Save Playlist";
            this.savePlaylistToolStripMenuItem.Click += new System.EventHandler(this.savePlaylistToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Player.Properties.Resources.cross;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toggleVisualsToolStripMenuItem
            // 
            this.toggleVisualsToolStripMenuItem.Name = "toggleVisualsToolStripMenuItem";
            this.toggleVisualsToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.toggleVisualsToolStripMenuItem.Text = "Toggle Visualizations";
            this.toggleVisualsToolStripMenuItem.Click += new System.EventHandler(this.toggleVisualsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tagTimer
            // 
            this.tagTimer.Interval = 3000;
            this.tagTimer.Tick += new System.EventHandler(this.tagTimer_Tick);
            // 
            // lstPlaylist
            // 
            this.lstPlaylist.AllowDrop = true;
            this.lstPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPlaylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5});
            this.lstPlaylist.ContextMenuStrip = this.lstPlaylistContentMenuStrip;
            this.lstPlaylist.FullRowSelect = true;
            this.lstPlaylist.Location = new System.Drawing.Point(12, 102);
            this.lstPlaylist.Name = "lstPlaylist";
            this.lstPlaylist.Size = new System.Drawing.Size(364, 72);
            this.lstPlaylist.TabIndex = 20;
            this.lstPlaylist.UseCompatibleStateImageBehavior = false;
            this.lstPlaylist.View = System.Windows.Forms.View.Details;
            this.lstPlaylist.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstPlaylist_DragDrop);
            this.lstPlaylist.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstPlaylist_DragEnter);
            this.lstPlaylist.DragOver += new System.Windows.Forms.DragEventHandler(this.lstPlaylist_DragOver);
            this.lstPlaylist.DoubleClick += new System.EventHandler(this.lstPlaylist_DoubleClick);
            this.lstPlaylist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstPlaylist_MouseDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Track";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Artist";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Album";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Path";
            // 
            // lstPlaylistContentMenuStrip
            // 
            this.lstPlaylistContentMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.lstPlaylistContentMenuStrip.Name = "lstPlaylistContentMenuStrip";
            this.lstPlaylistContentMenuStrip.Size = new System.Drawing.Size(118, 48);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::Player.Properties.Resources.delete;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::Player.Properties.Resources.cross;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // openPlaylistDialog
            // 
            this.openPlaylistDialog.Filter = "XSPF files|*.xspf|All files|*.*";
            // 
            // savePlaylistDialog
            // 
            this.savePlaylistDialog.Filter = "XSPF Files|*.xspf|All files|*.*";
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::Player.Properties.Resources.control_play_blue;
            this.btnPlay.Location = new System.Drawing.Point(12, 73);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(38, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::Player.Properties.Resources.control_stop_blue;
            this.btnStop.Location = new System.Drawing.Point(52, 73);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 186);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lstPlaylist);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.trkVol);
            this.Controls.Add(this.chkRepeatAll);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblPos);
            this.Controls.Add(this.chkRandom);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.trkPos);
            this.Controls.Add(this.chkRepeatTrack);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(400, 220);
            this.Name = "MainForm";
            this.Text = "Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trkPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVol)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.lstPlaylistContentMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.TrackBar trkPos;
        private System.Windows.Forms.TrackBar trkVol;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.CheckBox chkRepeatTrack;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnBrowse;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tagTimer;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleVisualsToolStripMenuItem;
        private System.Windows.Forms.ListView lstPlaylist;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox chkRepeatAll;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.ToolStripMenuItem openPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openPlaylistDialog;
        private System.Windows.Forms.SaveFileDialog savePlaylistDialog;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip lstPlaylistContentMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}