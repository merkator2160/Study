using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CodeForces.Units.Threads
{
    public static class ThreadPoolUnit
    {
        public static void Run()
        {
            RunTaskDelayAync();
        }
        private static void ThreadsAndMemoryLeak()
        {
            using (var obj = new ThreadObject())
            {
                Thread.Sleep(3000);
            }
        }
        private static async void RunTaskDelayAync()
        {
            await Task.Run(async () =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
        }
        private static async void RunTaskDelayPendedAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("async: Starting");
            var wait = Task.Delay(5000);
            Console.WriteLine("async: Running for {0} seconds", sw.Elapsed.TotalSeconds);
            await wait;
            Console.WriteLine("async: Running for {0} seconds", sw.Elapsed.TotalSeconds);
            Console.WriteLine("async: Done");
        }
    }
}