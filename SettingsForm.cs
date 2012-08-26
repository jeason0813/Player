using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;
using System.Runtime.InteropServices;

namespace Player
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            LoadDevices();

            txtProxy.Text = Properties.Settings.Default.Proxy;
        }

        private void LoadDevices()
        {
            for (int i = 1; i < Bass.BASS_GetDeviceCount(); i++)
            {
                BASS_DEVICEINFO info = Bass.BASS_GetDeviceInfo(i);
                if (info.IsEnabled)
                    cmbDevice.Items.Add(info.name);
            }

            if (Properties.Settings.Default.Device == -1)
            {
                if (cmbDevice.Items.Count > 0)
                    cmbDevice.SelectedIndex = 0;
            }
            else
                cmbDevice.SelectedIndex = Properties.Settings.Default.Device - 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Device = cmbDevice.SelectedIndex + 1;
            Properties.Settings.Default.Proxy = txtProxy.Text;

            Properties.Settings.Default.Save();
        }
    }
}
