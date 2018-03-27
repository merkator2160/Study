using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace FAM
{
    public partial class Options : Form
    {        
        public Options()
        {
            InitializeComponent();
        }
        private void Options_Load(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("config.txt");
            if (file.Exists)
            {
                using (StreamReader Reader = new StreamReader("config.txt"))
                {
                    string str = "";

                    while (!Reader.EndOfStream)
                        str += Reader.ReadLine();

                    Reader.Close();

                    string[] value = str.Split(':');

                    //(0)Кр.
                    //(1)С1.
                    //(2)С2.
                    //(3)M - идент знака преобразования по частоте F1 M.
                    //(4)S - идент знака преобразования по частоте F2 S.
                    //(5)Номинальное Q.
                    //(6)Итоговое Q.
                    //(7)DF - текущее значеие параметра относительной диапазонной работы.
                    //(8)Абсолютное значение частоты F.
                    //(9)Управление Q: 1 - Q<1; 2 - Q>1.
                    //(10)Диапазон частот: 1 - текущее; 2 - максимальное.
                    //(11) 1 -С игнал; 2 - Гетеродин; 3 - П.Ч.

                    textBox1.Text = value[0];
                    textBox2.Text = value[1];
                    textBox3.Text = value[2];
                    textBox4.Text = value[3];
                    textBox5.Text = value[4];
                    textBox6.Text = value[5];
                    textBox7.Text = value[6];
                    textBox8.Text = value[6];
                    textBox9.Text = value[8];

                    switch (value[9])
                    {
                        case "false":
                            radioButton1.Checked = true;
                            break;
                        case "true":
                            radioButton2.Checked = true;
                            break;
                        default:
                            break;
                    }
                    switch (value[10])
                    {
                        case "false":
                            radioButton3.Checked = true;
                            break;
                        case "true":
                            radioButton4.Checked = true;
                            break;
                        default:
                            break;
                    }
                    switch (value[11])
                    {
                        case "1":
                            radioButton5.Checked = true;
                            break;
                        case "2":
                            radioButton6.Checked = true;
                            break;
                        case "3":
                            radioButton7.Checked = true;
                            break;
                        default:
                            break;
                    }
                }
            }            
        }        
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            str += textBox1.Text + ":"; //Кр
            str += textBox2.Text + ":"; //С1
            str += textBox3.Text + ":"; //С2
            str += textBox4.Text + ":"; //M
            str += textBox5.Text + ":"; //S            
            str += textBox6.Text + ":"; //Q
            str += textBox7.Text + ":"; //Итоговое Q              
            str += textBox8.Text + ":"; //DF
            str += textBox9.Text + ":"; //Абсолютное значение частоты F
            
            if (radioButton1.Checked)//Управление Q: Q<1
                str += "false:";  
            if (radioButton2.Checked)//Управление Q: Q>1
                str += "true:";  
            if (radioButton3.Checked)//Диапазон частот: текущее
                str += "false:";     
            if (radioButton4.Checked)//Диапазон частот: максимальное
                str += "true:";     
            if (radioButton5.Checked)//Сигнал
                str += "1";     
            if (radioButton6.Checked)//Гетеродин
                str += "2";     
            if (radioButton7.Checked)//П.Ч.
                str += "3";      
                        

            FileInfo file = new FileInfo("config.txt");
            if (!file.Exists) 
            {
                using (FileStream config = file.Create())
                {
                    config.Close();
                }
            }

            using (StreamWriter Writer = new StreamWriter("config.txt"))
            {
                Writer.WriteLine(str);
                Writer.Close();
            }

            //FAM обновляет данные из файла
            DataTransfer.DataRefresh();

            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {            
            if (radioButton1.Checked)
            {
                if (Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox5.Text) == 3)
                {
                    textBox4.Text = "1";
                    textBox5.Text = "2";
                }
                if (Convert.ToDouble(textBox7.Text) > 1)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));                                                
            }

            if (radioButton2.Checked)
            {
                if (Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox5.Text) == 3)
                {
                    textBox4.Text = "2";
                    textBox5.Text = "1";
                }
                if (Convert.ToDouble(textBox7.Text) != 0)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox4.Text) >2)
            {
                textBox4.Text = "2";
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox5.Text) > 2)
            {
                textBox5.Text = "2";
            }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {                
                if (Convert.ToDouble(textBox7.Text) > 1)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));
            }
            if (radioButton2.Checked)
            {                
                if (Convert.ToDouble(textBox7.Text) != 0)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (Convert.ToDouble(textBox7.Text) > 1)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));
            }
            if (radioButton2.Checked)
            {
                if (Convert.ToDouble(textBox7.Text) != 0)
                    textBox7.Text = Convert.ToString(1 / Convert.ToDouble(textBox6.Text));
            }
        }        
    }
}
