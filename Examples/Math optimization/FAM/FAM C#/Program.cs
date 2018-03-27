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
            Application.Run(new FAMForm());
        }
    }

    public static class DataTransfer
    {
        //передача параметров преобразования в Nomogramma
        public delegate void NomogrammaDataUploadEvent(int kp);
        public static NomogrammaDataUploadEvent DataUpLoad;

        //вызываем обновление входных даных в форме FAM при изменении параметров
        public delegate void RefreshEvent();
        public static RefreshEvent DataRefresh;
    }
}
