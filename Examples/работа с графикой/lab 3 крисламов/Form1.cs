using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab3_prob
{
    public partial class Form1 : Form
    {
        float x, y, dx, dy;

        Graphics g;
        //Pen p;
        //Random r;
           
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            g = this.CreateGraphics();
            //p = new Pen(Brushes.Plum);
            g.FillRectangle(Brushes.White, 0, 0, this.ClientSize.Width - 120, this.ClientSize.Height);
            this.Update();
            //r = new Random();
            x = this.ClientSize.Width / 2;
            y = this.ClientSize.Height/ 2;           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(dx<0)
            {
            dx = this.trackBar1.Value;
            dx = -dx;
            }
            else
            {dx = this.trackBar1.Value;}
            
            if(dy<0)
            {
            dy = this.trackBar1.Value;
            dy = -dy;
            }
            else
            {dy = this.trackBar1.Value;}   
 
            //g.FillEllipse(Brushes.White, x, y, (float)14, (float)14);
            g.FillRectangle(Brushes.White, 0, 0, this.ClientSize.Width - 120, this.ClientSize.Height);
            g.FillRectangle(Brushes.LightGray, this.ClientSize.Width - 120, 0, this.ClientSize.Width, this.ClientSize.Height);

            if (0 >= x) { dx = -dx; }
            if (this.ClientSize.Width - 14 - 120 <= x) { dx = -dx; }
            if (0 >= y) { dy = -dy; }
            if (this.ClientSize.Height - 14 <= y) { dy = -dy; }
                      
            if(this.radioButton1.Checked)
            {
                x = x + dx;
                y = y + dy;                
            }
            
            if (this.radioButton2.Checked)
            {
                x = x + dx;
                //y = (float)Math.Abs((this.Height - 40) * Math.Sin(x / 50));
                y = (float)Math.Sin(x / 50)*40 + 100;
            }
           
            g.FillEllipse(Brushes.Red, x, y, (float)14, (float)14); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.label1.Text = "X=" + e.X.ToString();
            this.label2.Text = "Y=" + e.Y.ToString();
        }
    }
}
