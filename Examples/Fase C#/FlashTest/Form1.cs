using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxShockwaveFlashObjects;


namespace FlashTest
{
    public partial class Form1 : Form
    {
        private String _basePath;
        private String _awaitPath;
        private String _aaPath;
        private String _ehPath;
        private String _erPath;
        private String _fPath;
        private String _ihPath;
        private String _iyPath;
        private String _owPath;
        private String _sPath;
        private String _tPath;

        private String _browPath;
        private String _disgustPath;
        private String _excitementPath;
        private String _fearPath;
        private String _flirtingFeminineRightPath;
        private String _shockPath;
        private String _smilePath;



        public Form1()
        {
            InitializeComponent();
        }

        

        // USER ACTIONS ///////////////////////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            _basePath = Environment.CurrentDirectory;
            _awaitPath += _basePath + @"\Resources\Phoneme\Await.swf";
            _aaPath += _basePath + @"\Resources\Phoneme\AA.swf";
            _ehPath += _basePath + @"\Resources\Phoneme\EH.swf";
            _erPath += _basePath + @"\Resources\Phoneme\ER.swf";
            _fPath += _basePath + @"\Resources\Phoneme\F.swf";
            _ihPath += _basePath + @"\Resources\Phoneme\IH.swf";
            _iyPath += _basePath + @"\Resources\Phoneme\IY.swf";
            _owPath += _basePath + @"\Resources\Phoneme\OW.swf";
            _sPath += _basePath + @"\Resources\Phoneme\S.swf";
            _tPath += _basePath + @"\Resources\Phoneme\T.swf";

            _browPath += _basePath + @"\Resources\Emotions\Brow.swf";
            _disgustPath += _basePath + @"\Resources\Emotions\Disgust.swf";
            _excitementPath += _basePath + @"\Resources\Emotions\Excitement.swf";
            _fearPath += _basePath + @"\Resources\Emotions\Fear.swf";
            _flirtingFeminineRightPath += _basePath + @"\Resources\Emotions\FlirtingFeminineRight.swf";
            _shockPath += _basePath + @"\Resources\Emotions\Shock.swf";
            _smilePath += _basePath + @"\Resources\Emotions\Smile.swf";
            
            PlayAwait();
        }
        private void playAaBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _aaPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playEhBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _ehPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playErBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _erPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playFBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _fPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playIhBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _ihPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playIyBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _iyPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playOwBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _owPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playSBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _sPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playTBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _tPath);
            flash.Loop = false;
            flash.Play();
        }
        
        private void playBrowBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _browPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playDisgustBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _disgustPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playExcitementBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _excitementPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playFearBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _fearPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playFlirtingFeminineRightBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _flirtingFeminineRightPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playShockBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _shockPath);
            flash.Loop = false;
            flash.Play();
        }
        private void playSmileBtn_Click(object sender, EventArgs e)
        {
            flash.LoadMovie(0, _smilePath);
            flash.Loop = false;
            flash.Play();
        }
        
        private void playAll_Click(object sender, EventArgs e)
        {
            PlayAll(400);
        }
        


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void PlayAwait()
        {
            flash.LoadMovie(0, _awaitPath);
            flash.Loop = true;
            flash.Play();
        }
        private async void PlayAll(Int32 playTime)
        {
            #region Phoneme

            flash.LoadMovie(0, _aaPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _ehPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _erPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _fPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _ihPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));
            
            flash.LoadMovie(0, _iyPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _owPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _sPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _tPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            #endregion
            #region Emotions

            flash.LoadMovie(0, _browPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _disgustPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _excitementPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _fearPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _flirtingFeminineRightPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _shockPath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            flash.LoadMovie(0, _smilePath);
            flash.Loop = false;
            flash.Play();
            await Task.Factory.StartNew(() => Thread.Sleep(playTime));

            #endregion
        }

        

        

        

        
    }
}
