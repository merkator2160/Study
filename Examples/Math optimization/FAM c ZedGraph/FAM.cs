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
    public partial class FAM : Form
    {
        //Оптимальные параметры Модели1 преобразователя с перестраиваемым преселектором
        double Qopt;
        double DFopt;        
        double Cmin;
        double Cmax;
        //Максимальное и минимальное значение смешиваемых частот
        double Qmin;
        double Qmax;
        //Входные минимальные и максимальные частоты на входах преобразователя
        double Fmin1;
        double Fmax1;
        double Fmin2;
        double Fmax2;
        //Рабочая частота
        double F1;
        //Размах общего диапазона на входе и выходе преобразователя и их ближайших 4-х комбинационых частот
        double Fmin;
        double Fmax;
        //Входная частота преобразователя при нулевом диапазоне
        double Fbix;
        //Диапазон комбинационных частот преобраователя
        double Fk1min;
        double Fk1max;
        double Fk2min;
        double Fk2max;
        double Fk3min;
        double Fk3max;
        double Fk4min;
        double Fk4max;
        //Оптимальные параметры вычесленные по другой формуле
        double QoptOther;        
        //Место сохранения результатов
        string FileOutModel1 = "Model1.txt";

        //-----------------------------------------------------------

        //Установка параметров номограммы комбинационных частот
        double TN;
        double TC;
        //Параметр отвечающий за рассчет оптимальных параметров преобразователя и его текущих значений
        bool Optimum = true;
        //Начальые параметры преобразователя частоты
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
        
        int Kp = 5;
        double C1 = 0.5;
        double C2 = 0.5;
        int M = 1;
        int S = 2;
        double Qnom = 0.1;
        double QFinal = 10;
        double DF = 10;
        double FAbs = 100;

        bool IdQ = false;  
        bool DFBandwith = true;        
        int SignalType = 2;

        //******************************************************************************
        
        public FAM()
        {
            InitializeComponent();
            DataLoad.DataRefresh = new DataLoad.RefreshEvent(DataRefresh);  //обработчик события перезагрузки данных
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("config.txt");
            if (file.Exists == true)
            {
                StreamReader Reader = new StreamReader("config.txt");
                string str = "";
                while (!Reader.EndOfStream)
                {
                    str += Reader.ReadLine();
                }
                Reader.Close();
                
                string[] value = str.Split(':');

                Kp = Convert.ToInt16(value[0]);
                C1 = Convert.ToDouble(value[1]);
                C2 = Convert.ToDouble(value[2]);
                M = Convert.ToInt16(value[3]);
                S = Convert.ToInt16(value[4]);
                Qnom = Convert.ToDouble(value[5]);
                QFinal = Convert.ToDouble(value[6]);
                DF = Convert.ToDouble(value[7]);
                FAbs = Convert.ToDouble(value[8]);
                IdQ = Convert.ToBoolean(value[9]);
                DFBandwith = Convert.ToBoolean(value[10]);
                SignalType = Convert.ToInt16(value[11]);               
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            //программа вычисления оптимальных параметров преобразователя частоты и построения графиков
            double kk;
            double f2;

            //вычислеие оптимальных параметров
            Calc();

            if (Optimum)  //сработает если TRUE
            {
                //вывод оптимальных параметров/*
                toolStripStatusLabel3.Text = Convert.ToString(Qopt);
                toolStripStatusLabel5.Text = Convert.ToString(DFopt);
                toolStripStatusLabel7.Text = Convert.ToString(Cmin);
                toolStripStatusLabel9.Text = Convert.ToString(Cmax);
                
                //пересчет знаков преобразования частоты
                if (M + S == 3 && !IdQ)
                {
                    M = 1;
                    S = 2;
                }
                else
                {
                    M = 2;
                    S = 1;
                }
                
                //вычисление параметров области фильтрации на номограмме комбинационных частот
                
                //суммирование и вычитание частот ВЫНЕСЕНО ИЗ УСЛОВИЯ
                Fmin1 = Qopt - C1 * DFopt;                    
                Fmax1 = Qopt + (1 - C1) * DFopt;                    
                Fmin2 = 1 - C2 * DFopt;                    
                Fmax1 = 1 + (1 - C2) * DFopt;               
                
                if (M + S == 4)  //CASE True
                {
                                        
                    if (!IdQ)
                    {
                        Qmin = Fmin1 / Fmax2;
                        Qmax = Fmax1 / Fmin2;
                    }
                    else 
                    {
                        Qmin = Fmin2 / Fmax1;
                        Qmax = Fmax2 / Fmin1;
                    }                               
                }
                else
                {
                    if (!IdQ)
                    {
                        if (Fmin2 != 0)
                            Qmin = Fmin1 / Fmin2;
                        else
                            Qmin = Cmin;
                    }
                    else
                    {
                        if (Fmin1 != 0)
                            Qmin = Fmin2 / Fmin1;
                        else
                            Qmin = Cmin; 
                    }

                    if (!IdQ)
                        Qmax = Fmax1 / Fmax2;
                    else
                        Qmax = Fmax2 / Fmax1;
                    if (!IdQ)
                        Qmax = Fmax1 / Fmax2;
                    else
                        Qmax = Fmax2 / Fmax1;                                    
                }
                
                //Q = Qopt     
                //DF = DFopt

                //корректировка параметра DFabs

            }
            else  //CASE False
            {

            }
        }        
        
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Options Form3 = new Options();
            Form3.Owner = this;         //Передаём вновь созданной форме её владельца.
            Form3.Show();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            Nomogramma Form2 = new Nomogramma();
            Form2.Owner = this;         //Передаём вновь созданной форме её владельца.
            DataLoad.NomogrammaDataLoad(Kp);  //загрузка данных в форму Nomogramma
            Form2.Show();            
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            if (Optimum == true)
            {
                Optimum = false;
                toolStripStatusLabel1.Text = "Текущие:";
                toolStripLabel5.Text = "Текущие параметры";
            }
            else 
            {
                Optimum = true;
                toolStripStatusLabel1.Text = "Оптимум:";
                toolStripLabel5.Text = "Оптимальные параметры";
            }
        }

        private void DataRefresh()
        {
            FileInfo file = new FileInfo("config.txt");
            if (file.Exists == true)
            {
                StreamReader Reader = new StreamReader("config.txt");
                string str = "";
                while (!Reader.EndOfStream)
                {
                    str += Reader.ReadLine();
                }
                Reader.Close();

                string[] value = str.Split(':');

                Kp = Convert.ToInt16(value[0]);
                C1 = Convert.ToDouble(value[1]);
                C2 = Convert.ToDouble(value[2]);
                M = Convert.ToInt16(value[3]);
                S = Convert.ToInt16(value[4]);
                Qnom = Convert.ToDouble(value[5]);
                QFinal = Convert.ToDouble(value[6]);
                DF = Convert.ToDouble(value[7]);
                FAbs = Convert.ToDouble(value[8]);
                IdQ = Convert.ToBoolean(value[9]);
                DFBandwith = Convert.ToBoolean(value[10]);
                SignalType = Convert.ToInt16(value[11]);
            }            
        }  //перезагрузка данных в форму
          
        private void Calc()
        {
            throw new NotImplementedException();
        }  //должно что-то считать (пока заглушка)

        private void FamHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("FamHelp.txt");
        }  //HELP отрывает
    }
}
