using System;
using System.Windows.Forms;
using System.IO;
using Un4seen.Bass;

namespace Player
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //passing a path argument sets the working directory of the exe to that file's directory; bypass it
            Directory.SetCurrentDirectory(Application.ExecutablePath.Replace(Path.GetFileName(Application.ExecutablePath), ""));
            
            if (!File.Exists("bass.dll"))
            {
                MessageBox.Show("bass.dll not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Properties.Settings.Default.RegEmail == "" || Properties.Settings.Default.RegCode == "")
                MessageBox.Show("You haven't set your BASS.NET registration details. A splash screen will be shown.\nYou can set details in app.config.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            BassNet.Registration(Properties.Settings.Default.RegEmail, Properties.Settings.Default.RegCode);

            if (init())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                Bass.BASS_Free();
            }
            else
            {
                MessageBox.Show("Init error: " + Bass.BASS_ErrorGetCode(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool init()
        {
            Bass.BASS_Free();

            if (Bass.BASS_Init(Properties.Settings.Default.Device, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                return true;
            else
                return false;
        }
    }
}
