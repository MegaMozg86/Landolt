using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Landolt
{
    public partial class UserControl1 : UserControl
    {
        public int Type { get; set; } = 0;
        public bool Checked { get; set; } = false;

        Color defaultColor;
        public UserControl1()
        {
            InitializeComponent();
            defaultColor = this.BackColor;
        }

        private void UserControl1_Click(object sender, EventArgs e)
        {
            if (!Checked)
            {
                BackColor = Color.LightGreen;
                Checked = true;
            }
            else
            {
                BackColor = defaultColor;
                Checked = false;
            }
        }
    }
}
