using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;

using AcapelaGroup.BabTTSNet;

namespace FutureRobot.FuroWare.Applications
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class SpeechManager : ISpeech
    {
        public event RobotEventSendingDelegate SpeechOpened, SpeechStarted, SpeechEnded, VisemeUpdated;
        public event ProgramCloseDelegate ProgramClosed;

        ISpeech iSpeech;
        ISpeechCallBack client;
        MicrosoftSpeech microsoftSpeech;
        AcapelaSpeech acapelaSpeech;
        DiotekSpeech diotekSpeech;

        Dictionary<string, ISpeech> languageSpeech = new Dictionary<string, ISpeech>();

        public BabTTS TTS
        {
            get {
                if (acapelaSpeech == null) return null;
                else return acapelaSpeech.TTS;
            }
            set {
                if (acapelaSpeech != null) acapelaSpeech.TTS = value;
            }
        }
        public SpeechManager(string languageSpeechPair)
        {
            microsoftSpeech = new MicrosoftSpeech();
            microsoftSpeech.SpeechStarted += OnSpeechStarted;
            microsoftSpeech.SpeechEnded += OnSpeechEnded;
            microsoftSpeech.VisemeUpdated += OnVisemeUpdated;

            iSpeech = microsoftSpeech;

            if (languageSpeechPair.ToLower().IndexOf("acapela") >= 0)
            {
                acapelaSpeech = new AcapelaSpeech();
                acapelaSpeech.SpeechOpened += OnSpeechOpened;
                acapelaSpeech.SpeechStarted += OnSpeechStarted;
                acapelaSpeech.SpeechEnded += OnSpeechEnded;
                acapelaSpeech.VisemeUpdated += OnVisemeUpdated;
            }

            var langPair = languageSpeechPair.Split(',');
            for (var i = 0; i < langPair.Length; i += 2)
            {
                switch (langPair[i+1].ToLower())
                {
                    case "diotek":
                        diotekSpeech = new DiotekSpeech(langPair[i]);
                        diotekSpeech.SpeechStarted += OnSpeechStarted;
                        diotekSpeech.SpeechEnded += OnSpeechEnded;
                        diotekSpeech.VisemeUpdated += OnVisemeUpdated;
                        languageSpeech.Add(langPair[i], diotekSpeech);
                        break;
                    case "acapela": languageSpeech.Add(langPair[i], acapelaSpeech); break;
                    case "microsoft":
                    default: languageSpeech.Add(langPair[i], microsoftSpeech); break;
                }
            }
        }
        void MakeConnection()
        {
            //return; //Duplex가 아니면 무조건 return해야함
            if (client == null && OperationContext.Current != null)
                client = OperationContext.Current.GetCallbackChannel<ISpeechCallBack>();
        }
        public void PlaySpeech(string speechData)
        {
            MakeConnection();
            iSpeech.StopSpeech();
            iSpeech.PlaySpeech(speechData);
        }
        public void PlayLipSync(string speechData)
        {
            MakeConnection();
            iSpeech.PlayLipSync(speechData);
        }
        public void StopSpeech()
        {
            MakeConnection();
            iSpeech.StopSpeech();
        }
        public void SetPitch(int pitch)
        {
            MakeConnection();
            iSpeech.SetPitch(pitch);
        }
        public void SetSpeed(int speed)
        {
            MakeConnection();
            iSpeech.SetSpeed(speed);
        }
        public void SetVolume(int volume)
        {
            MakeConnection();
            iSpeech.SetVolume(volume);
        }
        public void SetLanguage(string language)
        {
            MakeConnection();
            iSpeech = null;

            foreach (var v in languageSpeech.Keys)
            {
                if (v == language)
                {
                    iSpeech = languageSpeech[v];
                    break;
                }
            }

            if (iSpeech == null) return;
            
            iSpeech.SetLanguage(language);
            iSpeech.SetSpeaker(language);
        }
        public void SetSpeaker(string speaker)
        {
            MakeConnection();
            iSpeech = null;

            foreach (var v in languageSpeech.Keys)
            {
                if (v == speaker)
                {
                    iSpeech = languageSpeech[v];
                    break;
                }
            }

            if (iSpeech == null) return;

            iSpeech.SetLanguage(speaker);
            iSpeech.SetSpeaker(speaker);
        }
        void OnSpeechOpened(object sender, StringMessageEventArgs e)
        {
            if (SpeechOpened != null) SpeechOpened(sender, e);
        }
        void OnSpeechStarted(object sender, StringMessageEventArgs e)
        {
            try
            {
                if (client != null) client.OnSpeechStarted(sender.ToString(), e);
            }
            catch (Exception ex)
            {
                client = null;
                Trace.Write(ex.ToString());
            }
            if (SpeechStarted != null) SpeechStarted(sender, e);
        }
        void OnSpeechEnded(object sender, StringMessageEventArgs e)
        {
            try
            {
                if (client != null) client.OnSpeechEnded(sender.ToString(), e);
            }
            catch (Exception ex)
            {
                client = null;
                Trace.Write(ex.ToString());
            }
            if (SpeechEnded != null) SpeechEnded(sender, e);
        }
        void OnVisemeUpdated(object sender, StringMessageEventArgs e)
        {
            try
            {
                if (client != null) client.OnVisemeUpdated(sender.ToString(), e);
            }
            catch (Exception ex)
            {
                client = null;
                Trace.Write(ex.ToString());
            }

            if (VisemeUpdated != null)
            {
                VisemeUpdated(sender, e);
            }
        }
        public void CloseProgram()
        {
            if (ProgramClosed != null) ProgramClosed();
        }
    }
}
