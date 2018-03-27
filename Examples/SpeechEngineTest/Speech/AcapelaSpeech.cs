using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.ServiceModel;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;

using AcapelaGroup.BabTTSNet;

namespace FutureRobot.FuroWare.Applications
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class AcapelaSpeech : ISpeech
    {
        public event RobotEventSendingDelegate SpeechOpened, SpeechStarted, SpeechEnded, VisemeUpdated;

        BabTTS tts;
        BabTtsError err;

        string speechData;

        public BabTTS TTS
        {
            get
            {
                return tts;
            }

            set
            {
                tts = value;
                tts.OnSpeak += tts_OnSpeak;
                tts.OnEnd += tts_OnEnd;
                tts.OnPhone += tts_OnPhone;
                tts.SetSettings(BabTtsParam.BABTTS_PARAM_MSGMASK, 0xFFFFFFFF);
            }
        }

        public void PlaySpeech(string speechData)
        {
            Trace.Write("PlaySpeech(" + speechData + ")");
            this.speechData = speechData;
            err = TTS.Speak(speechData, BabTtsSpeakFlags.BABTTS_TXT_UTF8 | BabTtsSpeakFlags.BABTTS_TAG_SAPI | BabTtsSpeakFlags.BABTTS_READ_TEXT | BabTtsSpeakFlags.BABTTS_ASYNC);
        }

        public void PlayLipSync(string speechData)
        {
            Trace.Write("PlayLipSync(" + speechData + ")");
            this.speechData = speechData;
            var volume = TTS.VolumeRatio;
            TTS.VolumeRatio = 0;
            err = TTS.Speak(speechData, BabTtsSpeakFlags.BABTTS_TXT_UTF8 | BabTtsSpeakFlags.BABTTS_TAG_SAPI | BabTtsSpeakFlags.BABTTS_READ_TEXT | BabTtsSpeakFlags.BABTTS_ASYNC);
            TTS.VolumeRatio = volume;
        }

        public void StopSpeech()
        {
            Trace.Write("StopSpeech()");
            TTS.Reset();
        }

        public void SetPitch(int pitch)
        {
            TTS.Pitch = Convert.ToUInt16(pitch);
        }

        public void SetSpeed(int speed)
        {
            TTS.Speed = Convert.ToUInt16(speed);
        }

        public void SetVolume(int volume)
        {
            TTS.VolumeRatio = Convert.ToUInt16(volume);
        }

        public void SetLanguage(string language)
        {
            if (string.IsNullOrEmpty(language.Trim())) return;

            foreach (var v in TTS.VoiceList)
            {
                if (v == language)
                {
                    if (SpeechOpened != null)
                    {
                        var msg = new StringMessageEventArgs();
                        msg.Message = v;
                        SpeechOpened(this, msg);
                    }
                    break;
                }
            }
        }

        public void SetSpeaker(string speaker)
        {
            if (string.IsNullOrEmpty(speaker.Trim())) return;

            foreach (var v in TTS.VoiceList)
            {
                if (v == speaker)
                {
                    if (SpeechOpened != null)
                    {
                        var msg = new StringMessageEventArgs();
                        msg.Message = v;
                        SpeechOpened(this, msg);
                    }
                    break;
                }
            }
        }

        public void CloseProgram()
        {
        }

        int tts_OnSpeak()
        {
            if (SpeechStarted != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechStarted(this, msg);
            }

            return 0;
        }

        int tts_OnEnd()
        {
            if (SpeechEnded != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechEnded(this, msg);
            }

            return 0;
        }

        int tts_OnPhone(BABTTSPHONEINFO value)
        {
            if (VisemeUpdated != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = value.dwViseme.ToString() + "," + value.dwDuration.ToString();
                VisemeUpdated(this, msg);
            }

            return 0;
        }
    }
}
