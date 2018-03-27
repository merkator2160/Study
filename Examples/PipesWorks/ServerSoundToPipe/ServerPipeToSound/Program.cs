using System.Threading;



namespace ServerSoundToPipe
{
    class Program
    {
        static void Main()
        {
            using (var multyStartProtection = new Mutex(false, "Microphone listener server"))
            {
                if (multyStartProtection.WaitOne(0, false))
                {
                    var listener = new MicrophoneListener();

                    var microphoneListenerThread = new Thread(listener.Run)
                    {
                        IsBackground = true
                    };
                    microphoneListenerThread.Start();

                    while (true)
                    {
                        Thread.Sleep(10000);
                    }
                }
            }
        }


    }
}
