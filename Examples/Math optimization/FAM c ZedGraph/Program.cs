using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FAM
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FAM());
        }
    }

    public static class DataLoad
    {
        //загружает данные в номограмму
        public delegate void DataLoadEvent(int DATA);
        public static DataLoadEvent NomogrammaDataLoad;

        //вызываем обновление входных даных в форме FAM при изменении параметров
        public delegate void RefreshEvent();
        public static RefreshEvent DataRefresh;
    }
}
