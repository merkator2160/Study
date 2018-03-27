using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;
using FutureRobot.FuroWare.Applications;

using AcapelaGroup.BabTTSNet;

namespace FutureRobot.FuroWare
{
    public partial class FormMain : Form
    {
        ServiceHost host;
        SpeechManager speechManager;
        BabTTS tts;


        public FormMain()
        {
            InitializeComponent();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            speechManager = new SpeechManager(ConfigUtil.VoiceDB);

            if (ConfigUtil.VoiceDB.ToLower().IndexOf("acapela") >= 0)
            {
                var path = Helpers.FindAcaTTS();
                Helpers.AcaTTSPath = path.Length > 0 ? path : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Acapela Group\Acapela TTS for Windows\acatts.dll";

                tts = new BabTTS();

                var langPair = ConfigUtil.VoiceDB.Split(',');
                for (var i = 0; i < langPair.Length; i += 2)
                {
                    if (langPair[i + 1].ToLower() == "acapela")
                    {
                        var err = tts.Open(langPair[i], BabTtsOpenModes.BABTTS_USEDEFDICT);
                        speechManager.TTS = tts;
                    }
                }
            }

            speechManager.SpeechStarted += OnSpeechStarted;
            speechManager.SpeechEnded += OnSpeechEnded;
            speechManager.VisemeUpdated += OnVisemeUpdated;
            speechManager.SpeechOpened += OnSpeechOpened;
            speechManager.ProgramClosed += speechManager_ProgramClosed;

            host = new ServiceHost(speechManager, new Uri("net.pipe://localhost/FuroWare/SpeechService"));
            host.AddServiceEndpoint(typeof(ISpeech), new NetNamedPipeBinding(), string.Empty);

            host.Open();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (host != null)
            {
                host.Abort();
                host.Close();
            }
        }
        void OnSpeechOpened(object sender, StringMessageEventArgs e)
        {
            if (tts != null)
            {
                tts.Close();
                tts = new BabTTS();
                var err = tts.Open(e.Message, BabTtsOpenModes.BABTTS_USEDEFDICT);
                speechManager.TTS = tts;
            }
        }
        void OnSpeechStarted(object sender, StringMessageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    listBox1.Items.Clear();
                }));
            }
            else
                listBox1.Items.Clear();
        }
        void OnSpeechEnded(object sender, StringMessageEventArgs e)
        {
        } //NOT IMPLEMENT
        void OnVisemeUpdated(object sender, StringMessageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    listBox1.Items.Add(e.Message);
                }));
            }
            else
                listBox1.Items.Add(e.Message);
        }
        void speechManager_ProgramClosed()
        {
            this.Close();
        }
    }
}
