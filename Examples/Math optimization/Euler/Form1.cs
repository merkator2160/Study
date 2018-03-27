using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Euler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Int64 N;
            N = Convert.ToInt64(textBox1.Text);
            richTextBox1.Text += "Значение функции Эйлера = " + Euler_sum(N) + "\n";
        }

        private double Euler_sum(Int64 N)
        {
            Int64 i;       // количество элементов последовательности            
            int j;
            double f = 1;    // значение функции эйлера
            double Sum = 1;
            Int64[,] Decomp = new Int64[2, 20];
                        
            
            for(i = 2; i != N + 1; i++)
            {
                Decomp = Decompose(i);
                f = i;
                for (j = 0; Decomp[0, j] != 0; j++)
                {
                    f = f * (1.0 - (1.0 / Decomp[0, j]));                    
                }  
                Sum += f;
            }
            return (Int64)Sum;
        }
       
        private Int64[,] Decompose(Int64 N)
        {
            Int64[,] PK = new Int64[2, 20]; //20-длины строк в 2-х столбцах
            //0-простое число, 1-его степень            
            bool l = false;
            int i, j; //простое число i, на которое проиводится деление j раз
            Int64 lim;  //максимально большой простой делитель
            Int64 NP;   //N-хранит входное число, NP-с ним работает
            int KN = 0;//номер простого числа

            NP = N;
            if (NP > 0)
            {
                i = 1;
                l = false;
                lim = Convert.ToInt64(Math.Sqrt(NP));
                do
                {
                    if (i >= 3) { i = i + 2; } else i++; //ускорение программы в 2 раза
                    j = 0;
                    do
                    {
                        if (NP % i == 0)
                        {
                            NP = NP / i;
                            j++;
                            l = true;
                        }
                        else
                            break;
                    } while (NP != 1);
                    if (j != 0)
                    {
                        PK[0, KN] = i;
                        PK[1, KN] = j;
                        KN++;
                    }
                } while (i <= lim);
            }
            if (l == false)
            {
                PK[0, KN] = Convert.ToInt64(NP);
                PK[1, KN] = 1;
            }
            if (NP != 1)
            {
                PK[0, KN] = Convert.ToInt64(NP);
                PK[1, KN] = 1;
            }
            return PK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int64 N;
            N = Convert.ToInt64(textBox1.Text);
            Form2(N);
        }

        private void Form2(Int64 N)
        {
            Form Form2 = new Form();
            AddOwnedForm(Form2);
            Form2.Text = "Euler(подробно)";
            
            RichTextBox RichTextBox2 = new RichTextBox();
            RichTextBox2.Location = new Point(0,0);

            Form2.Width = 500;
            Form2.Height = 1000;
            RichTextBox2.Size = Form2.Size;
          
            Form2.Controls.Add(RichTextBox2);
            
            Form2.Show();

          /////////////////////////////////////////////////////////////
            
            Int64 i;       // количество элементов последовательности            
            int j;
            double f = 1;    // значение функции эйлера
            double Sum = 1;
            Int64[,] Decomp = new Int64[2, 20];
            string str = "Подробное вычисление значений функции Эйлер на каждом этапе рассчета: \n\n";

            for (i = 0; i != N + 1; i++)
            {
                Decomp = Decompose(i);
                f = i;
                for (j = 0; Decomp[0, j] != 0; j++)
                {
                    f = f * (1.0 - (1.0 / Decomp[0, j]));
                    str += "От элемента массива №: "
                        + Convert.ToString(j) + " равного: " + Convert.ToString(Decomp[0, j]) + " , равно: " 
                        + Convert.ToString(f) + "\n";
                }
                str += "Значение функции Эйлера на порядке: " + Convert.ToString(i) 
                        + " равно: " + Convert.ToString(f) + "\n\n";
                Sum += f;
            }
            str += "\n";
            str += "Сумарное значение функции эйлера: " + Convert.ToString(Sum) + "\n";
            RichTextBox2.Text = str;
        }
    }
}
