using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class addUrlForm : Form
    {
        public string path = "";
        
        public addUrlForm()
        {
            InitializeComponent();
            txtPath.Select();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "")
            {
                DialogResult = DialogResult.None;
                return;
            }
            path = txtPath.Text;
            Close();
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnOK.PerformClick();
        }
    }
}
