using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace FAM
{
    public class Nomogramma  //самомтоятельный, не наследующий класс
    {
        #region КОНСТРУКТОРЫ КЛАССА

        //КОНСТРУКТОРЫ КЛАССА //////////////////////////////////////////////////////////////////////////////////        
        public Nomogramma()
        {
            kp = 5;  //порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот
            TN = 1;  //тип нелинейности
            TP = 0;  //тип преобразования  ПОКА НЕ ИСПОЛЬЗУЕТСЯ
            TC = 0;  //тип конвертора
            AllCombin = false;  //учет синтеза всех комбинационных частот в каждой пораженной точке

            //Максимальное количество пораженных точек и 
            //комбинационных частот  при KQ = 120, NX = 240 -- Kp <= 20
            //комбинационных частот  при KQ = 1200, NX = 2400 -- Kp <= (80 - 85) - для неполной номограммы кмбинационных частот

            const int KQ = 1200;
            const int NX = 2400;
            const int MaxLines = 4800;  //максимальное число комбинационных прямых

            fr = new int[KQ];
            fq = new int[KQ];

            cr = new int[KQ];
            cq = new int[KQ];

            br = new int[KQ];
            bq = new int[KQ];

            kmp1 = new int[NX];
            kmm1 = new int[NX];
            kmp2 = new int[NX];
            kmm2 = new int[NX];

            ax1 = new int[NX];
            cx1 = new int[NX];
            ax2 = new int[NX];
            cx2 = new int[NX];

            aline = new int[MaxLines];
            cline = new int[MaxLines];
        }  //конструктор класса NomogrammaData не принимающий параметров, DEFAULT
        public Nomogramma(int KQ, int NX, int MaxLines)
        {
            kp = 5;  //порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот
            TN = 1;  //тип нелинейности
            TP = 0;  //тип преобразования  ПОКА НЕ ИСПОЛЬЗУЕТСЯ
            TC = 0;  //тип конвертора
            AllCombin = false;  //учет синтеза всех комбинационных частот в каждой пораженной точке

            //Максимальное количество пораженных точек и 
            //комбинационных частот  при KQ = 120, NX = 240 -- Kp <= 20
            //комбинационных частот  при KQ = 1200, NX = 2400 -- Kp <= (80 - 85) - для неполной номограммы кмбинационных частот

            fr = new int[KQ];
            fq = new int[KQ];

            cr = new int[KQ];
            cq = new int[KQ];

            br = new int[KQ];
            bq = new int[KQ];

            kmp1 = new int[NX];
            kmm1 = new int[NX];
            kmp2 = new int[NX];
            kmm2 = new int[NX];

            ax1 = new int[NX];
            cx1 = new int[NX];
            ax2 = new int[NX];
            cx2 = new int[NX];

            aline = new int[MaxLines];
            cline = new int[MaxLines];
        }  //конструктор класса NomogrammaData принимающий раздельные параметры массивов
        public Nomogramma(int arraySize)
        {
            kp = 5;  //порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот
            TN = 1;  //тип нелинейности
            TP = 0;  //тип преобразования  ПОКА НЕ ИСПОЛЬЗУЕТСЯ
            TC = 0;  //тип конвертора
            AllCombin = false;  //учет синтеза всех комбинационных частот в каждой пораженной точке
            
            fr = new int[arraySize];
            fq = new int[arraySize];

            cr = new int[arraySize];
            cq = new int[arraySize];

            br = new int[arraySize];
            bq = new int[arraySize];

            kmp1 = new int[arraySize];
            kmm1 = new int[arraySize];
            kmp2 = new int[arraySize];
            kmm2 = new int[arraySize];

            ax1 = new int[arraySize];
            cx1 = new int[arraySize];
            ax2 = new int[arraySize];
            cx2 = new int[arraySize];

            aline = new int[arraySize];
            cline = new int[arraySize];
        }  //конструктор класса NomogrammaData принимающий общий параметр arraySize

        #endregion

        #region ПЕРЕМЕННЫЕ

        //ПЕРЕМЕННЫЕ //////////////////////////////////////////////////////////////////////////////////

        int fn;  //текущее число членов ряда Фарея
        int kp;  //порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот

        int tn;  //тип нелинейности
        int tp;  //тип преобразования
        int tc;  //тип конвертора

        int nc;  //количество соотношений смешиваемых частот, пораженных комбинационными частотами при суммировании
        int nb;  //количество соотношений смешиваемых частот, пораженных комбинационными частотами при вычитании

        int nx1;  //физическое число комбинационных частот при суммировании
        int nx2;  //физическое число комбинационных частот при вычитании

        int nlines;  //общее число комбинационных прямых

        bool allcombin;  //учет синтеза всех комбинационных частот в каждой пораженной точке

        int[] fr;  //массив рядов Фарея (исходный массив пораженных точек), числители
        int[] fq;  //массив рядов Фарея (исходный массив пораженных точек), знаменатели

        int[] cr;  //массив пораженных точек при суммировании, числители
        int[] cq;  //массив пораженных точек при суммировании, знаменатели

        int[] br;  //массив пораженных точек при при вычитании, числители
        int[] bq;  //массив пораженных точек при при вычитании, знаменатели

        int[] kmp1;  //массив хранящий номера уравнений комбинационных частот с положительними наклонами "P"
        int[] kmm1;  //массив хранящий номера уравнений комбинационных частот с отрицательными наклонами "M"
        int[] kmp2;  //массив хранящий номера уравнений комбинационных частот с положительними наклонами "P"
        int[] kmm2;  //массив хранящий номера уравнений комбинационных частот с отрицательными наклонами "M"

        int[] ax1;  //массив коэффициентов "А", уравнений комбинационных частот при суммировании
        int[] cx1;  //массив коэффициентов "С", уравнений комбинационных частот при суммировании
        int[] ax2;  //массив коэффициентов "А", уравнений комбинационных частот при вычитании
        int[] cx2;  //массив коэффициентов "С", уравнений комбинационных частот при вычитании

        int[] aline;  //коэффициенты уравнений комбинационной частоты
        int[] cline;  //свободные члены уравнений комбинационной частоты

        float[,] nmp;  //набор точек характеризующий положение линий комбинационных частотна номограмме

        #endregion

        #region ИНТЕРФЕЙСЫ ПЕРЕМЕННЫХ

        //ИНТЕРФЕЙСЫ ПЕРЕМЕННЫХ //////////////////////////////////////////////////////////////////////////////////

        public int FN
        {
            get
            {
                return fn;
            }
        }  //текущее число членов ряда Фарея
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
        }  //порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот

        public int TN
        {
            get
            {
                return tn;
            }
            set
            {
                tn = value;
            }
        }  //тип нелинейности
        public int TP
        {
            get
            {
                return tp;
            }
            set
            {
                tp = value;
            }
        }  //тип преобразования
        public int TC
        {
            get
            {
                return tc;
            }
            set
            {
                tc = value;
            }
        }  //тип конвертора

        public int NC
        {
            get
            {
                return nc;
            }
        }  //количество соотношений смешиваемых частот, пораженных комбинационными частотами при суммировании
        public int NB
        {
            get
            {
                return nb;
            }
        }  //количество соотношений смешиваемых частот, пораженных комбинационными частотами при вычитании

        public int NLines
        {
            get
            {
                return nlines;
            }
        }  //общее число комбинационных прямых

        public bool AllCombin
        {
            get
            {
                return allcombin;
            }
            set
            {
                allcombin = value;
            }
        }  //учет синтеза всех комбинационных частот в каждой пораженной точке

        public int NX1
        {
            get
            {
                return nx1;
            }
        }  //физическое число комбинационных частот при суммировании
        public int NX2
        {
            get
            {
                return nx2;
            }
        }  //физическое число комбинационных частот при вычитании

        public int[] FR
        {
            get
            {
                return fr;
            }
        }  //массив рядов Фарея (исходный массив пораженных точек), числители
        public int[] FQ
        {
            get
            {
                return fq;
            }
        }  //массив рядов Фарея (исходный массив пораженных точек), знаменатели

        public int[] CR
        {
            get
            {
                return cr;
            }
        }  //массив пораженных точек при суммировании, числители
        public int[] CQ
        {
            get
            {
                return cq;
            }
        }  //массив пораженных точек при суммировании, знаменатели

        public int[] BR
        {
            get
            {
                return br;
            }
        }  //массив пораженных точек при при вычитании, числители
        public int[] BQ
        {
            get
            {
                return bq;
            }
        }  //массив пораженных точек при при вычитании, знаменатели

        public int[] KMP1
        {
            get
            {
                return kmp1;
            }
        }  //массив хранящий номера уравнений комбинационных частот с положительними наклонами "P"
        public int[] KMM1
        {
            get
            {
                return kmm1;
            }
        }  //массив хранящий номера уравнений комбинационных частот с отрицательными наклонами "M"
        public int[] KMP2
        {
            get
            {
                return kmp2;
            }
        }  //массив хранящий номера уравнений комбинационных частот с положительними наклонами "P"
        public int[] KMM2
        {
            get
            {
                return kmm2;
            }
        }  //массив хранящий номера уравнений комбинационных частот с отрицательными наклонами "M"

        public int[] AX1
        {
            get
            {
                return ax1;
            }
        }  //массив коэффициентов "А", уравнений комбинационных частот при суммировании
        public int[] CX1
        {
            get
            {
                return cx1;
            }
        }  //массив коэффициентов "С", уравнений комбинационных частот при суммировании
        public int[] AX2
        {
            get
            {
                return ax2;
            }
        }  //массив коэффициентов "А", уравнений комбинационных частот при вычитании
        public int[] CX2
        {
            get
            {
                return cx2;
            }
        }  //массив коэффициентов "С", уравнений комбинационных частот при вычитании

        public int[] ALine
        {
            get
            {
                return aline;
            }
        }  //коэффициенты уравнений комбинационной частоты
        public int[] CLine
        {
            get
            {
                return cline;
            }
        }  //свободные члены уравнений комбинационной частоты

        public float[,] NMP
        {
            get
            {
                return nmp;
            }
        }  //(Nomogramma Math Points)набор точек характеризующий положение линий комбинационных частотна номограмме

        #endregion

        #region ФУНКЦИИ КЛАССА

        //ФУНКЦИИ КЛАССА //////////////////////////////////////////////////////////////////////////////////
        public void CulcNomogramma(bool DrawLogs)
        {
            FareySeries(0);
            if (DrawLogs)
            {
                FareySeriesLog("clear");
                FareySeriesLog("1");
            }

            SintezPorT();
            if (DrawLogs)
            {
                SintezPorTLog("clear");
                SintezPorTLog("1");
            }

            CalcPorLines();
            if (DrawLogs)
            {
                CalcPorLinesLog("clear");
                CalcPorLinesLog("1");
            }
            
            SintezLines();
            if (DrawLogs)
            {                
                SintezLinesLog("clear");
                SintezLinesLog("1");
            }

            CulcPoints();
            if (DrawLogs)
            {                
                CulcPointsLog("clear");
                CulcPointsLog("1");
            }

            if (DrawLogs)
                NomogrammaDataGlobalLog();  //GlobalLog
        }  //расчет всей номограммы комбинационных частот

        private void FareySeries(int kpCorrection)
        {
            /*
            Подпрограмма FareySeries предназначена для синтеза последовательности
            дробей Фарея индекса Кр, используя классический последовательный алгоритм 
            добавления медиант соседних дробей.

            ВХОДНЫЕ ДАННЫЕ:
               Кр - индекс синтезируемого ряда Фарея
            ВЫХОДНЫЕ ДАННЫЕ:
               FR(I) - числители ряда Фарея
               FQ(I) - знаменатели ряда Фарея
               FN - текущее число членов ряда Фарея
            */
            int k;
            int i;
            int j;
            int N;
            int jk;

            //инициализация дроби Фарея индекса Кр = 1
            fn = 2;
            fr[1] = 0;
            fq[1] = 1;
            fr[2] = 1;
            fq[2] = 1;
            N = fn;

            //синтез следующих рядов Фарея
            for (k = 2; k <= kp + kpCorrection; k++)
            {
                i = 1;
                do
                {
                    i++;
                    if ((fq[i - 1] + fq[i]) <= k)
                    {
                        //вставить новый член ряда Фарея в последовательность
                        N++;  //увеличить число членов ряда Фарея на 1
                        jk = i + 1;
                        j = N;
                        //сместить остаток ряда на 1 член выше
                        do
                        {
                            fr[j] = fr[j - 1];
                            fq[j] = fq[j - 1];
                            j--;
                        } while (j >= jk);
                        //вставить новый член ряда Фарея
                        fr[i] = fr[i - 1] + fr[i + 1];
                        fq[i] = fq[i - 1] + fq[i + 1];
                        i++;
                    }
                } while (i != N);
            }
            fn = N;
        }  //вычисление последовательности ряда фарея методом полного перебора его членов        
        #region SintezPorT

        int k = 1;  //коэффициент нескольких комбинационных частот проходящих через праженную точку, k = 1 - указывает что необходимо выполнить проверку только основных комбинационных частот            
        int N;  //предел перебора пораженных точек            
            
        bool YesComb;  //указатель на прохождение через точку комбинационных частот
        int MPlus;  //коэффициент комбинацинной частоты с положительной производной            
        int NPlus;  //свободный член комбинационной частоты            
        int MMinus;  //коэфициент комбинационной частоты с отрицательной производной            
        int NMinus;  //свободный член комбинационной частоты            
        bool YesPlus;  //проходит ли через точку комбинационная чстота с положительной производной           
        bool YesMinus;  //проходит ли через точку комбинационная частота с отрицательной производной'
        
        private void SintezPorT()
        {
            if (tn == 1)
            {
                SintezPorTHalf();  //неполная номограмма комбинационных частот
            }
            else
            {
                SintezPorTFull();  //полная номограмма комбинационных частот
            }
        }  //синтез пораженных точек номограммы комбинационных частот
        
        private void SintezPorTHalf()
        {
            //неполная номограмма комбинационных частот
            //синтез последовательности пораженных точек для суммирования частот
            SintezPorTHulf_forSum();

            //сохранение пораженных точек в соответствующих массивах
            nc = fn;
            for (int i = 1; i <= nc; i++)
            {
                cr[i] = fr[i];
                cq[i] = fq[i];
            }


            //неполная номограмма комбинационных частот
            //синтез последовательности пораженных точек для вычитания частот
            SintezPorTHulf_forSub();

            //сохранение пораженных точек в соответствующих массивах
            nb = fn;
            for (int i = 1; i <= nb; i++)
            {
                br[i] = fr[i];
                bq[i] = fq[i];
            }
        }  //неполная номограмма комбинационных частот
        private void SintezPorTHulf_forSum()
        {
            //неполная номограмма комбинационных частот
            //синтез последовательности пораженных точек для суммирования частот            
            FareySeries(0);
            int i = 2;
            N = fn;
            do
            {
                do
                {
                    YesComb = false;
                    if (fr[i] + fq[i] < kp)
                        YesComb = true;
                    //дополнительная проверка на вычеркивание по остальным параметрам
                    if (YesComb)
                    {
                        //для каждой пораженной точки
                        //вычисление коэффициентов комбинационных частот с положительной производной
                        MPlus = k * fq[i] + 1;
                        NPlus = -k * fr[i] + 1;
                        //вычисление коэффициентов комбинационных частот с отрицательной производной
                        MMinus = -k * fq[i] + 1;
                        NMinus = k * fr[i] + 1;
                        //проверяем, удовлетворяет ли данная комбинационная частота входным условиям
                        YesPlus = false;
                        YesMinus = false;

                        //синтезируется неполная номограмма комбинационных частот
                        CondictionOfHalfSintesis();

                        if (YesPlus || YesMinus)
                            YesComb = true;
                        else
                            YesComb = false;
                    }
                    if (YesComb)
                        break;

                    N--;

                    for (int j = i; j <= N; j++)
                    {
                        fr[j] = fr[j + 1];
                        fq[j] = fq[j + 1];
                    }
                } while (i < N);
                i++;
            } while (i < N);
            fn = N;
        }  //процедура вычеркивания пораженных точек для NC (при суммировании)
        private void SintezPorTHulf_forSub()
        {
            FareySeries(1);
            int i = 1;
            N = fn;

            do
            {
                do
                {
                    YesComb = false;
                    if (fr[i] + fq[i] <= kp + 1)
                        YesComb = true;
                    //вычеркивание членов ряда Фарея
                    //при вычеркивании вычеркиваются R + Q > Kp + 1
                    //дополнительная проверка на вычеркивание по остальным параметрам
                    if (YesComb)
                    {
                        //для каждой пораженной точки
                        //вычисление коэффициентов комбинационных частот с положительной производной
                        MPlus = k * fq[i] - 1;
                        NPlus = -k * fr[i] + 1;
                        //вычисление коэффициентов комбинационных частот с отрицательной производной
                        MMinus = -k * fq[i] - 1;
                        NMinus = k * fr[i] + 1;
                        //проверяем, удовлетворяет ли данная комбинационная частота входным условиям
                        YesPlus = false;
                        YesMinus = false;

                        //синтезируется неполная номограмма комбинационных частот
                        CondictionOfHalfSintesis();

                        if (YesPlus || YesMinus)
                            YesComb = true;
                        else
                            YesComb = false;
                    }
                    //вычеркивание членов ряда фарея
                    //при суммировании вычеркиваются R + Q >= Kp                        
                    if (YesComb)
                        break;

                    N--;

                    for (int j = i; j <= N; j++)
                    {
                        fr[j] = fr[j + 1];
                        fq[j] = fq[j + 1];
                    }
                } while (i < N);
                i++;
            } while (i < N);
            fn = N;
        }  //процедура вычеркивания пораженных точек по другим условиям для NB (при вычитании)
        private void CondictionOfHalfSintesis()
        {
            //синтезируется неполная номограмма комбинационных частот
            switch (tc)
            {
                case 0:  //простой преобразователь
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        YesMinus = true;
                    break;
                case 1:  //балансный преобразователь по гетеродинному входу
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(NPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(NMinus) % 2 > 0)
                            YesMinus = true;
                    break;
                case 2:  //балансный преобразователь по сигнальному входу
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(MPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(MMinus) % 2 > 0)
                            YesMinus = true;
                    break;
                case 3:  //двойной балансный преобразователь
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(MPlus) % 2 > 0 && Math.Abs(NPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(MMinus) % 2 > 0 && Math.Abs(NMinus) % 2 > 0)
                            YesMinus = true;
                    break;
            }
        }  //условия синтеза неполной номограммы комбинационных частот

        private void SintezPorTFull()
        {
            //полная номограмма комбинационных частот
            //синтез последовательности пораженных точек для суммирования частот
            FareySeries(0);
            //сохранение пораженных точек в соответствующих массивах
            nc = fn;
            for (int i = 1; i <= nc; i++)
            {
                cr[i] = fr[i];
                cq[i] = fq[i];
            }
            nc--;
            cr[nc] = cr[nc + 1];
            cq[nc] = cq[nc + 1];
            SintezPorTFullOther_forSum();


            //полная номограмма комбинационных частот
            //синтез последовательности пораженных точек для вычитания частот
            FareySeries(0);
            //сохранение пораженных точек в соответствующих массивах
            nb = fn;
            for (int i = 1; i <= nb; i++)
            {
                br[i] = fr[i];
                bq[i] = fq[i];
            }
            SintezPorTFullOther_forSub();
        }  //полная номограмма комбинационных частот
        private void SintezPorTFullOther_forSum()
        {
            //процедура вычеркивания пораженных точек по другим условиям
            N = nc;
            int i = 1;
            do
            {
                do
                {
                    YesComb = false;
                    //дополнительная проверка на вычеркивание по остальным параметрам
                    //для каждой пораженной точки
                    //вычисление коэффициентов комбинационных частот с положительной производной
                    MPlus = k * cq[i] + 1;
                    NPlus = -k * cr[i] + 1;
                    //вычисление коэффициентов комбинационных частот с отрицательной производной
                    MMinus = -k * cq[i] + 1;
                    NMinus = k * cr[i] + 1;
                    //проверяем, удовлетворяет ли данная комбинационная частота входным условиям
                    YesPlus = false;
                    YesMinus = false;

                    //синтезируется полная номограмма комбинационных частот
                    CondictionOfFullSintesis();

                    if (YesPlus || YesMinus)
                        YesComb = true;
                    else
                        YesComb = false;

                    //вычеркивание членов ряда Фарея
                    //при суммировании вычеркиваются R + Q >= Kp
                    if (YesComb)
                        break;

                    N--;

                    for (int j = i; j <= N; j++)
                    {
                        cr[j] = cr[j + 1];
                        cq[j] = cq[j + 1];
                    }
                } while (i < N);
                i++;
            } while (i < N);
            nc = N;
        }  //процедура вычеркивания пораженных точек по другим условиям для NC (при суммировании)
        private void SintezPorTFullOther_forSub()
        {
            //процедура вычеркивания пораженных точек по другим условиям               
            N = nb;
            int i = 1;
            do
            {
                do
                {
                    YesComb = false;
                    //дополнительная проверка на вычеркивание по остальным параметрам
                    //для каждой пораженной точки
                    //вычисление коэффициентов комбинационных частот с положительной производной
                    MPlus = k * bq[i] - 1;
                    NPlus = -k * br[i] + 1;
                    //вычисление коэффициентов комбинационных частот с отрицательной производной
                    MMinus = -k * bq[i] - 1;
                    NMinus = k * br[i] + 1;
                    //проверяем, удовлетворяет ли данная комбинационная частота входным условиям
                    YesPlus = false;
                    YesMinus = false;

                    //синтезируется полная номограмма комбинационных частот
                    CondictionOfFullSintesis();

                    if (YesPlus || YesMinus)
                        YesComb = true;
                    else
                        YesComb = false;
                    //вычеркивание членов ряда Фарея
                    //при вычитании вычеркиваются R + Q >= Kp
                    if (YesComb)
                        break;

                    N--;

                    for (int j = i; j <= N; j++)
                    {
                        br[j] = br[j + 1];
                        bq[j] = bq[j + 1];
                    }
                } while (i < N);
                i++;
            } while (i < N);
            nb = N;
        }  //процедура вычеркивания пораженных точек по другим условиям для NB (при вычитании)
        private void CondictionOfFullSintesis()
        {
            //синтезируется полная номограмма комбинационных частот
            switch (tc)
            {
                case 0:  //простой преобразователь
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        YesMinus = true;
                    break;
                case 1:  //балансный преобразователь по гетеродинному входу
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(NPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(NMinus) % 2 > 0)
                            YesMinus = true;
                    break;
                case 2:  //балансный преобразователь по сигнальному входу
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(MPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(MMinus) % 2 > 0)
                            YesMinus = true;
                    break;
                case 3:  //двойной балансный преобразователь
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        if (Math.Abs(MPlus) % 2 > 0 && Math.Abs(NPlus) % 2 > 0)
                            YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        if (Math.Abs(MMinus) % 2 > 0 && Math.Abs(NMinus) % 2 > 0)
                            YesMinus = true;
                    break;
            }
        }  //условия синтеза полной номограммы комбинационных частот
        
        #endregion
        private void CalcPorLines()
        {
            int i;
            int k;  //номер комбинационной частоты проходящей через анализируемую пораженную точку

            int MPlus;  //коффициент комбинационной частоты с положительной производной
            int NPlus;  //свободный член комбинационной частоты с положительной производной
            int MMinus;  //коффициент комбинационной частоты с отрицательной производной
            int NMinus;  //свободный член комбинационной частоты с отрицательной производной
            bool YesPlus;  //проходит ли через точку комбинационная чстота с положительной производной
            bool YesMinus;  //проходит ли через точку комбинационная частота с отрицательной производной

            //обнуление промежуточных массивов
            k = 1;  //анализируются только пораженые точки с минимальными порядками комбнационных частот

            for (i = 1; i <= nc; i++)
            {
                kmp1[i] = 0;
                kmm1[i] = 0;
            }
            for (i = 1; i <= nb; i++)
            {
                kmp2[i] = 0;
                kmm2[i] = 0;
            }

            nx1 = 0;
            nx2 = 0;

            //анализ каждой пораженной точки для суммирования частот
            for (i = 1; i <= nc; i++)
            {
                //для каждой пораженной точки
                //вычисление коэффициентов комбинационных частот с положительной производной
                MPlus = k * cq[i] + 1;
                NPlus = -k * cr[i] + 1;
                //вычисление коэффициентов комбинационных частот с отрицательной производной  
                MMinus = -k * cq[i] + 1;
                NMinus = k * cr[i] + 1;
                //проверка, удовлетворяет ли данная комбинационная частота входным условиям
                YesPlus = false;
                YesMinus = false;
                if (tn == 0)
                {
                    //синтезируется полная номограмма комбинационных частот
                    if (Math.Abs(MPlus) < Kp && Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) < Kp && Math.Abs(NMinus) < kp)
                        YesMinus = true;
                }
                else
                {
                    //синтезируется неполная номограмма комбинационных частот
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        YesMinus = true;
                }

                //сохранение коэффициентов комбинационных частот в массивах
                if (YesPlus)
                {
                    nx1++;
                    kmp1[i] = nx1;
                    ax1[nx1] = MPlus;
                    cx1[nx1] = NPlus;
                }
                if (YesMinus)
                {
                    nx1++;
                    kmm1[i] = nx1;
                    ax1[nx1] = MMinus;
                    cx1[nx1] = NMinus;
                }
            }

            //анализ каждой пораженной точки для вычитания частот
            for (i = 1; i <= nb; i++)
            {
                //для каждой пораженной точки
                //вычисление коэффициентов комбинационных частот с положительной производной
                MPlus = k * bq[i] - 1;
                NPlus = -k * br[i] + 1;
                //вычисление коэффициентов комбинационных частот с отрицательной производной  
                MMinus = -k * bq[i] - 1;
                NMinus = k * br[i] + 1;

                //проверка, удовлетворяет ли данная комбинационная частота входным условиям
                YesPlus = false;
                YesMinus = false;

                if (tn == 0)
                {
                    //синтезируется полная номограмма комбинационных частот
                    if (Math.Abs(MPlus) < Kp && Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) < Kp && Math.Abs(NMinus) < kp)
                        YesMinus = true;
                }
                else
                {
                    //синтезируется неполная номограмма комбинационных частот
                    if (Math.Abs(MPlus) + Math.Abs(NPlus) < kp)
                        YesPlus = true;
                    if (Math.Abs(MMinus) + Math.Abs(NMinus) < kp)
                        YesMinus = true;
                }

                //сохранение коэффициентов комбинационных частот в массивах
                if (YesPlus)
                {
                    nx2++;
                    kmp2[i] = nx2;
                    ax2[nx2] = MPlus;
                    cx2[nx2] = NPlus;
                }
                if (YesMinus)
                {
                    nx2++;
                    kmm2[i] = nx2;
                    ax2[nx2] = MMinus;
                    cx2[nx2] = NMinus;
                }
            }
        }  //синтез линий номограммы проходящих через пораженные точки
        private void SintezLines()
        {
            //программа синтеза прямых образующих номограмму
            int KS1;
            int KS2;
            int i;
            int j;
            int k;
            int A;
            int C;

            //приведение всех комбинационных прямых в один массив
            nlines = 0;
            for (i = 1; i <= nc; i++)
            {
                KS1 = kmp1[i];
                KS2 = kmm1[i];
                if (KS1 > 0)
                {
                    nlines++;
                    aline[nlines] = ax1[KS1];
                    cline[nlines] = cx1[KS1];
                }
                if (KS2 > 0)
                {
                    nlines++;
                    aline[nlines] = ax1[KS2];
                    cline[nlines] = cx1[KS2];
                }
            }
            for (i = 1; i <= nb; i++)
            {
                KS1 = kmp2[i];
                KS2 = kmm2[i];
                if (KS1 > 0)
                {
                    nlines++;
                    aline[nlines] = ax2[KS1];
                    cline[nlines] = cx2[KS1];
                }
                if (KS2 > 0)
                {
                    nlines++;
                    aline[nlines] = ax2[KS2];
                    cline[nlines] = cx2[KS2];
                }
            }

            //синтез дополнительных комбинационных прямых проходящих через пораженные точки с порядком меньшим Кр
            if (allcombin)
                SintezRestCombin();

            //исключение избыточности массива комбинационных частот
            for (i = 1; i <= nlines - 1; i++)
            {
                A = aline[i];
                C = cline[i];
                for (j = i + 1; j <= nlines; j++)
                {
                    if (A == aline[j] && C == cline[j])
                    {
                        //обнаружены две одинаковые комбинационные прямые
                        for (k = j + 1; k <= nlines; k++)
                        {
                            aline[k - 1] = aline[k];
                            cline[k - 1] = cline[k];
                        }
                        nlines--;
                    }
                }
            }
        }  //программа синтеза прямых образующих номограмму
        private void SintezRestCombin()
        {
            //синтез дополнительных комбинационных прямых проходящих через пораженные точки с порядком меньшим Кр
        }  //МЕТОД ПОКА ПУСТ
        private void CulcPoints()
        {
            float Xn;
            float Xk;
            float Yn;
            float Yk;

            nmp = new float[nlines + 1, 4];

            for (int i = 1; i <= nlines; i++)
            {
                Xn = 0F;
                Xk = 1F;

                Yn = aline[i] * Xn + cline[i];
                Yk = aline[i] * Xk + cline[i];

                if (Yn < 0F)
                {
                    Yn = 0F;
                    Xn = (float)-cline[i] / (float)aline[i];
                }
                if (Yn > 2F)
                {
                    Yn = 2F;
                    Xn = (2F - (float)cline[i]) / (float)aline[i];
                }
                if (Yk < 0F)
                {
                    Yk = 0F;
                    Xk = (float)-cline[i] / (float)aline[i];
                }
                if (Yk > 2F)
                {
                    Yk = 2F;
                    Xk = (2F - (float)cline[i]) / (float)aline[i];
                }

                nmp[i, 0] = Xn;
                nmp[i, 1] = Yn;
                nmp[i, 2] = Xk;
                nmp[i, 3] = Yk;
            }
        }
        #endregion

        #region ЛОГИРОВАНИЕ КЛАССА
        //ЛОГИРОВАНИЕ КЛАССА //////////////////////////////////////////////////////////////////////////////////
        private void NomogrammaDataGlobalLog()
        {
            string str = "*** Синтез номограммы комбинационных частот, параметры модуля FAMData. *** \r\n\r\n";
            str += "Текущее число членов ряда Фарея. \r\n    FN: " + fn + "\r\n";
            str += "Порядок преобразования, индекс синтезируемого ряда Фарея, допустимый порядок комбинационных частот. \r\n    KP: " + kp + "\r\n" + "\r\n";

            str += "Тип нелинейности. \r\n   TN: " + tn + "\r\n";
            str += "Тип преобразования. \r\n   TP: " + tp + "\r\n";
            str += "Тип конвертора. \r\n   TC: " + tc + "\r\n" + "\r\n";

            str += "Количество соотношений смешиваемых частот, пораженных комбинационными частотами при суммировании. \r\n   NC: " + nc + "\r\n";
            str += "Количество соотношений смешиваемых частот, пораженных комбинационными частотами при вычитании. \r\n   NB: " + nb + "\r\n" + "\r\n";

            str += "Физическое число комбинационных частот при суммировании. \r\n   NX1: " + nx1 + "\r\n";
            str += "Физическое число комбинационных частот при вычитании. \r\n   NX2: " + nx2 + "\r\n" + "\r\n";

            str += "Общее число комбинационных прямых. \r\n   NLines: " + nlines + "\r\n" + "\r\n";

            str += "Учет синтеза всех комбинационных частот в каждой пораженной точке. \r\n   AllConbin: " + allcombin + "\r\n";

            Functions z = new Functions();
            int arraySize = z.EulerSum(kp) + 2;  //определение количества елементов массива рассчитывается с применением метода Эйлера

            str += "\r\n\r\n\r\n" + "Массив рядов Фарея (исходный массив пораженных точек), числители. \r\n\r\n   FR:  \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "FR[" + i + "]: " + fr[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив рядов Фарея (исходный массив пораженных точек), знаменатели.\r\n\r\n   FQ: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "FQ[" + i + "]: " + fq[i] + "\r\n";
            

            str += "\r\n\r\n\r\n" + "Массив пораженных точек при суммировании, числители.\r\n\r\n   CR: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "CR[" + i + "]: " + cr[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив пораженных точек при суммировании, знаменатели.\r\n\r\n   CQ: \r\n";
            for (int i = 1; i <= arraySize; i++)            
                str += "CQ[" + i + "]: " + cq[i] + "\r\n";

            str += "\r\n\r\n\r\n" + "Массив пораженных точек при при вычитании, числители.\r\n\r\n   BR: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "BR[" + i + "]: " + br[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив пораженных точек при при вычитании, знаменатели.\r\n\r\n   BQ: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "BQ[" + i + "]: " + bq[i] + "\r\n";            

            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с положительними наклонами.\r\n\r\n   KMP1: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "KMP1[" + i + "]: " + kmp1[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с отрицательными наклонами.\r\n\r\n   KMM1: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "KMP1[" + i + "]: " + kmm1[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с положительними наклонами.\r\n\r\n   KMP2: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "KMP2[" + i + "]: " + kmp2[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с отрицательными наклонами.\r\n\r\n   KMM2: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "KMM2[" + i + "]: " + kmm2[i] + "\r\n";
            

            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с положительними наклонами.\r\n\r\n   AX1: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "AX1[" + i + "]: " + ax1[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с отрицательными наклонами.\r\n\r\n   CX1: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "CX1[" + i + "]: " + cx1[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с положительними наклонами.\r\n\r\n   AX2: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "AX2[" + i + "]: " + ax2[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с отрицательными наклонами.\r\n\r\n   CX2: \r\n";
            for (int i = 1; i <= arraySize; i++)            
                str += "CX2[" + i + "]: " + cx2[i] + "\r\n";
            

            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с положительними наклонами.\r\n\r\n   ALine: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "ALine[" + i + "]: " + aline[i] + "\r\n";
            
            str += "\r\n\r\n\r\n" + "Массив хранящий номера уравнений комбинационных частот с отрицательными наклонами.\r\n\r\n   CLine: \r\n";
            for (int i = 1; i <= arraySize; i++)
                str += "CLine[" + i + "]: " + cline[i] + "\r\n";

            str += "\r\n\r\n\r\n" + "Массив хранящий математические точки для построения линий номограммы.\r\n\r\n";
            for (int i = 1; i <= nlines; i++)
                str += "Line: " + i + "\r\n" + "  Mn:(" + nmp[i, 0] + ";" + nmp[i, 1] + ")" + " Mk:(" + nmp[i, 2] + ";" + nmp[i, 3] + ")\r\n\r\n";
                        

            str += "\r\n\r\n      ************************(NEXT)*************************\r\n\r\n";

            using (FileStream stream = new FileStream("NomogrammaDataLog.txt", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(str);
                writer.Close();
            }
        }  //масштабный лог всего класса
        private void FareySeriesLog(string LogNumber_or_Clear)
        {
            switch (LogNumber_or_Clear)
            {
                case "clear":
                    using (FileStream stream = new FileStream("FareySeriesLog.txt", FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Close();
                    }
                    break;

                default:
                    string str = "Log №: " + LogNumber_or_Clear + "\r\n";
                    str += "Входные данные подпрограмма FareySries" + "\r\n";
                    str += "Ряд фарея индекса Кр = " + kp + "\r\n";
                    str += "Общее количество членов ряда фарея Fn = " + fn + "\r\n";

                    for (int i = 1; i <= fn; i++)                    
                        str += "R(" + i + ")/Q(" + i + ") = " + fr[i] + " / " + fq[i] + " = " + (float)fr[i] / fq[i] + "\r\n";
                    
                    str += "\r\n";
                    using (FileStream stream = new FileStream("FareySeriesLog.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }
        }  //лог подпрограммы FareySeries
        private void SintezPorTLog(string LogNumber_or_Clear)
        {
            switch (LogNumber_or_Clear)
            {
                case "clear":
                    using (FileStream stream = new FileStream("SintezPorTLog.txt", FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Close();
                    }
                    break;

                default:
                    string str = "Log №: " + LogNumber_or_Clear + "\r\n";
                    str += "Входные данные подпрограмма SintezPorT" + "\r\n";
                    str += "Номограмма комбинационных частот для Кр = " + kp + "\r\n";

                    if (tn == 0)
                        str += "Синтезируется полная номограмма комбинаионных частот";
                    else
                        str += "Синтезируется неполная номограмма комбинаионных частот ";
                    if (tc == 0)
                        str += "простого преобразователя." + "\r\n\r\n";
                    if (tc == 1)
                        str += "с учетом балансности по гетеродинному входу." + "\r\n\r\n";
                    if (tc == 2)
                        str += "с учетом балансности по сигнальному входу." + "\r\n\r\n";
                    if (tc == 3)
                        str += "для кольцевого балансного преобразователя." + "\r\n\r\n";

                    str += "Суммирование частот" + "\r\n";
                    for (int i = 1; i <= nc; i++)                
                        str += " I = " + i + "  " + cr[i] + " / " + cq[i] + " Q = " + (float)cr[i] / cq[i] + "\r\n";
                   
                    str += "\r\n";

                    str += "Вычитание частот" + "\r\n";
                    for (int i = 1; i <= nb; i++)                    
                        str += " I = " + i + "  " + br[i] + " / " + bq[i] + " Q = " + (float)br[i] / bq[i] + "\r\n";
                    
                    str += "\r\n";
                    using (FileStream stream = new FileStream("SintezPorTLog.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }
        }  //лог подпрограммы SintezPorT
        private void CalcPorLinesLog(string LogNumber_or_Clear)
        {
            switch (LogNumber_or_Clear)
            {
                case "clear":
                    using (FileStream stream = new FileStream("CalcPorLinesLog.txt", FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Close();
                    }
                    break;

                default:
                    string str = "Log №: " + LogNumber_or_Clear + "\r\n";
                    str += "Входные данные подпрограмма CalcPorLines" + "\r\n";
                    str += "Номограмма комбинационных частот для Кр = " + kp + "\r\n";

                    if (tn == 0)
                        str += "Синтезируется полная номограмма комбинаионных частот";
                    else
                        str += "Синтезируется неполная номограмма комбинаионных частот ";
                    if (tc == 0)
                        str += "простого преобразователя." + "\r\n\r\n";
                    if (tc == 1)
                        str += "с учетом балансности по гетеродинному входу." + "\r\n\r\n";
                    if (tc == 2)
                        str += "с учетом балансности по сигнальному входу." + "\r\n\r\n";
                    if (tc == 3)
                        str += "для кольцевого балансного преобразователя." + "\r\n\r\n";

                    str += "Суммирование частот" + "\r\n";
                    str += "Общее количество комбинационных частот NX1 = " + nx1 + "\r\n";
                    for (int i = 1; i <= nc; i++)
                    {
                        int KS1 = kmp1[i];
                        int KS2 = kmm1[i];
                        int RP1 = Math.Abs(ax1[KS1]) + Math.Abs(cx1[KS1]);
                        int RP2 = Math.Abs(ax1[KS2]) + Math.Abs(cx1[KS2]);

                        str += " I = " + i + "  " + cr[i] + " / " + cq[i] + " Q = " + (float)cr[i] / cq[i] + "\r\n";
                        if (KS1 > 0)
                            str += "     +   m = " + ax1[KS1] + "  n = " + cx1[KS1] + "\r\n";
                        else
                            str += "     +   m = " + "---" + "  n = " + "---" + "\r\n";
                        if (KS2 > 0)
                            str += "     -   m = " + ax1[KS2] + "  n = " + cx1[KS2] + "\r\n";
                        else
                            str += "     -   m = " + "---" + "  n = " + "---" + "\r\n";
                    }

                    str += "\r\n";

                    str += "Вычитание частот" + "\r\n";
                    str += "Общее количество комбинационных частот NX2 = " + nx2 + "\r\n";
                    for (int i = 1; i <= nb; i++)
                    {
                        int KS1 = kmp2[i];
                        int KS2 = kmm2[i];
                        int RP1 = Math.Abs(ax2[KS1]) + Math.Abs(cx2[KS1]);
                        int RP2 = Math.Abs(ax2[KS2]) + Math.Abs(cx2[KS2]);

                        str += " I = " + i + "  " + br[i] + " / " + bq[i] + " Q = " + (float)br[i] / bq[i] + "\r\n";
                        if (KS1 > 0)
                            str += "     +   m = " + ax2[KS1] + "  n = " + cx2[KS1] + "\r\n";
                        else
                            str += "     +   m = " + "---" + "  n = " + "---" + "\r\n";
                        if (KS2 > 0)
                            str += "     -   m = " + ax2[KS2] + "  n = " + cx2[KS2] + "\r\n";
                        else
                            str += "     -   m = " + "---" + "  n = " + "---" + "\r\n";
                    }

                    str += "\r\n";
                    using (FileStream stream = new FileStream("CalcPorLinesLog.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }
        }  //лог подпрограммы CalcPorLines
        private void SintezLinesLog(string LogNumber_or_Clear)
        {
            switch (LogNumber_or_Clear)
            {
                case "clear":
                    using (FileStream stream = new FileStream("SintezLinesLog.txt", FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Close();
                    }
                    break;

                default:
                    string str = "Log №: " + LogNumber_or_Clear + "\r\n";
                    str += "Входные данные подпрограмма SintezLines" + "\r\n";
                    str += "Номограмма комбинационных частот для Кр = " + kp + "\r\n";

                    if (tn == 0)
                        str += "Синтезирована полная номограмма комбинаионных частот";
                    else
                        str += "Синтезирована неполная номограмма комбинаионных частот ";
                    if (tc == 0)
                        str += "простого преобразователя." + "\r\n\r\n";
                    if (tc == 1)
                        str += "с учетом балансности по гетеродинному входу." + "\r\n\r\n";
                    if (tc == 2)
                        str += "с учетом балансности по сигнальному входу." + "\r\n\r\n";
                    if (tc == 3)
                        str += "для кольцевого балансного преобразователя." + "\r\n\r\n";

                    for (int i = 1; i <= nlines; i++)                    
                        str += "I = " + i + "\r\n" + "  A = " + aline[i] + ", " + "C = " + cline[i] + "\r\n";                    

                    str += "\r\n";
                    using (FileStream stream = new FileStream("SintezLinesLog.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }
        }  //лог подпрограммы SintezLines
        private void CulcPointsLog(string LogNumber_or_Clear)
        {
            switch (LogNumber_or_Clear)
            {
                case "clear":
                    using (FileStream stream = new FileStream("CulcPointsLog.txt", FileMode.Create))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Close();
                    }
                    break;

                default:
                    string str = "Log №: " + LogNumber_or_Clear + "\r\n";
                    str += "Входные данные подпрограмма CulcPoints" + "\r\n";
                    str += "Номограмма комбинационных частот для Кр = " + kp + "\r\n";

                    for (int i = 1; i <= nlines; i++)
                        str += "Line: " + i + "\r\n" + "  Mn:(" + nmp[i, 0] + ";" + nmp[i, 1] + ")" + " Mk:(" + nmp[i, 2] + ";" + nmp[i, 3] + ")\r\n";

                    str += "\r\n";
                    using (FileStream stream = new FileStream("CulcPointsLog.txt", FileMode.Append))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(str);
                        writer.Close();
                    }
                    break;
            }


        }
        #endregion
    }
}
