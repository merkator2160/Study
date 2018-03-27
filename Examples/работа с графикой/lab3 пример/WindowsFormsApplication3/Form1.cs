using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p=new Pen(Brushes.Plum);
            Random r=new Random();
            Brush b = new SolidBrush(this.BackColor);
            float x, y;
            for (x = 0; x < this.Width; x++)
            {
                y = (float)Math.Abs((this.Height - 40) * Math.Sin(x / 50));
                g.FillEllipse(Brushes.Plum, x, y, (float)15, (float)15);
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                g.FillEllipse(b, x, y, (float)15, (float)15);
            }
     

            
        }
    }
}
