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
    public partial class Form1 : Form
    {
        int rows = 12;
        int cols = 12;
        int size = 1;
        int targetType = 0;

        List<List<UserControl1>> ctrls = new List<List<UserControl1>>();

        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();

            SetMatrix();
            this.Height = button1.Location.Y + 150;            
        }

        void SetMatrix()
        {
            for (int i = 0; i < rows; ++i)
                ctrls.Add(new List<UserControl1>());

            size = panel1.Width / cols;
            
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    int t = rand.Next(1, 9);
                    UserControl1 control = new UserControl1();
                    control.Location = new Point(size * c, size * r);
                    control.Width = size;
                    control.Height = size;
                    control.Type = t;
                    control.BackgroundImageLayout = ImageLayout.Stretch;
                    control.BackgroundImage = Image.FromFile("./Images/" + "_" + t.ToString() + ".png");
                    panel1.Controls.Add(control);
                    ctrls[r].Add(control);
                }
            }

            targetType = rand.Next(1, 9);
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.BackgroundImage = Image.FromFile("./Images/" + "_" + targetType.ToString() + ".png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка проходом по матрице
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    UserControl1 u = ctrls[r][c];
                    if(u.Checked)
                    {
                        // неверно отмеченный
                        if (u.Type != targetType)
                            u.BackColor = Color.Red;
                    }    
                    else
                    {
                        // пропущенный
                        if (u.Type == targetType)
                            u.BackColor = Color.Yellow;
                    }
                }
            }
        }
    }
}
