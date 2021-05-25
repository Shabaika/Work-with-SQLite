using System;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            checkBox1.Checked = Properties.Settings.Default.information;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.information = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = Properties.Settings.Default.timer;
        }
    }
}
