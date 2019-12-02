using System;
using System.Threading;

namespace CodeForces.Units.Semafore.Models
{
    public class Reader
    {
        // создаем семафор
        private static readonly Semaphore _semaphore = new Semaphore(3, 3);
        private Thread _myThread;
        private Int32 _count = 3;// счетчик чтения


        public Reader(Int32 i)
        {
            _myThread = new Thread(Read);
            _myThread.Name = $"Читатель {i}";
            _myThread.Start();
        }


        public void Read()
        {
            while (_count > 0)
            {
                _semaphore.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} входит в библиотеку");

                Console.WriteLine($"{Thread.CurrentThread.Name} читает");
                Thread.Sleep(1000);

                Console.WriteLine($"{Thread.CurrentThread.Name} покидает библиотеку");

                _semaphore.Release();

                _count--;
                Thread.Sleep(1000);
            }
        }
    }
}