using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAM
{
    public class FAMData  //самомтоятельный, не наследующий класс
    {
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
        
        //Рабочая частота
        double f1;
        
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
        
        

        //КОНСТРУКТОРЫ КЛАССА //////////////////////////////////////////////////////////////////////////////////


    }
}