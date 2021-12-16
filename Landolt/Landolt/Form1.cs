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
        int rows = 20;
        int cols = 20;
        int size = 1;
        int targetType = 0;
        bool started = false;

        List<List<UserControl1>> ctrls;

        Timer timer = new Timer();
        int seconds = 0;// время в секундах
        int timeLeft = 0;

        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
                        
            this.Height = button1.Location.Y + 150;            
        }

        void SetMatrix()
        {
            panel1.Controls.Clear();

            ctrls = new List<List<UserControl1>>();

            rows = (int)numericUpDown1.Value;
            cols = (int)numericUpDown1.Value;

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

        void Finish()
        {
            timer.Stop();
            started = false;
            button1.Text = "Старт";
            // Проверка проходом по матрице
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    UserControl1 u = ctrls[r][c];
                    if (u.Checked)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(!started)
            {
                SetMatrix();
                seconds = ((int)numericUpDown2.Value) * 60;
                timeLeft = seconds;
                started = true;
                button1.Text = "Завершить";
                ShowTimeLeft(timeLeft);
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                Finish();
            }
        }

        void ShowTimeLeft(int timeLeft)
        {
            TimeSpan span = TimeSpan.FromSeconds(timeLeft);
            label3.Text = string.Format("Осталось времени: {0:00}:{1:00}", span.Minutes, span.Seconds);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft -= 1;
            ShowTimeLeft(timeLeft);
            if(timeLeft == 0)
                Finish();
        }
    }
}
