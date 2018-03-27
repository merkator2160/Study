using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int x, y, a, b, i, dx, dy, flag = 0, p = 0;
        public Form1()
        {
            InitializeComponent();
            b = y = this.pictureBox1.Top;
            a = x = this.pictureBox1.Left;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible = false;
            this.pictureBox1.Visible = true;
            flag = 1;
            this.label2.Text = "" + i;
            dx = dy = this.trackBar1.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                x = x + dx;
                y = y + dy;
                this.pictureBox1.Top = y;
                this.pictureBox1.Left = x;
                if (this.pictureBox1.Left >= this.ClientSize.Width - this.pictureBox1.Width) { dx = -dx; p = 0; }                
                if (this.pictureBox1.Top >= this.ClientSize.Height - this.pictureBox1.Height)
                { 
                    x = a; 
                    y = b; 
                    i++; 
                    this.label2.Text = "" + i; 
                    dx = dy = this.trackBar1.Value;
                    p = 0;
                } 

                if (this.pictureBox1.Top >= this.button3.Top - this.pictureBox1.Height&&
                    this.button3.Left<=this.pictureBox1.Left&&
                    this.pictureBox1.Left <= this.button3.Left + this.button3.Width) 
                { 
                    if (p == 0)
                    {
                    dy = -dy;
                    p = 1;
                    }
                }

                if (this.pictureBox1.Left <= 0) { dx = -dx; p = 0; }
                if (this.pictureBox1.Top <= 0) { dy = -dy; p = 0; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.button3.Left = e.X - this.button3.Width / 2;
        }
    }
}
