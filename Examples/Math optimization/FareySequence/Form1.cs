using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FareySequence
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            int Q2 = Convert.ToInt16(textBox1.Text); //Ближняя дробь
            string str = "";
            int arraySize =  EulerSum(Q2) + 1;
            int[,] Forward = new int[2, arraySize];
            int[,] Backward = new int[2, arraySize];
            Forward = fareyForward(Q2, arraySize);
            Backward = fareyBackward(Q2, arraySize);

            if (checkBox1.Checked == true)
            {
                foreach (int x in Forward)
                {
                    str += Convert.ToString(x) + " ";
                }
                str += "\n\n";
                foreach (int x in Backward)
                {
                    str += Convert.ToString(x) + " ";
                }
                richTextBox1.Text = str;
            }
            else 
            {
                str += "В прямую сторону: \n\n";
                for (int i = 0; i != arraySize; i++) 
                {
                    str += Convert.ToString(Forward[0, i]) + "/" + Forward[1, i] + ", ";
                }
                str += "\n\n" + "Число элементов последовательности: " + Convert.ToString(arraySize);
                str += "\n\n\n";
                str += "В обратную сторону: \n\n";
                for (int i = 0; i != arraySize; i++)
                {
                    str += Convert.ToString(Backward[0, i]) + "/" + Backward[1, i] + ", ";
                }
                str += "\n\n" + "Число элементов последовательности: " + Convert.ToString(arraySize);

                richTextBox1.Text = str;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            new Thread(Thread1).Start();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile("Farey.txt", RichTextBoxStreamType.PlainText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";           
        }

        private static int[,] fareyForward(int Q2, int arraySize)
        {            
            int[,] fareyForward = new int[2, arraySize];   // 0-числитель R, 1-знаменатель Q
            int i = 1;
            fareyForward[0, 0] = 0;  //int R1 = 0;  Дальняя дробь числитель
            fareyForward[1, 0] = 1;  //int Q1 = 1;  Дальняя дробь знаменатель
            fareyForward[0, 1] = 1;  //int R2 = 1;  Ближняя дробь числитель
            fareyForward[1, 1] = Q2; //int N = Q2;  Ближняя дробь знаменатель                        
            do
            {
                i++;             
                //R = (int)((Q1 + N) / Q2) * R2 - R1;
                //Q = (int)((Q1 + N) / Q2) * Q2 - Q1;
                fareyForward[0, i] = (int)((fareyForward[1, i - 2] + Q2) / fareyForward[1, i - 1]) * fareyForward[0, i - 1] - fareyForward[0, i - 2];
                fareyForward[1, i] = (int)((fareyForward[1, i - 2] + Q2) / fareyForward[1, i - 1]) * fareyForward[1, i - 1] - fareyForward[1, i - 2];               
            } while (((double)fareyForward[0, i]) / ((double)fareyForward[1, i]) < 1);                        
            return fareyForward ;
            }  //В прямую сторону!      

        private static int[,] fareyBackward(int Q2, int arraySize)
        {
            int[,] fareyBackward = new int[2, arraySize];   // 0-числитель R, 1-знаменатель Q
            int i = 1; 
 
            fareyBackward[0, 0] = 1;       //int R1 = 0;  Дальняя дробь числитель
            fareyBackward[1, 0] = 1;       //int Q1 = 1;  Дальняя дробь знаменатель
            fareyBackward[0, 1] = Q2 - 1;  //int R2 = 1;  Ближняя дробь числитель
            fareyBackward[1, 1] = Q2;      //int N = Q2;  Ближняя дробь знаменатель                        
            do
            {                
                i++;
                //R = (int)((Q1 + N) / Q2) * R2 - R1;
                //Q = (int)((Q1 + N) / Q2) * Q2 - Q1;
                fareyBackward[0, i] = (int)((fareyBackward[1, i - 2] + Q2) / fareyBackward[1, i - 1]) * fareyBackward[0, i - 1] - fareyBackward[0, i - 2];
                fareyBackward[1, i] = (int)((fareyBackward[1, i - 2] + Q2) / fareyBackward[1, i - 1]) * fareyBackward[1, i - 1] - fareyBackward[1, i - 2];
            } while (((double)fareyBackward[0, i]) / ((double)fareyBackward[1, i]) > 0);
            return fareyBackward;
        } //В обратную сторону!

        private int EulerSum(Int64 N)
        {
            Int64 i;       // количество элементов последовательности            
            int j;
            double f = 1;    // значение функции эйлера
            double Sum = 1;
            Int64[,] Decomp = new Int64[2, 20];


            for (i = 2; i != N + 1; i++)
            {
                Decomp = Decompose(i);
                f = i;
                for (j = 0; Decomp[0, j] != 0; j++)
                {
                    f = f * (1.0 - (1.0 / Decomp[0, j]));
                }
                Sum += f;
            }
            return (int)Sum;
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
                
        private void Thread1()
        {
            string m = "sdsdgw";
            richTextBox1.Text = m;            
        }
    }
}
