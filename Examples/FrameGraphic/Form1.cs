using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrameGraphic
{
    public partial class Form1 : Form
    {
        int HEIGHT, WIDTH;
        float dx, dy;
        int[,] points = new int[4,2];
                
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const float x = 230, y = 210;
            dx = ClientSize.Width / x;
            dy = ClientSize.Height / y;
            label1.Text = "dx: " + Convert.ToString(dx);
            label2.Text = "dy: " + Convert.ToString(dy);
            
            points[0, 0] = lineShape1.X1;
            points[1, 0] = lineShape1.Y1;
            points[2, 0] = lineShape1.X2;
            points[3, 0] = lineShape1.Y2;

            points[0, 1] = lineShape2.X1;
            points[1, 1] = lineShape2.Y1;
            points[2, 1] = lineShape2.X2;
            points[3, 1] = lineShape2.Y2;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            const float x = 230, y = 210;
            dx = ClientSize.Width / x;
            dy = ClientSize.Height / y;
            label1.Text = "dx: " + Convert.ToString(dx);
            label2.Text = "dy: " + Convert.ToString(dy);

            if(HEIGHT < this.Height && WIDTH < this.Width || HEIGHT > this.Height && WIDTH > this.Width)
            {
                HEIGHT = this.Height;
                WIDTH = this.Width;

                lineShape1.X1 = Convert.ToInt16(points[0, 0] * dx);
                lineShape1.Y1 = Convert.ToInt16(points[1, 0] * dy);
                lineShape1.X2 = Convert.ToInt16(points[2, 0] * dx);
                lineShape1.Y2 = Convert.ToInt16(points[3, 0] * dy);

                lineShape2.X1 = Convert.ToInt16(points[0, 1] * dx);
                lineShape2.Y1 = Convert.ToInt16(points[1, 1] * dy);
                lineShape2.X2 = Convert.ToInt16(points[2, 1] * dx);
                lineShape2.Y2 = Convert.ToInt16(points[3, 1] * dy);
            }
                       
        }

    }

     
}
