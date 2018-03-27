using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Decompose
{
    public partial class Decompose : Form
    {
        public Decompose()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int64 num;
            string result_str = "";
            num = Convert.ToInt64(Math.Abs(Convert.ToDecimal(textBox1.Text)));
            Int64[,] result = new Int64[20, 2];
            result = SearchPrimes(num);

            if (checkBox1.Checked)
            {
                foreach (int i in result)
                {
                    result_str += Convert.ToString(i);
                }
            }
            else 
            {
                int j = 0;
                result_str += "Число " + num + " раскладывается на следующие простые сомножители в степенях: \n\n";
                do
                {                    
                    result_str += result[0, j] + " в " + result[1, j] + "\n";
                    j++;
                } while (result[0, j] != 0);
            }
            richTextBox1.Text = result_str;
        }


        // Разложение на сомножитеи в степенях алгоритмом Эвклида.
        private Int64[,] SearchPrimes(Int64 N)
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
                        PK[0,KN] = i;
                        PK[1,KN] = j;
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
    }
}
