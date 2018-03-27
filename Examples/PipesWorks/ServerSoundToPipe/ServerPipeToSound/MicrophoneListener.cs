using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio;
using NAudio.Wave;

namespace ServerSoundToPipe
{
    class MicrophoneListener
    {
        private WaveInEvent _waveIn;
        private NamedPipeServerStream _server;
        private Boolean _exceptionOnce;
        private Int32 _pocketNumber = 0;

        
        

        public void Run()
        {
            Console.WriteLine("Listener woiking...");

            InitialyzeMicrophoneListener();
            
            while (true)
            {
                if (!_server.IsConnected)
                {
                    Console.WriteLine("Waiting for a new client...");
                    InitPipe();
                }

                Thread.Sleep(1000);
            }
        }




        private void InitialyzeMicrophoneListener()
        {
            _server = new NamedPipeServerStream("MicrophoneDataPipeBothChannels", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            
            _waveIn = new WaveInEvent 
            {
                DeviceNumber = 0,
                WaveFormat = new WaveFormat(8000, 32,  2),
                BufferMilliseconds = 100
            };
        }
        private void InitPipe()
        {
            try
            {
                _server.WaitForConnection();
                _exceptionOnce = false;
                _pocketNumber = 0;
                Console.WriteLine("Client connected");

                _waveIn.DataAvailable += waveIn_DataAvailable;
                _waveIn.StartRecording();
            }
            catch (MmException)
            {
                Console.WriteLine("No microphone is connected");
                Console.ReadKey();
            }
        }
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {            
                _server.BeginWrite(e.Buffer, 0, e.BytesRecorded, WriteCallback, new Object());
            }
            catch (Exception)
            {
                Ooops();
            }
        }
        private void WriteCallback(IAsyncResult ar)
        {
            Console.WriteLine("Pocket № " + _pocketNumber + " was sent");
            _pocketNumber++;
        }
        private void Ooops()
        {
            if (!_exceptionOnce)
            {
                _exceptionOnce = true;
               
                _waveIn.DataAvailable -= waveIn_DataAvailable;
                Console.WriteLine("Client was lost");
                _waveIn.StopRecording();
                _server.Disconnect();
            }
        }

    }
}
