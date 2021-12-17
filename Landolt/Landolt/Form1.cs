using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

        Report report;

        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();

            this.Height = panel1.Location.Y + panel1.Height + 50;            
            this.Width = button1.Location.X + 200;

            button2.Enabled = false;
        }

        void SetMatrix()
        {
            report = new Report();
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

        void GetReport()
        {
            int count = 0;
            // проход по матрице
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    count++;
                    UserControl1 u = ctrls[r][c];
                    if (u.Type == targetType)
                        report.n++;

                    if (u.Checked)
                    {
                        report.M++;
                        if (u.Type == targetType)
                        {
                            report.S++;
                            report.N = count;
                            report.C = r + 1;
                        }
                        // неверно отмеченный
                        if (u.Type != targetType)
                            report.O++;
                    }
                    else
                    {
                        // пропущенный
                        if (u.Type == targetType)
                            report.P++;
                    }
                }
            }

            textBox4.Text = report.ToString();
            button2.Enabled = true;
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

            GetReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!started)
            {
                if(textBox1.Text == string.Empty ||
                    textBox2.Text == string.Empty ||
                    textBox3.Text == string.Empty)
                {
                    MessageBox.Show("Заполните все поля!", "Внимание!");
                    return;
                }

                textBox4.Text = string.Empty;
                button2.Enabled = false;
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
            report.t += 1;
            timeLeft -= 1;
            ShowTimeLeft(timeLeft);
            if (timeLeft == 0)
            {
                MessageBox.Show("Время вышло, тест завершен!", "Внимание!");
                Finish();
                label3.Text = "Осталось времени:";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "txt files (*.txt)|*.txt";
                dialog.FileName = string.Format("{0} {1} {2} {3}",
                    textBox1.Text, textBox2.Text, textBox3.Text, DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));
                var r = dialog.ShowDialog();
                if (r == DialogResult.OK)
                {
                    string content = string.Format("{0} {1} {2} {3}",
                    textBox1.Text, textBox2.Text, textBox3.Text, DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));
                    content += Environment.NewLine;
                    content += report.ToString();
                    File.WriteAllText(dialog.FileName, content);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}
