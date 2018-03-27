using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NAudio.Wave;

namespace ChartClient
{
    public partial class ChartForm : Form
    {
        private readonly Thread _pipeWorksThread;
        private readonly Thread _chartRefreshThread;

        private readonly Queue<Byte[]> _chartData;
        
        private const Int32 NUMBER_OF_POINTS = 500;
        private const Int32 MAXIMUM = 2147483647;
        private const Int32 MINIMUM = -2147483647;
        
        private enum Channel
        {
            Left, Right
        };

        private WaveOutEvent _waveOut;
        private BufferedWaveProvider _waveProvider;

        private readonly String _outputFilename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\audioFile.wav";
        private readonly WaveFileWriter _fileWriter;
        



        public ChartForm()
        {
            InitializeComponent();
            ChartInite();
            SoundInite();

            _fileWriter = new WaveFileWriter(_outputFilename, new WaveFormat(8000, 32, 2));
            _chartData = new Queue<Byte[]>(1000);


            _chartRefreshThread = new Thread(ChartRefreshThreadLoop);
            _chartRefreshThread.Name = "Refresh chart thread";
            _chartRefreshThread.IsBackground = true;
            _chartRefreshThread.Start();

            _pipeWorksThread = new Thread(PipeProcessingThreadLoop);
            _pipeWorksThread.Name = "Pipe working thread";
            _pipeWorksThread.IsBackground = true;
            _pipeWorksThread.Start();
        }


        

        // USER ACTIONS ///////////////////////////////////////////////////////////////////////////
        private void ChartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _chartRefreshThread.Abort();
            _pipeWorksThread.Abort();

            _fileWriter.Close();
            _waveOut.Stop();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        // THREADS ////////////////////////////////////////////////////////////////////////////////
        private void PipeProcessingThreadLoop()
        {
            var client = new NamedPipeClientStream("MicrophoneDataPipeBothChannels");
            
            while (true)
            {
                client.Connect();
                _waveOut.Play();

                while (true)
                {
                    try
                    {
                        if (client.IsConnected)
                        {
                            var buffer = new byte[6400];
                            client.Read(buffer, 0, 6400);   // read buffer from pipe

                            _chartData.Enqueue(buffer);     // add buffer to work queue
                            _fileWriter.Write(buffer, 0, buffer.Count());   // write buffer to file
                            _waveProvider.AddSamples(buffer, 0, buffer.Count());

                            //client.Write(buffer, 0, buffer.Count());  // prepare buffer to writing
                            //client.Flush();   //write buffer to pipe
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                    catch (IOException)
                    {
                        break;
                    }
                }
            }
        }
        private void ChartRefreshThreadLoop()
        {
            #region INITE LOCAL VARIBLES
            
            var microphoneLeftChannelSeries = LeftChannelSeriesInite();
            var microphoneRightChannelSeries = RightChannelSeriesInite();

            Int16 currentX = 0;
            
            var currentChannel = Channel.Left;

            #endregion

            while (true)
            {
                if (_chartData.Count != 0)
                {
                    try
                    {
                        var value = _chartData.Dequeue();
                        
                        var buf = new Byte[4];
                        Int32 i = 0;

                        foreach (var x in value)
                        {
                            buf[i] = x;
                            
                            i++;

                            if (i == 4)
                            {
                                #region READ SAMPLE

                                var y = BitConverter.ToInt32(buf, 0);
                                
                                // Warning: If we are writing in stereo, than we will be have a deal with two of 16 bit samples: the first sample will be belong to left channel, second - right.
                                if (currentChannel == Channel.Left)
                                {
                                    microphoneLeftChannelSeries.Points.AddY(y);
                                    currentChannel = Channel.Right;
                                }
                                else
                                {
                                    microphoneRightChannelSeries.Points.AddY(y);
                                    currentChannel = Channel.Left;
                                }

                                #endregion

                                #region REFRESH CHART

                                if (currentX >= NUMBER_OF_POINTS * 2 - 1)
                                {
                                    chart.Invoke(new Action(() => RefreshChart(microphoneLeftChannelSeries, microphoneRightChannelSeries, MAXIMUM, MINIMUM)));

                                    microphoneLeftChannelSeries = LeftChannelSeriesInite();
                                    microphoneRightChannelSeries = RightChannelSeriesInite();

                                    currentX = 0;
                                }

                                #endregion

                                currentX++;

                                i = 0;
                                buf = new Byte[4];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void SoundInite()
        {
            _waveOut = new WaveOutEvent
            {
                DeviceNumber = 0,
            };
            _waveProvider = new BufferedWaveProvider(new WaveFormat(8000, 32, 2));
            _waveOut.Init(_waveProvider);
        }
        private Series LeftChannelSeriesInite()
        {
            var microphoneLeftChannelSeries = new Series("Microphone left channel");
            microphoneLeftChannelSeries.Name = "Microphone left channel";
            microphoneLeftChannelSeries.ChartType = SeriesChartType.Spline;
            microphoneLeftChannelSeries.Color = Color.Yellow;
            microphoneLeftChannelSeries.BorderWidth = 3;
            microphoneLeftChannelSeries.ShadowOffset = 2;

            return microphoneLeftChannelSeries;
        }
        private Series RightChannelSeriesInite()
        {
            var microphoneRightChannelSeries = new Series("Microphone right channel");
            microphoneRightChannelSeries.Name = "Microphone right channel";
            microphoneRightChannelSeries.ChartType = SeriesChartType.Spline;
            microphoneRightChannelSeries.Color = Color.Green;
            microphoneRightChannelSeries.BorderWidth = 3;
            microphoneRightChannelSeries.ShadowOffset = 2;

            return microphoneRightChannelSeries;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////

        // INVOKE /////////////////////////////////////////////////////////////////////////////////
        private void RefreshChart(Series leftSeries, Series rightSeries, Int32 maximum, Int32 minimum)
        {
            chart.ChartAreas[0].AxisY.Maximum = maximum;
            chart.ChartAreas[0].AxisY.Minimum = minimum;

            chart.Series[0] = leftSeries;
            chart.Series[1] = rightSeries;

            chart.Invalidate();
        }
        private void ChartInite()
        {
            chart.Titles.Add(new Title("Charts from microphone data pipe."));
            chart.ChartAreas[0].AxisX.Maximum = NUMBER_OF_POINTS;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            
            chart.Series.Add(new Series());
            chart.Series.Add(new Series());
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
    }
}
