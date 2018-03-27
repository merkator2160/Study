using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.ServiceModel;

using Microsoft.Speech.Synthesis;
using SpeechLib;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;

namespace FutureRobot.FuroWare.Applications
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class MicrosoftSpeech : ISpeech
    {
        public event RobotEventSendingDelegate SpeechStarted, SpeechEnded, VisemeUpdated;

        protected SpeechSynthesizer speechSynthesizer;
        protected SpVoiceClass spVoice;
        protected string culture = Language.KO_KR;
        protected const string categoryPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech Server\v11.0\Voices";
        string speechData;

        protected Dictionary<string, VoiceInfo> installedVoices = new Dictionary<string, VoiceInfo>();

        public MicrosoftSpeech()
        {
            InitSpeech();
            SetLanguage(culture);
        }

        private void InitSpeech()
        {
            try
            {
                speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SetOutputToDefaultAudioDevice();
                //speechSynthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(speechSynthesizer_SpeakCompleted);
                //speechSynthesizer.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(speechSynthesizer_SpeakProgress);

                var voices = speechSynthesizer.GetInstalledVoices();
                foreach (var voice in voices)
                {
                    installedVoices.Add(voice.VoiceInfo.Culture.ToString().ToLower(), voice.VoiceInfo);
                }

                spVoice = new SpVoiceClass();
                spVoice.SetVolume(0);
                spVoice.SetPriority(SPVPRIORITY.SPVPRI_OVER);
                spVoice.StartStream += new _ISpeechVoiceEvents_StartStreamEventHandler(spVoice_StartStream);
                spVoice.EndStream += new _ISpeechVoiceEvents_EndStreamEventHandler(spVoice_EndStream);
                spVoice.Viseme += new _ISpeechVoiceEvents_VisemeEventHandler(spVoice_Viseme);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }
        protected void speechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            try
            {
                //Trace.Write("OnPlaySpeechCompleted\n");
                //SendMessage("OnPlaySpeechCompleted.");


                //if (speechRecognitionEngine != null)
                //    speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

                // voice recognizing by xtion pro
                //var stream = xtionPro.GetAudioStream();
                //stream.Close();
                //Thread.Sleep(2000);
                //stream = xtionPro.GetAudioStream();
                //speechRecognitionEngine.SetInputToWaveStream(stream);
                //var result = speechRecognitionEngine.Recognize();
                //stream.Close();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }
        protected void speechSynthesizer_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            try
            {
                //Trace.Write("speechSynthesizer_SpeakProgress\n");
                //if (speechRecognitionEngine != null)
                //    speechRecognitionEngine.RecognizeAsyncStop();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }
        protected void spVoice_Viseme(int streamNumber, object streamPosition, int duration, SpeechVisemeType nextViseme,SpeechVisemeFeature feature, SpeechVisemeType currentViseme)
        {
            if (VisemeUpdated != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = Convert.ToInt32(currentViseme).ToString() + "," + duration.ToString();
                VisemeUpdated(this, msg);
            }
        }
        protected void spVoice_StartStream(int streamNumber, object streamPosition)
        {
            if (SpeechStarted != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechStarted(this, msg);
            }
        }
        protected void spVoice_EndStream(int streamNumber, object streamPosition)
        {
            if (SpeechEnded != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechEnded(this, msg);
            }
        }
        

        //InterfaceMethods///////////////////////////////////////////////////////////////////////////////////////////
        

        public void PlaySpeech(string speechData)
        {
            try
            {
                Trace.Write("PlaySpeech(" + speechData + ")");
                this.speechData = speechData;
                spVoice.Speak(speechData, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                System.Threading.Thread.Sleep(300);
                speechSynthesizer.SpeakAsync(speechData);
                //SendMessage("OnPlaySpeechStarted." + speechData);


                //음성데이터 wav 파일 생성
                //string wavName = "./voice/";
                //wavName += DateTime.Now.ToString("yyyyMMdd_HHmmss.wav");
                //DirectoryInfo di = new DirectoryInfo("./voice/");
                //if (!di.Exists)
                //{
                //    di.Create();
                //}

                //SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
                //SpFileStream SpFileStream = new SpFileStream();
                //SpFileStream.Open(wavName, SpFileMode, false);
                //spVoice.AudioOutputStream = SpFileStream;
                //spVoice.Rate = 0;
                //spVoice.Volume = 100;
                //spVoice.Speak(speechData, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                //spVoice.WaitUntilDone(Timeout.Infinite);
                //SpFileStream.Close();


            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }
        public void PlayLipSync(string speechData)
        {
            Trace.Write("PlayLipSync(" + speechData + ")");
            spVoice.Speak(speechData, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }
        public void StopSpeech()
        {
            try
            {
                Trace.Write("StopSpeech()");
                spVoice.Skip("Sentence", int.MaxValue);
                speechSynthesizer.SpeakAsyncCancelAll();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }

        public void SetPitch(int pitch)
        {
        }  //NOT IMPLEMENT
        public void SetSpeed(int speed)
        {
        }  //NOT IMPLEMENT
        public void SetVolume(int volume)
        {
        }  //NOT IMPLEMENT
        public void SetLanguage(string language)
        {
            language = language.ToLower();

            if (installedVoices.Keys.Contains(language))
            {
                try
                {
                    culture = language;
                    speechSynthesizer.SelectVoice(installedVoices[culture].Name);
                    var voiceToken = new SpObjectTokenClass();
                    voiceToken.SetId(categoryPath + @"\Tokens\" + installedVoices[culture].Id, categoryPath, false);
                    spVoice.SetVoice(voiceToken);
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.ToString());
                }
            }
        }
        public void SetSpeaker(string speaker)
        {
        }  //NOT IMPLEMENT
        public void CloseProgram()
        {
        }  //NOT IMPLEMENT
    }
}
