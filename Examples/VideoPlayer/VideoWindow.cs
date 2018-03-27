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
    public partial class VideoWindow : Form
    {
        Video video;
        double currentPosition;
        string currentFile;

        public VideoWindow()
        {
            InitializeComponent();
            //У формы есть свойство KeyPreview, и если оно установлено в true, то в начале все события клавиатуры будут проходить через обработчики формы, а уж потом через обработчики компонентов на форме.
            this.KeyPreview = true;
            this.KeyUp += new KeyEventHandler(FullScreen_StateChange);
        }
//------------------------------------------------------------------------------------
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FullScreen_StateChange();
        }
        private void FullScreen_StateChange(object sender, KeyEventArgs e)
        {
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
        }
        private void FullScreen_StateChange()
        {
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
        }
        public void Play(string fileName, double position)
        {
            currentFile = fileName;
            video = new Video(fileName);
            video.Owner = pictureBox1;
            video.Fullscreen = false;
            video.CurrentPosition = currentPosition;

            this.ClientSize = video.Size;
            pictureBox1.Size = this.ClientSize;

            video.Play();
        }


    }
}
