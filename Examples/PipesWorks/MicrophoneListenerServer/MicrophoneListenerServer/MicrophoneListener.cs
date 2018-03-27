using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio;
using NAudio.Wave;

namespace MicrophoneListenerServer
{
    class MicrophoneListener
    {
        private WaveInEvent _waveIn;
        private WaveOutEvent _waveOut;
        private NamedPipeServerStream _server;
        private BufferedWaveProvider _waveProvider;
        private Boolean _exceptionOnce;

        
        

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
            _waveOut = new WaveOutEvent
            {
                DeviceNumber = 0,
            };
            _waveProvider = new BufferedWaveProvider(new WaveFormat(8000, 32, 2));
            _waveOut.Init(_waveProvider);
        }
        private void InitPipe()
        {
            try
            {
                _server.WaitForConnection();
                _exceptionOnce = false;
                Console.WriteLine("Client connected");

                _waveIn.DataAvailable += waveIn_DataAvailable;
                _waveIn.StartRecording();
                _waveOut.Play();
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
            var buffer = new Byte[6400];

            if (ar.IsCompleted)
            {
                try
                {
                    _server.BeginRead(buffer, 0, buffer.Count(), ReadCallback, buffer);
                }
                catch (Exception)
                {
                    Ooops();
                }
            }
        }
        private void ReadCallback(IAsyncResult ar)
        {
            var buffer = ar.AsyncState as Byte[];

            if (ar.IsCompleted)
            {
                try
                {
                    _waveProvider.AddSamples(buffer, 0, buffer.Count());
                }
                catch (Exception)
                {
                    Ooops();
                }
            }
            
        }
        private void Ooops()
        {
            if (!_exceptionOnce)
            {
                _exceptionOnce = true;
               
                _waveIn.DataAvailable -= waveIn_DataAvailable;
                Console.WriteLine("Client was lost");
                _waveIn.StopRecording();
                _waveOut.Stop();
                _server.Disconnect();
            }
        }

    }
}
