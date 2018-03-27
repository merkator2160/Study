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
    public partial class FAMForm : Form
    {
        //GLOBAL_DATA
        //////////////////////////////////////////////////////////////////////////////////
        FAM data;
        //////////////////////////////////////////////////////////////////////////////////
        
        public FAMForm()
        {
            InitializeComponent();
            DataTransfer.DataRefresh = new DataTransfer.RefreshEvent(DataRefresh);  //обработчик события перезагрузки данных
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new FAM();  //инициализация класса данных FAM
            DataRefresh();  //обновление данныйх в классе из файла
        }
        private void Save_Click(object sender, EventArgs e)
        {

        }  //сохранение результатов расчета
        private void Calc_Click(object sender, EventArgs e)
        {
            data.Calc();
            if (data.Optimum)  //вывод параметров рассчета
            {
                //вывод оптимальных параметров            
                toolStripStatusLabel3.Text = Convert.ToString(data.Qopt);
                toolStripStatusLabel5.Text = Convert.ToString(data.DFopt);
                toolStripStatusLabel7.Text = Convert.ToString(data.Cmin);
                toolStripStatusLabel9.Text = Convert.ToString(data.Cmax);

                data.OptimumTRUE();  //программа вычисления оптимальных параметров преобразователя частоты
            }
            else  //CASE False
            {
                data.OptimumFALSE();  //программа вычисления текущих параметров преобразователя частоты

                //вывод текущих параметров            
                toolStripStatusLabel3.Text = Convert.ToString(data.Qopt);
                toolStripStatusLabel5.Text = Convert.ToString(data.DFopt);
                toolStripStatusLabel7.Text = Convert.ToString(data.Cmin);
                toolStripStatusLabel9.Text = Convert.ToString(data.Cmax);

                data.NomogrammaRegion();  //вычисление параметров области фильтрации на номограмме комбинацинных частот
            }

            DrawFilter();  //построение области фильтрации на номограмме комбинационных частот
            CalcDrawFriqency();  //построение всех чатот в абсолютном масштабе            

        }  //вычислеие оптимальных параметров
        private void Options_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show(this);  //Передаём вновь созданной форме её владельца, и запускаем
        }  //Open Options
        private void Nomogramma_Click(object sender, EventArgs e)
        {
            NomogrammaForm nomogramma = new NomogrammaForm();
            DataTransfer.DataUpLoad(data.Kp);
            nomogramma.Show(this);  //Передаём вновь созданной форме её владельца, и запускаем
        }  //Open Nomogramma
        private void Optimum_Click(object sender, EventArgs e)
        {
            if (data.Optimum == true)
            {
                data.Optimum = false;
                toolStripStatusLabel1.Text = "Текущие:";
                Optimum.Text = "Текущие параметры";
            }
            else 
            {
                data.Optimum = true;
                toolStripStatusLabel1.Text = "Оптимум:";
                Optimum.Text = "Оптимальные параметры";
            }
        }  //Optimum
        private void FamHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("FamHelp.txt");
        }  //HELP отрывает
        //FUNCTIONS*********************************************************************

        private void DataRefresh()
        {
            //загрузка данных из файла
            FileInfo file = new FileInfo("config.txt");
            if (file.Exists)
            {
                string str = "";
                using (StreamReader Reader = new StreamReader("config.txt"))
                {
                    while (!Reader.EndOfStream)
                    {
                        str += Reader.ReadLine();
                    }
                    Reader.Close();
                }

                string[] value = str.Split(':');

                data.Kp = Convert.ToInt16(value[0]);             //(0)Кр.
                data.C1 = Convert.ToDouble(value[1]);            //(1)С1.
                data.C2 = Convert.ToDouble(value[2]);            //(2)С2.
                data.M = Convert.ToInt16(value[3]);              //(3)M - идент знака преобразования по частоте F1 M.
                data.S = Convert.ToInt16(value[4]);              //(4)S - идент знака преобразования по частоте F2 S.
                data.Qnom = Convert.ToDouble(value[5]);          //(5)Номинальное Q.
                data.QFinal = Convert.ToDouble(value[6]);        //(6)Итоговое Q.
                data.DF = Convert.ToDouble(value[7]);            //(7)DF - текущее значеие параметра относительной диапазонной работы.
                data.FAbs = Convert.ToDouble(value[8]);          //(8)Абсолютное значение частоты F.
                data.IdQ = Convert.ToBoolean(value[9]);          //(9)Управление Q: 1 - Q<1; 2 - Q>1.
                data.DFBandwith = Convert.ToBoolean(value[10]);  //(10)Диапазон частот: 1 - текущее; 2 - максимальное.
                data.SignalType = Convert.ToInt16(value[11]);    //(11) 1 -С игнал; 2 - Гетеродин; 3 - П.Ч.
            }   
        }  //перезагрузка данных в форму
        private void DrawFilter()
        {

        }  //построение области фильтрации на номограмме комбинационных частот

        private void CalcDrawFriqency()
        {

        }  //построение всех чатот в абсолютном масштабе

        
    }
}
