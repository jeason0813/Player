using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Player
{
    public partial class MainForm : Form
    {
        public static int stream = 0;

        bool stopped = true;
        bool paused = false;
        bool scrubbing = false;
        int activeTrack = 0;

        public static List<string> supportedExts = new List<string>();

        List<PlaylistItem> list = new List<PlaylistItem>();

        Visualizations visForm = new Visualizations();

        string[] args = Environment.GetCommandLineArgs();

        IntPtr userAgentPtr;
        IntPtr proxyPtr;

        public MainForm()
        {
            InitializeComponent();

            BuildFilter();
            BuildUserAgent();
            ProcessCmdLine();

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_NET_PLAYLIST, 1);
        }

        private void BuildFilter() //build the filter string for openFileDialog
        {
            supportedExts.AddRange(Bass.SupportedStreamExtensions.Split(';'));
            supportedExts.AddRange(Bass.SupportedMusicExtensions.Split(';'));
            
            foreach (KeyValuePair<int, string> item in Bass.BASS_PluginLoadDirectory("plugins"))
            {
                BASS_PLUGINFORM[] pforms = Bass.BASS_PluginGetInfo(item.Key).formats;
                foreach (BASS_PLUGINFORM pf in pforms)
                {
                    foreach (string ext in pf.exts.Split(';'))
                        if (!supportedExts.Contains(ext)) supportedExts.Add(ext);
                }
            }
            
            string filter = "All files (*.*)|*.*|";
            for (int i = 0; i < supportedExts.Count; i++)
                filter += supportedExts[i].Replace("*.", "") + " files (" + supportedExts[i] + ")|" + supportedExts[i] + ((i == supportedExts.Count - 1) ? "" : "|");

            openFileDialog.Filter = filter;
        }

        private void BuildUserAgent() //build the user agent string for playing urls
        {
            string agent = "";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                    agent += titleAttribute.Title + " ";
            }

            agent += Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ";

            attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length > 0)
            {
                agent += ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }

            agent = agent.Replace("©", "");
            userAgentPtr = Marshal.StringToHGlobalAnsi(agent); //can't use StringToHGlobalUni for user agent :(

            Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_AGENT, userAgentPtr);
        }

        private void ProcessCmdLine()
        {
            if (args.Length < 2) return;

            if (new System.IO.FileInfo(args[1]).Extension == ".xspf")
            {
                List<PlaylistItem> loaded = XSPF.Load(args[1]);

                if (loaded.Count != 0)
                {
                    foreach (PlaylistItem p in loaded)
                        AddToPlaylist(p);
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++) //skip the first argument, it's just the exe
                    AddToPlaylist(XSPF.GetTags(args[i]));
            }
            
            Play();
        }

        private void Play()
        {
            if (list.Count == 0) return;

            if (activeTrack > list.Count - 1)
                activeTrack = list.Count - 1;

            if (list[activeTrack].Type == PlaylistItem.TYPE_MUSIC)
                stream = Bass.BASS_MusicLoad(list[activeTrack].Path, 0, 0, BASSFlag.BASS_MUSIC_FLOAT | BASSFlag.BASS_MUSIC_PRESCAN, 0);
            else if (list[activeTrack].Type == PlaylistItem.TYPE_STREAM_URL)
                stream = Bass.BASS_StreamCreateURL(list[activeTrack].Path, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_STATUS, null, IntPtr.Zero);
            else
                stream = Bass.BASS_StreamCreateFile(list[activeTrack].Path, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);

            if (stream != 0)
            {
                Bass.BASS_ChannelPlay(stream, true);

                stopped = false;
                paused = false;

                if (list[activeTrack].Type == PlaylistItem.TYPE_STREAM_URL)
                {
                    trkPos.Enabled = false;
                    chkRepeatTrack.Enabled = chkRepeatAll.Enabled = chkRandom.Enabled = false;
                    chkRepeatTrack.Checked = chkRepeatAll.Checked = chkRandom.Checked = false;
                }
                else
                {
                    trkPos.Enabled = true;
                    chkRepeatTrack.Enabled = chkRepeatAll.Enabled = chkRandom.Enabled = true;
                    trkPos.Maximum = (int)Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));
                }

                timer.Start();
                if (list[activeTrack].Type == PlaylistItem.TYPE_STREAM_URL) tagTimer.Start();
                btnStop.Enabled = true;

                lblTag.Text = list[activeTrack].Artist.Replace("&", "&&") + " - " + list[activeTrack].Title.Replace("&", "&&"); //escape ampersands (no keyboard shortcuts)
                Text = "Player | " + list[activeTrack].Artist + " - " + list[activeTrack].Title;

                if (visForm.Visible && !visForm.IsDisposed)
                    visForm.timer.Start();

                btnPlay.Image = (Image)Properties.Resources.control_pause_blue;
            }
            else
            {
                MessageBox.Show(string.Format("Stream Error: {0}", Bass.BASS_ErrorGetCode()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pause()
        {
            Bass.BASS_ChannelPause(stream);
            if (tagTimer.Enabled) tagTimer.Stop();
            timer.Stop();
            if (visForm.Visible && !visForm.IsDisposed)
                visForm.Stop();
            paused = true;
            btnPlay.Image = (Image)Properties.Resources.control_play_blue;
        }

        private void Resume()
        {
            Bass.BASS_ChannelPlay(stream, false);
            timer.Start();
            if (list[activeTrack].Type == PlaylistItem.TYPE_STREAM_URL) tagTimer.Start();
            if (visForm.Visible && !visForm.IsDisposed)
                visForm.timer.Start();
            paused = false;
            btnPlay.Image = (Image)Properties.Resources.control_pause_blue;
        }

        private void Stop()
        {
            stopped = true;

            Bass.BASS_ChannelStop(stream);

            if (tagTimer.Enabled) tagTimer.Stop();
            timer.Stop();
            btnStop.Enabled = false;
            trkPos.Enabled = false;
            trkPos.Value = 0;

            lblTag.Text = "Idle";
            Text = "Player";
            lblPos.Text = "-/-";

            if (visForm.Visible && !visForm.IsDisposed)
                visForm.Stop();

            btnPlay.Image = (Image)Properties.Resources.control_play_blue;
        }

        private void GetTags()
        {
            PlaylistItem item = XSPF.GetTags(list[activeTrack].Path);

            list[activeTrack].ListViewItem.SubItems[1].Text = item.Title;
            list[activeTrack].ListViewItem.SubItems[2].Text = item.Artist;
            list[activeTrack].ListViewItem.SubItems[3].Text = item.Album;

            lblTag.Text = item.Artist.Replace("&", "&&") + " - " + item.Title.Replace("&", "&&"); //escape ampersands (no keyboard shortcuts)
            Text = "Player | " + item.Artist + " - " + item.Title;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (stopped)
                Play();
            else if (paused)
                Resume();
            else if (!stopped && !paused)
                Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (stopped) return;

            Stop();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (stopped || list.Count == 0) return;
            
            Stop();

            if (activeTrack == 0)
                activeTrack = list.Count - 1;
            else
                activeTrack--;

            Play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (stopped || list.Count == 0) return;
            
            Stop();

            if (activeTrack == list.Count - 1)
                activeTrack = 0;
            else
                activeTrack++;

            Play();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            Marshal.FreeHGlobal(userAgentPtr);
            Marshal.FreeHGlobal(proxyPtr);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (activeTrack > list.Count - 1)
                activeTrack = list.Count - 1;
            
            if (Bass.BASS_ChannelIsActive(stream) == BASSActive.BASS_ACTIVE_STOPPED && list[activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
                ShouldPlayNext();
            
            string length = null;
            double lSecsD = -1;

            if (list[activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
            {
                lSecsD = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));

                int lHrs = (int)Math.Floor(lSecsD / 3600);
                int lMins = (int)Math.Floor((lSecsD % 3600) / 60);
                int lSecs = (int)lSecsD % 60;

                length = string.Format("{0}{1}:{2}", ((lHrs == 0) ? "" : lHrs + ":"), lMins.ToString("00"), lSecs.ToString("00"));
            }
            
            double pSecsD = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream));

            if (!scrubbing && list[activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
            {
                if (list[activeTrack].Type == PlaylistItem.TYPE_MUSIC && pSecsD >= lSecsD)
                    ShouldPlayNext();
                else
                    trkPos.Value = (int)pSecsD;
            }

            int pHrs = (int)Math.Floor(pSecsD / 3600);
            int pMins = (int)Math.Floor((pSecsD % 3600) / 60);
            int pSecs = (int)pSecsD % 60;

            string pos = string.Format("{0}{1}:{2}", ((pHrs == 0) ? "" : pHrs + ":"), pMins.ToString("00"), pSecs.ToString("00"));

            if (list[activeTrack].Type == PlaylistItem.TYPE_STREAM_URL)
                lblPos.Text = pos;
            else
                lblPos.Text = pos + "/" + length;
        }

        private void ShouldPlayNext()
        {
            if (chkRepeatTrack.Checked)
                    Bass.BASS_ChannelPlay(stream, true);
            else if (chkRandom.Checked)
            {
                Stop();
                activeTrack = new Random().Next(list.Count - 1);
                Play();
            }
            else if (activeTrack == list.Count - 1)
            {
                if (chkRepeatAll.Checked)
                {
                    Stop();
                    activeTrack = 0;
                    Play();
                }
                else
                    Stop();
            }
            else
            {
                Stop();
                activeTrack++;
                Play();
            }
        }

        private void tagTimer_Tick(object sender, EventArgs e)
        {
            GetTags();
        }

        private void trkVol_Scroll(object sender, EventArgs e)
        {
            if (list.Count == 0) return;
            if (list[activeTrack].Type == PlaylistItem.TYPE_MUSIC)
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_MUSIC, trkVol.Value);
            else
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, trkVol.Value);
        }

        private void trkPos_MouseDown(object sender, MouseEventArgs e)
        {
            scrubbing = true;
        }

        private void trkPos_MouseUp(object sender, MouseEventArgs e)
        {
            scrubbing = false;
        }

        private void trkPos_Scroll(object sender, EventArgs e)
        {
            if (!scrubbing) return;

            Bass.BASS_ChannelSetPosition(stream, (double)trkPos.Value);
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string f in openFileDialog.FileNames)
                AddToPlaylist(XSPF.GetTags(f));

            if (stopped)
                Play();
        }

        private void addUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUrlForm form = new AddUrlForm();
            
            if (form.ShowDialog() != DialogResult.OK)
                return;

            if (!stopped)
                Stop();

            AddToPlaylist(XSPF.GetTags(form.path));

            if (stopped)
                Play();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void toggleVisualsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (visForm.Visible)
            {
                visForm.Stop();
                visForm.Close();
            }
            else if (visForm.IsDisposed)
            {
                visForm = new Visualizations();
                visForm.Show();
                visForm.timer.Start();
            }
            else
            {
                visForm.Show();
                visForm.timer.Start();
            }
        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPlaylistDialog.ShowDialog() != DialogResult.OK)
                return;

            List<PlaylistItem> loaded = XSPF.Load(openPlaylistDialog.FileName);

            if (loaded.Count == 0) return;
            
            list.Clear();
            lstPlaylist.Items.Clear();
            
            foreach (PlaylistItem p in loaded)
                AddToPlaylist(p);
        }

        private void savePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list.Count == 0 || savePlaylistDialog.ShowDialog() != DialogResult.OK)
                return;

            XSPF.Save(savePlaylistDialog.FileName, list);
        }

        private void AddToPlaylist(PlaylistItem p)
        {
            if (p == null)
                return; //unsupported file type
            
            p.ListViewItem = new ListViewItem(p.Track);

            p.ListViewItem.SubItems.Add(p.Title);
            p.ListViewItem.SubItems.Add(p.Artist);
            p.ListViewItem.SubItems.Add(p.Album);
            p.ListViewItem.SubItems.Add(new Uri(p.Path).LocalPath);

            lstPlaylist.Items.Add(p.ListViewItem);

            list.Add(p);
        }

        private void RemoveFromPlaylist(PlaylistItem p)
        {
            lstPlaylist.Items.Remove(p.ListViewItem);
            list.Remove(p);
            if (activeTrack > list.Count - 1)
                activeTrack = list.Count - 1;
        }

        private PlaylistItem GetPlaylistItem(ListViewItem i)
        {
            foreach (PlaylistItem p in list) //surely there's a better way of doing this than looping through all entries
                if (p.ListViewItem == i)
                    return p;

            return null;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count == 0) return;

            if (lstPlaylist.SelectedItems.Contains(list[activeTrack].ListViewItem))
                Stop();

            foreach (ListViewItem i in lstPlaylist.SelectedItems)
            {
                PlaylistItem item = GetPlaylistItem(i);
                if (item != null)
                    RemoveFromPlaylist(item);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!stopped)
                Stop();
            
            lstPlaylist.Items.Clear();
            list.Clear();

            activeTrack = 0;
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count != 1) return;

            if (!stopped)
                Stop();
            activeTrack = lstPlaylist.SelectedIndices[0];
            Play();
        }

        private void lstPlaylist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lstPlaylist_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (string file in files)
                    AddToPlaylist(XSPF.GetTags(file));
            }
        }

        private void lstPlaylist_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lstPlaylist_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count == 0)
                return;

            lstPlaylist.DoDragDrop(lstPlaylist.SelectedItems[0], DragDropEffects.Move);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prevDevice = Properties.Settings.Default.Device;
            
            if (new SettingsForm().ShowDialog() != DialogResult.OK)
                return;

            proxyPtr = Marshal.StringToHGlobalAnsi(Properties.Settings.Default.Proxy);

            Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_PROXY, proxyPtr);

            if (prevDevice != Properties.Settings.Default.Device)
            {
                Stop();
                Program.init(); //reinitialise BASS on the new device
                Play();
            }
        }
    }
}
