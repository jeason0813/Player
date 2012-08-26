using System;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using System.Drawing;

namespace Player
{
    public partial class Visualizations : Form
    {
        Visuals vis = new Visuals();

        int selectedVis = 0;

        const int VIS_SPECTRUM = 0;
        const int VIS_SPECTRUMLINE = 1;
        const int VIS_SPECTRUMWAVE = 2;
        
        public Visualizations()
        {
            InitializeComponent();
        }

        public void Stop()
        {
            timer.Stop();

            lbldBL.Text = "00.0dB";
            lbldBR.Text = "00.0dB";

            prgL.Value = 0;
            prgR.Value = 0;

            if (pbVis.Image != null) //form isn't closing
            {
                Graphics.FromImage(pbVis.Image).Clear(Color.Black);
                pbVis.Refresh();
            }
        }

        // calculates the level of a stereo signal between 0 and 65535
        // where 0 = silent, 32767 = 0dB and 65535 = +6dB
        private void GetLevel(int channel, out int peakL, out int peakR)
        {
            float maxL = 0f;
            float maxR = 0f;

            // length of a 20ms window in bytes
            int length20ms = (int)Bass.BASS_ChannelSeconds2Bytes(channel, 0.02);
            // the number of 32-bit floats required (since length is in bytes!)
            int l4 = length20ms / 4; // 32-bit = 4 bytes

            // create a data buffer as needed
            float[] sampleData = new float[l4];

            int length = Bass.BASS_ChannelGetData(channel, sampleData, length20ms);

            // the number of 32-bit floats received
            // as less data might be returned by BASS_ChannelGetData as requested
            l4 = length / 4;

            for (int a = 0; a < l4; a++)
            {
                float absLevel = Math.Abs(sampleData[a]);

                // decide on L/R channel
                if (a % 2 == 0)
                {
                    // Left channel
                    if (absLevel > maxL)
                        maxL = absLevel;
                }
                else
                {
                    // Right channel
                    if (absLevel > maxR)
                        maxR = absLevel;
                }
            }

            // limit the maximum peak levels to +6bB = 65535 = 0xFFFF
            // the peak levels will be int values, where 32767 = 0dB
            // and a float value of 1.0 also represents 0db.
            peakL = (int)Math.Round(32767f * maxL) & 0xFFFF;
            peakR = (int)Math.Round(32767f * maxR) & 0xFFFF;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int peakL = 0;
            int peakR = 0;
            GetLevel(MainForm.stream, out peakL, out peakR);

            int progL = (int)peakL;
            int progR = (int)peakR;

            // convert the level to dB
            double dBlevelL = Utils.LevelToDB(peakL, 65535);
            double dBlevelR = Utils.LevelToDB(peakR, 65535);

            lbldBL.Text = dBlevelL.ToString("00.#dB");
            lbldBR.Text = dBlevelR.ToString("00.#dB");
            
            prgL.Value = progL;
            prgR.Value = progR;

            switch (selectedVis)
            {
                case VIS_SPECTRUM:
                    pbVis.Image = vis.CreateSpectrum(MainForm.stream, pbVis.Width, pbVis.Height,
                        Color.Lime, Color.Red, Color.Black, false, false, false);
                    break;

                case VIS_SPECTRUMLINE:
                    pbVis.Image = vis.CreateSpectrumLine(MainForm.stream, pbVis.Width, pbVis.Height,
                        Color.Lime, Color.Red, Color.Black, 3, 3, false, false, false);
                    break;
                
                case VIS_SPECTRUMWAVE:
                    pbVis.Image = vis.CreateSpectrumWave(MainForm.stream, pbVis.Width, pbVis.Height,
                        Color.Lime, Color.Red, Color.Black, 1, false, false, false);
                    break;
            }
        }

        private void spectrumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUM);
        }

        private void spectrumLineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMLINE);
        }

        private void spectrumWaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMWAVE);
        }

        private void SetVisualization(int vis)
        {
            selectedVis = vis;
            switch (vis)
            {
                case VIS_SPECTRUM:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;

                case VIS_SPECTRUMLINE:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumLineToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;

                case VIS_SPECTRUMWAVE:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumWaveToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;
            }
        }
    }
}
