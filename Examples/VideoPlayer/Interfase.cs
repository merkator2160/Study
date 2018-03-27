using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.AudioVideoPlayback;

namespace VideoPlayer
{
    public partial class Interfase : Form
    {
        Video video;

        double currentPosition;
        string currentFile;

        public Interfase()
        {
            InitializeComponent();         
        }
        private void Interfase_Load(object sender, EventArgs e)
        {
            //У формы есть свойство KeyPreview, и если оно установлено в true, то в начале все события клавиатуры будут проходить через обработчики формы, а уж потом через обработчики компонентов на форме.
            this.KeyPreview = true;
            this.KeyUp += new KeyEventHandler(Key_Handler);   
        }       
//------------------------------------------------------------------------------------     
        private void playButton_Click(object sender, EventArgs e)
        {
            if (video == null)
            {
                label1.Text = "Следует указать имя файла!";
                notifyIcon1.BalloonTipText = "Следует указать имя файла!";
            }
            else
            {
                if (video.Playing)
                {
                    video.Pause();
                    playButton.Text = "Play";
                }
                else
                {
                    video.Play();
                    stopButton.Visible = true;
                    playButton.Text = "Pause";
                }
            }
        }
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\видео";
            openFileDialog.Filter = "All files (*.*)|*.*|avi files (*.avi)|*.avi|MKV files (*.mkv)|*.mkv";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                label1.Text = FileNameCutter(openFileDialog.FileName);

                notifyIcon1.BalloonTipText = FileNameCutter(openFileDialog.FileName);
                notifyIcon1.Text = FileNameCutter(openFileDialog.FileName);
                notifyIcon1.ShowBalloonTip(10);
                
                currentFile = openFileDialog.FileName;
                if (video != null)
                {
                    video.Stop();
                    video.Dispose();
                }
                Play(openFileDialog.FileName, 0);
                playButton.Text = "Pause";
                stopButton.Visible = true;
            }
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            video.Stop();
            stopButton.Visible = false;
            playButton.Text = "Play";
        }        
//------------------------------------------------------------------------------------
        private void Key_Handler(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (video.Fullscreen)
                        {
                            video.Pause();
                            currentPosition = video.CurrentPosition;
                            video.Dispose();
                            Play(currentFile, currentPosition);
                        }
                        else
                        {
                            video.Fullscreen = true;
                        }
                break;

                case Keys.Space:
                if (video.Playing)
                {
                    video.Pause();
                    playButton.Text = "Play";
                }
                else
                {
                    video.Play();
                    playButton.Text = "Pause";
                }
                break;

                default:
                break;
            }
        }
        private void video_Ending(object sender, EventArgs e)
        {
            video.Stop();
            stopButton.Visible = false;
            playButton.Text = "Play";
        }
//------------------------------------------------------------------------------------
        private void Play(string fileName, double position)
        {
            currentFile = fileName;
            try
            {
                video = new Video(fileName);
                video.Ending += new EventHandler(video_Ending);
                video.Owner = pictureBox1;
                video.Fullscreen = false;
                video.CurrentPosition = position;

                ChangeSize();

                video.Play();
                
                stopButton.Visible = true;
            }
            catch 
            {
                label1.Text = "Не могу открыть!";
                notifyIcon1.BalloonTipText = "Не могу открыть!";
                notifyIcon1.ShowBalloonTip(10);
            }
        }
        private void ChangeSize()
        {
            Size windowRectangle;

            windowRectangle = video.DefaultSize;
            windowRectangle.Height  += 50;
            
            this.ClientSize = windowRectangle;

            this.label1.Location = new System.Drawing.Point(9, video.DefaultSize.Height + 5);
            
            this.openButton.Location = new System.Drawing.Point(12, video.DefaultSize.Height + 20);
            this.playButton.Location = new System.Drawing.Point(93, video.DefaultSize.Height + 20);
            this.stopButton.Location = new System.Drawing.Point(174, video.DefaultSize.Height + 20);
        }        
        private string FileNameCutter(string fileNamePath)
        {             
            //string delimStr = " ,.:";
            //char[] delimiter = delimStr.ToCharArray();

            string[] stringSeparators = new string[] {"\\"};
            string[] path = fileNamePath.Split(stringSeparators, 1000, StringSplitOptions.RemoveEmptyEntries);
            return path.Last();
        }         
    }
}
