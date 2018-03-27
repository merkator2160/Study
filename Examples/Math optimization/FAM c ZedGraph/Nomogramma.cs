using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;

namespace FAM
{
    public partial class Nomogramma : Form
    {
        GraphPane Pane;  //полотно для рисования

        //входые данные для построения номограммы
        int Kp;
        
        //******************************************************************************

        public Nomogramma()
        {
            InitializeComponent();
            DataLoad.NomogrammaDataLoad = new DataLoad.DataLoadEvent(DataInput);  //обработчик события загрузки данных
        }

        private void DataInput(int DATA)
        {            
            Kp = DATA;            
        }    //загрузка данных в форму

        private void Nomogramma_Load(object sender, EventArgs e)
        {
            // Получим панель для рисования
            Pane = new GraphPane(ClientRectangle, "Nomogramma", "X", "Y");
            
            zedGraphControl1.Size = this.ClientSize;

            int arraySize = EulerSum(Kp) + 1;

            int[,] Forward = new int[2, arraySize];
            int[,] Backward = new int[2, arraySize];

            Forward = fareyForward(Kp, (EulerSum(Kp) + 1));
            Backward = fareyBackward(Kp, (EulerSum(Kp) + 1));

            string str = "";
            str += "В прямую сторону: \r\n\r\n";
            for (int i = 0; i != arraySize; i++)
            {
                str += Convert.ToString(Forward[0, i]) + "/" + Forward[1, i] + ", ";
            }
            str += "\r\n\r\n" + "Число элементов последовательности: " + Convert.ToString(arraySize);
            str += "\r\n\r\n\r\n";
            str += "В обратную сторону: \r\n\r\n";
            for (int i = 0; i != arraySize; i++)
            {
                str += Convert.ToString(Backward[0, i]) + "/" + Backward[1, i] + ", ";
            }
            str += "\r\n\r\n" + "Число элементов последовательности: " + Convert.ToString(arraySize);
            log(str);

            DrawGrid();
        }

        private void NomogrammaHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("NomogrammaHelp.txt");
        }

        private void Draw() 
        {
            Pane.CurveList.Clear();

            PointPairList coordinats = new PointPairList();

            // Интервал, в котором будут лежать точки
            int xmin = 0;
            int xmax = 1;
            int ymin = 1;
            int ymax = 2;

            // Заполняем список точек

            //строим график
            /*LineItem myCurve = Pane.AddCurve("", coordinats, Color.Gray, SymbolType.Default);
            myCurve.Line.IsVisible = false;
            Pane.YAxis.MajorGrid.IsZeroLine = false;
            myCurve.Symbol.Fill.Color = Color.Gray;
            myCurve.Symbol.Fill.Type = FillType.Solid;
            myCurve.Symbol.Size = 1;*/

            // Включаем отображение сетки напротив крупных рисок по оси X    
            Pane.XAxis.MajorGrid.IsVisible = true;    
            Pane.XAxis.MajorGrid.DashOn = 10; 
            Pane.XAxis.MajorGrid.DashOff = 5;

            // Включаем отображение сетки напротив крупных рисок по оси Y    
            Pane.YAxis.MajorGrid.IsVisible = true;
            Pane.YAxis.MajorGrid.DashOn = 10;
            Pane.YAxis.MajorGrid.DashOff = 5;

            Pane.YAxis.MinorGrid.IsVisible = true;
            Pane.YAxis.MinorGrid.DashOn = 1;
            Pane.YAxis.MinorGrid.DashOff = 2;

            Pane.XAxis.MinorGrid.IsVisible = true;   
            Pane.XAxis.MinorGrid.DashOn = 1;
            Pane.XAxis.MinorGrid.DashOff = 2;
            
            //установка интервалов
            Pane.XAxis.Scale.Min = xmin;
            Pane.XAxis.Scale.Max = xmax;

            // Устанавливаем интересующий нас интервал по оси Y
            Pane.YAxis.Scale.Min = ymin;
            Pane.YAxis.Scale.Max = ymax;

            Pane.AxisChange();
            zedGraphControl1.GraphPane = Pane;
            zedGraphControl1.Refresh();
        }

