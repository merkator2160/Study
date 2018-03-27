using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FAM
{
    public class FAM  //самомтоятельный, не наследующий класс
    {
        #region ПЕРЕМЕННЫЕ

        //ПЕРЕМЕННЫЕ //////////////////////////////////////////////////////////////////////////////////
                
        bool optimum = true;  //Параметр отвечающий за рассчет оптимальных параметров преобразователя и его текущих значений
        
        //Начальые параметры преобразователя частоты
        int kp = 5;              //(0)Кр.
        double c1 = 0.5;         //(1)С1.
        double c2 = 0.5;         //(2)С2.
        int m = 1;               //(3)M - идент знака преобразования по частоте F1 M.
        int s = 2;               //(4)S - идент знака преобразования по частоте F2 S.
        double qnom = 0.1;       //(5)Номинальное Q.
        double qfinal = 10;      //(6)Итоговое Q.
        double df = 10;          //(7)DF - текущее значеие параметра относительной диапазонной работы.
        double fabs = 100;       //(8)Абсолютное значение частоты F.

        bool idq = false;        //(9)Управление Q: 1 - Q<1; 2 - Q>1.
        bool dfbandwith = true;  //(10)Диапазон частот: 1 - текущее; 2 - максимальное.
        int signaltype = 2;      //(11) 1 -С игнал; 2 - Гетеродин; 3 - П.Ч.
        
        //Оптимальные параметры Модели1 преобразователя с перестраиваемым преселектором
        double qopt;
        double dfopt;
        double cmin;
        double cmax;
        
        //Максимальное и минимальное значение смешиваемых частот
        double qmin;
        double qmax;
        
        //Входные минимальные и максимальные частоты на входах преобразователя
        double fmin1;
        double fmax1;
        double fmin2;
        double fmax2;
        
        double f1;  //рабочая частота
        double f2;  //частота гетеродина
        
        //Размах общего диапазона на входе и выходе преобразователя и их ближайших 4-х комбинационых частот
        double fmin;
        double fmax;
        
        //Входная частота преобразователя при нулевом диапазоне
        double fbix;
        
        //Диапазон комбинационных частот преобраователя
        double fk1min;
        double fk1max;
        double fk2min;
        double fk2max;
        double fk3min;
        double fk3max;
        double fk4min;
        double fk4max;
        
        //Оптимальные параметры вычесленные по другой формуле
        double qoptother;
        #endregion

        #region ИНТЕРФЕЙСЫ ПЕРЕМЕННЫХ

        //ИНТЕРФЕЙСЫ ПЕРЕМЕННЫХ //////////////////////////////////////////////////////////////////////////////////
                
        public bool Optimum
        {
            get
            {
                return optimum;
            }
            set
            {
                optimum = value;
            }
        }  //Параметр отвечающий за рассчет оптимальных параметров преобразователя и его текущих значений

        //Начальые параметры преобразователя частоты
        public int Kp
        {
            get
            {
                return kp;
            }
            set
            {
                kp = value;
            }
        }           //(0)Кр.
        public double C1
        {
            get
            {
                return c1;
            }
            set
            {
                c1 = value;
            }
        }        //(1)С1.
        public double C2
        {
            get
            {
                return c2;
            }
            set
            {
                c2 = value;
            }
        }        //(2)С2.
        public int M
        {
            get
            {
                return m;
            }
            set
            {
                m = value;
            }
        }            //(3)M - идент знака преобразования по частоте F1 M.
        public int S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }            //(4)S - идент знака преобразования по частоте F2 S.
        public double Qnom
        {
            get
            {
                return qnom;
            }
            set
            {
                qnom = value;
            }
        }      //(5)Номинальное Q.
        public double QFinal
        {
            get
            {
                return qfinal;
            }
            set
            {
                qfinal = value;
            }
        }    //(6)Итоговое Q.
        public double DF
        {
            get
            {
                return df;
            }
            set
            {
                df = value;
            }
        }        //(7)DF - текущее значеие параметра относительной диапазонной работы.
        public double FAbs
        {
            get
            {
                return fabs;
            }
            set
            {
                fabs = value;
            }
        }      //(8)Абсолютное значение частоты F.

        public bool IdQ
        {
            get
            {
                return idq;
            }
            set
            {
                idq = value;
            }
        }         //(9)Управление Q: 1 - Q<1; 2 - Q>1.
        public bool DFBandwith
        {
            get
            {
                return dfbandwith;
            }
            set
            {
                dfbandwith = value;
            }
        }  //(10)Диапазон частот: 1 - текущее; 2 - максимальное.
        public int SignalType
        {
            get
            {
                return signaltype;
            }
            set
            {
                signaltype = value;
            }
        }   //(11) 1 -С игнал; 2 - Гетеродин; 3 - П.Ч.

        //Оптимальные параметры Модели1 преобразователя с перестраиваемым преселектором        
        public double Qopt
        {
            get
            {
                return qopt;
            }
            set
            {
                qopt = value;
            }
        }
        public double DFopt
        {
            get
            {
                return dfopt;
            }
            set
            {
                dfopt = value;
            }
        }
        public double Cmin
        {
            get
            {
                return cmin;
            }
            set
            {
                cmin = value;
            }
        }
        public double Cmax
        {
            get
            {
                return cmax;
            }
            set
            {
                cmax = value;
            }
        }
                
        //Максимальное и минимальное значение смешиваемых частот
        public double Qmin
        {
            get
            {
                return qmin;
            }
            set
            {
                qmin = value;
            }
        }
        public double Qmax
        {
            get
            {
                return qmax;
            }
            set
            {
                qmax = value;
            }
        }
        
        //Входные минимальные и максимальные частоты на входах преобразователя        
        public double Fmin1
        {
            get
            {
                return fmin1;
            }
            set
            {
                fmin1 = value;
            }
        }
        public double Fmax1
        {
            get
            {
                return fmax1;
            }
            set
            {
                fmax1 = value;
            }
        }
        public double Fmin2
        {
            get
            {
                return fmin2;
            }
            set
            {
                fmin2 = value;
            }
        }
        public double Fmax2
        {
            get
            {
                return fmax2;
            }
            set
            {
                fmax2 = value;
            }
        }

        //Рабочая частота        
        public double F1
        {
            get
            {
                return f1;
            }
            set
            {
                f1 = value;
            }
        }
        
        //Размах общего диапазона на входе и выходе преобразователя и их ближайших 4-х комбинационых частот
        
        public double Fmin
        {
            get
            {
                return fmin;
            }
            set
            {
                fmin = value;
            }
        }
        public double Fmax
        {
            get
            {
                return fmax;
            }
            set
            {
                fmax = value;
            }
        }

        //Входная частота преобразователя при нулевом диапазоне        
        public double Fbix
        {
            get
            {
                return fbix;
            }
            set
            {
                fbix = value;
            }
        }

        //Диапазон комбинационных частот преобраователя
        public double Fk1min
        {
            get
            {
                return fk1min;
            }
            set
            {
                fk1min = value;
            }
        }
        public double Fk1max
        {
            get
            {
                return fk1max;
            }
            set
            {
                fk1max = value;
            }
        }
        public double Fk2min
        {
            get
            {
                return fk2min;
            }
            set
            {
                fk2min = value;
            }
        }
        public double Fk2max
        {
            get
            {
                return fk2max;
            }
            set
            {
                fk2max = value;
            }
        }
        public double Fk3min
        {
            get
            {
                return fk3min;
            }
            set
            {
                fk3min = value;
            }
        }
        public double Fk3max
        {
            get
            {
                return fk3max;
            }
            set
            {
                fk3max = value;
            }
        }
        public double Fk4min
        {
            get
            {
                return fk4min;
            }
            set
            {
                fk4min = value;
            }
        }
        public double Fk4max
        {
            get
            {
                return fk4max;
            }
            set
            {
                fk4max = value;
            }
        }

        //Оптимальные параметры вычесленные по другой формуле        
        public double QoptOther
        {
            get
            {
                return qoptother;
            }
            set
            {
                qoptother = value;
            }
        }

        #endregion

        #region КОНСТРУКТОРЫ

        //КОНСТРУКТОРЫ КЛАССА //////////////////////////////////////////////////////////////////////////////////

        public void FAM
        {

        }  //первичный конструктор (пуст)

        #endregion

        #region ФУНКЦИИ
        //ФУНКЦИИ КЛАССА //////////////////////////////////////////////////////////////////////////////////

        public void Calc()
        {

        }  //вычисление оптимальных параметров

        public void OptimumTRUE()
        {
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

            qfinal = Qopt;
            df = dfopt;

            //корректировка параметра DFabs
            DFabsCorrection();  //подпрограмма вычисления абсолютной гетеродинной частоты F2
        }  //программа вычисления оптимальных параметров преобразователя частоты
        public void OptimumFALSE()
        {
            if (dfbandwith)
            {
                if (!idq)
                {
                    //Q<1
                    if (qnom < qopt)
                        df = dfopt * (qnom - cmin) / (qopt - cmin);
                    else
                        df = dfopt * (cmax - qnom) / (cmax - qopt);
                }
                else
                {
                    //Q>1
                    double qinv;
                    qinv = 1 / qfinal;
                    if (qinv < qopt)
                        df = dfopt * (qinv - cmin) / (1 / qopt - cmin);
                    else
                        df = dfopt * (cmax - qinv) / (cmax - 1 / qopt);
                }
            }
            else
                DFabsCorrection();  //подпрограмма вычисления абсолютной гетеродинной частоты F2
        }  //программа вычисления текущих параметров преобразователя частоты
        private void DFabsCorrection()
        {            
            if (signaltype == 1)
                f2 = fabs / qfinal;
            if (signaltype == 2)
                f2 = fabs;
            if (signaltype == 3)
            {
                double kk = 0;
                
                //kk = ((-1) ^ m * ((-1) ^ s)) / 2 - c2 * (-1) ^ s - c1 * (-1) ^ m;
                //kk = ((-1) ^ m * qfinal + (-1) ^ s + df * kk);

                if (kk == 0)
                {
                    MessageBox.Show("Ошибка при вычислении частот, деление на нуль (принимаем F2 = Fa/0.1) !");
                    f2 = fabs / 0.1;
                }
                else
                    f2 = fabs / kk;
                fabs = df / f2;  //корректировка DFabs
            }
        }  //подпрограмма вычисления абсолютной гетеродинной частоты F2
        public void NomogrammaRegion()
        {
            //установка начальных параметров из обеих ветвей вынесена за скобки
            fmin1 = qfinal - c1 * df;
            fmax1 = qfinal + (1 - c1) * df;
            fmin2 = 1 - c2 * df;
            fmax2 = 1 + (1 - c2) * df;

            if (m + s == 4)
            {
                //для суммирования частот                
                if (!idq)
                {
                    qmin = fmin1 / fmax2;
                    qmax = fmax1 / fmin2;                                   
                }
                else
                {
                    qmin = fmin2 / fmax1;
                    qmax = fmax2 / fmin1;
                }
            }
            else
            {
                //для вычитания частот
                if (!idq)
                    if (fmin2 != 0)
                        qmin = fmin1 / fmin2;
                    else
                        qmin = cmin;
                else
                    if (fmin1 != 0)
                        qmin = fmin2 / fmin1;
                    else
                        qmin = cmin;
                if (!idq)
                    qmax = fmax1 / fmax2;
                else
                    qmax = fmax2 / fmax1;
            }
        }  //вычисление параметров области фильтрации на номограмме комбинацинных частот

        #endregion
    }
}