using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphic
{
    public partial class Form1 : Form
    {
        float x, y, dx, dy;
        float[,] points = new float[4, 2];
        
        public Form1()
        {
            InitializeComponent();            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Aqua;

            x = ClientSize.Width;
            y = ClientSize.Height;
            dx = ClientSize.Width / x;
            dy = ClientSize.Height / y;

            label1.Text = "x: 0";
            label2.Text = "y: 0";

            points[0, 0] = 0;
            points[1, 0] = 0;
            points[2, 0] = ClientSize.Width;
            points[3, 0] = ClientSize.Height;

            points[0, 1] = 0;
            points[1, 1] = ClientSize.Height;
            points[2, 1] = ClientSize.Width;
            points[3, 1] = 0;
            
            Graphics g = this.CreateGraphics();                         
            Pen p = new Pen(Color.Red, (float)2);
                        
            g.DrawLine(p, points[0, 0] * dx, points[1, 0] * dy, points[2, 0] * dx, points[3, 0] * dy);
            g.DrawLine(p, points[0, 1] * dx, points[1, 1] * dy, points[2, 1] * dx, points[3, 1] * dy);  
            g.Dispose();            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "x: " + e.X;
            label2.Text = "y: " + e.Y;
        }

        private void label2_Resize(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Aqua;

            Graphics g = this.CreateGraphics();
            Pen r = new Pen(BackColor, (float)2);
            Pen p = new Pen(Color.Red, (float)2);

            g.DrawLine(r, points[0, 0] * dx, points[1, 0] * dy, points[2, 0] * dx, points[3, 0] * dy);
            g.DrawLine(r, points[0, 1] * dx, points[1, 1] * dy, points[2, 1] * dx, points[3, 1] * dy);
                        
            dx = ClientSize.Width / x;
            dy = ClientSize.Height / y;

            g.DrawLine(p, points[0, 0] * dx, points[1, 0] * dy, points[2, 0] * dx, points[3, 0] * dy);
            g.DrawLine(p, points[0, 1] * dx, points[1, 1] * dy, points[2, 1] * dx, points[3, 1] * dy);

            //r.Dispose();
            //p.Dispose();
            //g.Dispose();

            //this.Invalidate();
        }
    }
}