        //FUNCTIONS*********************************************************************                        
        
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
            return fareyForward;
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
        }  //вычисление суммы функций Эйлера (размер массива)
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
        }  //разбиение на простые сомножители
        private void log(string str) 
        {
            FileInfo file = new FileInfo("log.txt");
            if (!file.Exists)
            {
                FileStream log = file.Create();
                log.Close();
            }

            switch (str)
            {
                case "clear":                    
                    using (FileStream stream = new FileStream("log.txt", FileMode.Truncate))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {                        
                        writer.Close();
                    }
                    break;
                
                default:
                    str += "\r\n\r\n************************(NEXT)*************************\r\n";
                    using (FileStream stream = new FileStream("log.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }
        }  //писалка логов

        //NOT FOR USE*******************************************************************
        
        private void DrawGrid()
        {
            int xmin = 0;
            int xmax = 1;
            int ymin = 1;
            int ymax = 2;

            int arraySize = EulerSum(Kp + 1);
             
            Pane.CurveList.Clear();

            Pane.YAxis.MajorGrid.IsZeroLine = false;
            Pane.XAxis.MajorGrid.IsZeroLine = false;

            Pane.XAxis.Scale.Min = xmin;
            Pane.XAxis.Scale.Max = xmax;
            Pane.YAxis.Scale.Min = ymin;
            Pane.YAxis.Scale.Max = ymax;

            LineItem[] Line = new LineItem[20];                        
            
            PointPairList[,] coordinats = new PointPairList[2,20];  // 0 - по горизонтали, 1 - по вертикали

            int i = 0;  //номер линии
            //горизонтальные линии
            for (double y = ymin + 0.1; y <= ymax + 0.1; y += 0.1)
            {
                coordinats[0, i].Add(0, y);
                coordinats[0, i].Add(1, y);
                Line[i] = Pane.AddCurve("", coordinats[0, i], Color.Black, SymbolType.Default);
                Line[i].Line.IsVisible = true;
                i++;
            }


            //вертикальные линии
            for (double x = xmin + 0.1; x <= xmax + 0.1; x += 0.1)
            {
                coordinats[1, i].Add(x, 1);
                coordinats[1, i].Add(x, 2);
                Line[i] = Pane.AddCurve("", coordinats[1, i], Color.Black, SymbolType.Default);
                Line[i].Line.IsVisible = true;
                i++;
            }




            Pane.AxisChange();            
            zedGraphControl1.GraphPane = Pane;
            zedGraphControl1.Refresh();
        }  //рисуем сетку
        private void DrawTest()
        {
            // coordinats-хранит координаты точек функции
            Dictionary<double, double> coordinats = new Dictionary<double, double>();

            //
            for (double y = 1; y <= 19; y++)
            {
                for (double x = 0; x <= 1; x += 0.01)
                {
                    coordinats.Add(1, y);
                }
            }
            
            GraphPane Grid = new GraphPane();
            zedGraphControl1.GraphPane = Grid;

            Grid.XAxis.Title.Text = "Координата X";//подпись оси X
            Grid.YAxis.Title.Text = "Координата Y";//подпись оси Y

            //подпись графика
            Grid.Title.Text = "График функции y=x^2";        

            //фон графика заливаем градиентом
            Grid.Fill = new Fill(Color.White, Color.LightSkyBlue, 45.0f);            

            Grid.Chart.Fill.Type = FillType.None;
            Grid.Legend.Position = LegendPos.Float;
            Grid.Legend.IsHStack = false;

            //строим график, цвет линии синий
            LineItem myCurve = Grid.AddCurve("y=x^2",coordinats.Keys.ToArray(), coordinats.Values.ToArray(), Color.Gold,SymbolType.Triangle);
            

            myCurve.Symbol.Fill = new Fill(Color.White);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
            zedGraphControl1.Visible = true;        
        }  //тест рисование кривой графика
        private void DrawTest2()
        {
            GraphPane Pane = new GraphPane();
            
            Pane.CurveList.Clear();

            PointPairList list = new PointPairList();

            int xmin = -100;
            int xmax = 100;
            int ymin = -100;
            int ymax = 100;
            int pointsCount = 50;

            Random rnd = new Random();

            for (int i = 0; i < pointsCount; i++)
            {
                int x = rnd.Next(xmin, xmax);
                int y = rnd.Next(ymin, ymax);

                list.Add(x, y);
            }
            
            LineItem myCurve = Pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Diamond);
            
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Fill.Color = Color.Gray;
            myCurve.Symbol.Fill.Type = FillType.Solid;
            myCurve.Symbol.Size = 7;
                       
            Pane.XAxis.Scale.Min = xmin;
            Pane.XAxis.Scale.Max = xmax;                        
            Pane.YAxis.Scale.Min = ymin;
            Pane.YAxis.Scale.Max = ymax;
           
            Pane.AxisChange();

            zedGraphControl1.GraphPane = Pane;
            zedGraphControl1.Refresh();
        }  //тест2 рисование массива точек
        private void DrawTest3()
        {
            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            Pane.CurveList.Clear();

            // Создадим список точек
            PointPairList coordinats = new PointPairList();

            // Интервал, в котором будут лежать точки
            int xmin = 0;
            int xmax = 1;
            int ymin = 1;
            int ymax = 2;
                            
            // Включаем отображение сетки напротив крупных рисок по оси X    
            Pane.XAxis.MajorGrid.IsVisible = true;

    
            // Задаем вид пунктирной линии для крупных рисок по оси X:    
            // Длина штрихов равна 10 пикселям, ...     
            Pane.XAxis.MajorGrid.DashOn = 10;
    
            // затем 5 пикселей - пропуск    
            Pane.XAxis.MajorGrid.DashOff = 5;
    
            // Включаем отображение сетки напротив крупных рисок по оси Y    
            Pane.YAxis.MajorGrid.IsVisible = true;    
        
            //Аналогично задаем вид пунктирной линии для крупных рисок по оси Y    
            Pane.YAxis.MajorGrid.DashOn = 10;    
            Pane.YAxis.MajorGrid.DashOff = 5;
    
            // Включаем отображение сетки напротив мелких рисок по оси X    
            Pane.YAxis.MinorGrid.IsVisible = true;
    
            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...     
            Pane.YAxis.MinorGrid.DashOn = 1;
    
            // затем 2 пикселя - пропуск    
            Pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y    
            Pane.XAxis.MinorGrid.IsVisible = true;
    
            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y    
            Pane.XAxis.MinorGrid.DashOn = 1;    
            Pane.XAxis.MinorGrid.DashOff = 2;

            // Устанавливаем интересующий нас интервал по оси X
            Pane.XAxis.Scale.Min = xmin;
            Pane.XAxis.Scale.Max = xmax;

            // Устанавливаем интересующий нас интервал по оси Y
            Pane.YAxis.Scale.Min = ymin;
            Pane.YAxis.Scale.Max = ymax;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            // В противном случае на рисунке будет показана только часть графика, 
            // которая умещается в интервалы по осям, установленные по умолчанию
            Pane.AxisChange();

            //определяем панель рисования
            zedGraphControl1.GraphPane = Pane;
            
            // Обновляем график
            zedGraphControl1.Refresh(); 
        }  //тест 3 сетка
        private void Nomogramma_Resize(object sender, EventArgs e)
        {
            Pane.Rect = ClientRectangle;
            zedGraphControl1.GraphPane = Pane;
            zedGraphControl1.Size = this.ClientSize;
            zedGraphControl1.Refresh();
        }
    }
}
