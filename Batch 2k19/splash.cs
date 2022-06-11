using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Batch_2k19
{
    public partial class splash : Form
    {
        int r, g, b;

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public splash()
        {
            InitializeComponent();

            r = 0;
            g = 0;
            b = 0;

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            if (r < 31)
                r += 1;
            if (g < 131)
                g += 2;
            if (b < 201)
                b += 5;

            this.BackColor = Color.FromArgb(r, g, b);

            if (r >= 30 && g >= 130 && b >= 200)
            {
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                timer1.Enabled = false;
                form1.Show();
            }
        }
    }
}
