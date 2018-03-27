using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;


namespace Sound
{
    public partial class Form1 : Form
    {
        private WaveIn _waveInEvent;
        private WaveOut _waveOut;
        private WaveFileWriter _writer;
        private WaveFileReader _reader;
        private String _outputFilename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\audioFile.wav";    //Имя файла для записи



        public Form1()
        {
            InitializeComponent();
        }



        private void getDevicesBtn_Click(object sender, EventArgs e)
        {
            
        }
        private void playSoundBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(_outputFilename))
            {
                InitializeOutWave();
                _waveOut.Play();
            }
            else
            {
                logRtb.AppendText("File non found" + "\r\n");
            }
        }
        private void stopBtn_Click(object sender, EventArgs e)
        {
            _waveOut.Stop();
        }
        private void startRecordingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                logRtb.AppendText("Start Recording" + "\r\n");

                _waveInEvent = new WaveIn();
                _waveInEvent.DeviceNumber = 0;  //Дефолтное устройство для записи (если оно имеется)

                _waveInEvent.DataAvailable += waveIn_DataAvailable;                       //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                _waveInEvent.RecordingStopped += waveIn_RecordingStopped;                 //Прикрепляем обработчик завершения записи

                _waveInEvent.WaveFormat = new WaveFormat(8000, 1);                        //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                _writer = new WaveFileWriter(_outputFilename, _waveInEvent.WaveFormat);    //Инициализируем объект WaveFileWriter
                _waveInEvent.StartRecording();                                            //Начало записи
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void stopRecordingBtn_Click(object sender, EventArgs e)
        {
            if (_waveInEvent != null)
            {
                StopRecording();
            }
        }
        
        

        private void InitializeOutWave()
        {
            _waveOut = new WaveOut();
            _waveOut.DeviceNumber = 0;
            _waveOut.PlaybackStopped += waveOut_PlayingStopped;
            _reader = new WaveFileReader(_outputFilename);
            _waveOut.Init(_reader);
        }
        
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                _writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            }
        }
        void StopRecording()
        {
            logRtb.AppendText("StopRecording" + "\r\n");
            _waveInEvent.StopRecording();
        }
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                _waveInEvent.Dispose();
                _waveInEvent = null;
                _writer.Close();
                _writer = null;
            }
        }
        private void waveOut_PlayingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveOut_PlayingStopped), sender, e);
            }
            else
            {
                _waveOut.Dispose();
                _waveOut = null;
                _reader.Close();
                _reader = null;
            }
        }
        
        
        




    }
}
