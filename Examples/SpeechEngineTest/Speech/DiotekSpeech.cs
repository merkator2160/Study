using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;

namespace FutureRobot.FuroWare.Applications
{
    public class DiotekSpeech : ISpeech
    {
        const string powetTTSdll = "PowerTTS_M_U.dll";
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_Initialize();
        [DllImport(powetTTSdll)]
        private static extern void PTTS_U_UnInitialize();
        [DllImport(powetTTSdll, CharSet = CharSet.Unicode)]
        private static extern int PTTS_U_LoadEngine(int Language, string DbDir, int bLoadInMemory);
        [DllImport(powetTTSdll)]
        private static extern void PTTS_U_UnLoadEngine(int Language);
        [DllImport(powetTTSdll, CharSet = CharSet.Unicode)]
        private static extern int PTTS_U_LoadEngineEx(int Language, int SpeakerID, string DbDir, int bLoadInMemory);
        [DllImport(powetTTSdll)]
        private static extern void PTTS_U_UnLoadEngineEx(int Language, int SpeakerID);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetCharSet(IntPtr pTTSThread, byte chCharSet);
        [DllImport(powetTTSdll, CharSet = CharSet.Unicode)]
        private static extern void PTTS_U_PlayTTS(IntPtr hUsrWnd, uint uUsrMsg, byte[] TextBuf, string tagString, int Language, int SpeakerID);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_StopTTS();
        [DllImport(powetTTSdll)]
        private static extern IntPtr PTTS_U_CreateThread(IntPtr pInParam, PTTSCallBack CallBack, int Language, int SpeakerID);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetHighLight(IntPtr pTTSThread, byte chMode);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetLipSync(IntPtr pTTSThread, byte chMode);
        [DllImport(powetTTSdll, CharSet = CharSet.Unicode)]
        private static extern int PTTS_U_TextToSpeech(IntPtr pTTSThread, byte[] Text, int bTaggedText);
        [DllImport(powetTTSdll)]
        private static extern void PTTS_U_DeleteThread(IntPtr pTTSThread);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetPitch(IntPtr pTTSThread, int Pitch);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetSpeed(IntPtr pTTSThread, int Speed);
        [DllImport(powetTTSdll)]
        private static extern int PTTS_U_SetVolume(IntPtr pTTSThread, int Volume);

        public delegate int PTTSCallBack(IntPtr pInParam, ref SYNRES pSynRes);

        public struct SYNRES
        {
            public uint NumBytes;
            public IntPtr pData;
            public short Status;
            public IntPtr pVoiceStatus;
            public IntPtr pMarkInfo;
            public IntPtr pReserve1;
            public IntPtr pReserve2;
        }

        public struct VOICESTATUS
        {
            public uint ulWordPos;
            public uint ulWordLen;
            public uint ulSentPos;
            public uint ulSentLen;
            public int lBookmarkID;
            public uint uIPhonemeNum;
            public IntPtr PhonemeID;
            public IntPtr VisemeID;
            public uint dwReserved1;
            public uint dwReserved2;
        }

        public struct PHONEID
        {
            public short PhoneID;
            public int nStPos;
            public int nDur;
            public byte chStress;
        }

        public struct VISEMES
        {
            public short VisemeID;
            public int nStPos;
            public int nDur;
            public byte chStress;
        }

        public struct MARKINFO
        {
            public int nTotalPhoneNum;
            public int nCurrentPhoneNum;
            public IntPtr LipSyncData;
            public IntPtr WordSync;
        }

        public struct LIPSYNCINFO
        {
            public int phType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] phone;
            public byte bComplete;
            public int nStPos;
            public int nDur;
        }

        public struct WORDSYNCINFO
        {
            public int nTextPos_Start;
            public int nTextPos_End;
            public int nAudioPos_Start;
            public int nAudioPos_End;
        }

        public event RobotEventSendingDelegate SpeechStarted, SpeechEnded, VisemeUpdated;

        protected List<int> visemes = new List<int>(), durations = new List<int>();
        string speechData;
        int language = 0, speaker = 0;
        int _speed = 100, _pitch = 100, _volume = 100;
        int rtn0;
        IntPtr pTTS;
        Encoding encoding = Encoding.Unicode;
        Timer ttsTimer;

        PTTSCallBack callback;

        enum ErrorCode
        {
            E_PTTS_FILEOPEN = -1,
            E_PTTS_LACKOFMEMORY = -2,
            E_PTTS_TTSTHREADFAIL = -3,
            E_PTTS_DATEEXPIRED = -4,
            E_PTTS_ENGINELOADFAIL = -5,
            E_PTTS_INVALIDPARAM = -6,
            E_PTTS_INVALIDSPEAKER = -7,
            E_PTTS_INVALIDCHANNEL = -8,
            E_PTTS_NOMORECHANNEL = -9,
            E_PTTS_NOENGINELOAD = -10,
            E_PTTS_OUTOFRANGE = -11,
            E_PTTS_NOTSUPPORTED = -12,
            E_PTTS_LANGUAGEMISMATCH = -13,
            E_PTTS_SPEAKERMISMATCH = -14,
            E_PTTS_CHARSETMISMATCH = -21,
            E_PTTS_VXMLTAGERROR = -22,
            E_PTTS_FORMATMISMATCH = -31,
            E_PTTS_EXCEED_TEXTLIMIT = -41,
            E_PTTS_EXCEED_PCMLIMIT = -42,
            E_PTTS_INITIALIZE = -100,
            E_PTTS_LICENSE_KEY_NOT_FOUND = -101,
            E_PTTS_LICENSE_DATE_EXPIRED = -102,
            E_PTTS_LICENSE_INVALID_SYSTEM = -103,
            E_PTTS_LICENSE_INVALID_KEY = -104,
            E_PTTS_LICENSE_OEMKEY_NOT_FOUND = -201,
            E_PTTS_LICENSE_INVALID_OEMKEY = -202,
            E_PTTS_LICENSE_INVALID_SPEAKER = -301,
            E_PTTS_LICENSE_EXKEY_NOT_FOUND = -401,
            E_PTTS_LICENSE_EXKEY_INVALID_SYSTEM = -403,
            E_PTTS_LICENSE_EXKEY_INVALID_KEY = -404,
        }

        public DiotekSpeech(string lang)
        {
            try
            {
                SetLanguage(lang);

                PTTS_U_Initialize();

                rtn0 = PTTS_U_LoadEngine(language, encoding.GetString(encoding.GetBytes(@"C:\Furo_TTS\PowerTTS-PC\PowerTTS_M_DB\" + lang.ToUpper())), 0);

                //PTTS_U_SetCharSet(IntPtr.Zero, 1); // 0 : ANSI, 1: UTF8 // unicode를 사용할땐 불필요

                if (rtn0 == 0)
                {
                    callback = new PTTSCallBack(this.PCMCallBack);
                    Trace.WriteLine("Diotek TTS engine (" + lang + ") load complete.");
                }
                else
                    Trace.WriteLine("Diotek TTS engine (" + lang + ") load error!! ERR[" + rtn0.ToString() + "] = " + GetErrorString(rtn0));
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }

        ~DiotekSpeech()
        {
            try
            {
                PTTS_U_UnLoadEngine(language);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }

        string GetErrorString(int errCode)
        {
            if (errCode == 1)
                return @"C:\Furo_TTS\PowerTTS-PC\PowerTTS_M_DB\ is not found!!";
            else
                return ((ErrorCode)errCode).ToString("G");
        }

        public void PlaySpeech(string speechData)
        {
            PlayLipSync(speechData);
            var tagString = "<speed=\"" + _speed.ToString() + "\"><pitch=\"" + _pitch + "\"><volume=\"" + _volume + "\">";
            PTTS_U_PlayTTS(IntPtr.Zero, 0, encoding.GetBytes(speechData), encoding.GetString(encoding.GetBytes(tagString)), language, speaker);
        }

        public void PlayLipSync(string speechData)
        {
            ProcVisemes(speechData);

            if (SpeechStarted != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechStarted(this, msg);
            }

            if (VisemeUpdated != null)
            {
                var msg = new StringMessageEventArgs();
                for (var i = 0; i < visemes.Count; i++)
                {
                    if (i > 0) msg.Message += ",";
                    msg.Message += visemes[i].ToString() + "," + durations[i].ToString();
                }
                VisemeUpdated(this, msg);
            }
        }

        void ProcVisemes(string speechData)
        {
            if (this.speechData == speechData) return;

            visemes.Clear();
            durations.Clear();

            pTTS = PTTS_U_CreateThread(IntPtr.Zero, callback, language, speaker);

            if (pTTS != IntPtr.Zero)
            {
                PTTS_U_SetHighLight(pTTS, 1);
                PTTS_U_SetLipSync(pTTS, 1); // 립씽크정보를 받기 위함. 립씽크정보를 받기 위해서는 반드시 먼저 PTTS_U_SetHighLight(1) 함수를 호출하여 주어야 함.
                //PTTS_U_SetCharSet(ptts, 1); // 0 : ANSI, 1: UTF8 // unicode를 사용할땐 불필요

                SetPitch(_pitch);
                SetSpeed(_speed);
                SetVolume(_volume);

                try
                {
                    PTTS_U_TextToSpeech(pTTS, encoding.GetBytes(speechData), 1);
                }
                catch (Exception) { }

                PTTS_U_DeleteThread(pTTS);
                pTTS = IntPtr.Zero;
                
                this.speechData = speechData;
            }

            if (durations.Count > 0)
                ttsTimer = new Timer(ttsTimer_Tick, null, durations.Sum(), Timeout.Infinite);
        }

        int PCMCallBack(IntPtr pInParam, ref SYNRES pSynRes)
        {
            if (pSynRes.NumBytes <= 0) return 0;

            var pVoiceStatus = (VOICESTATUS)Marshal.PtrToStructure(pSynRes.pVoiceStatus, typeof(VOICESTATUS));
            var vis = (VISEMES)Marshal.PtrToStructure(pVoiceStatus.VisemeID, typeof(VISEMES));
            var visSize = Marshal.SizeOf(vis);

            for (var i = 0; i < pVoiceStatus.uIPhonemeNum; i++)
            {
                var VisemeID = (VISEMES)Marshal.PtrToStructure(new IntPtr(pVoiceStatus.VisemeID.ToInt32() + i * visSize), typeof(VISEMES));
                visemes.Add(VisemeID.VisemeID);
                durations.Add(VisemeID.nDur);
            }

            return 0;
        }

        public void StopSpeech()
        {
            PTTS_U_StopTTS();

            if (SpeechEnded != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechEnded(this, msg);
            }
        }

        public void SetPitch(int pitch)
        {
            _pitch = Math.Max(80, Math.Min(pitch, 120));
            PTTS_U_SetPitch(pTTS, _pitch);
        }

        public void SetSpeed(int speed)
        {
            _speed = Math.Max(50, Math.Min(speed, 200));
            PTTS_U_SetSpeed(pTTS, _speed);
        }

        public void SetVolume(int volume)
        {
            _volume = Math.Max(0, Math.Min(volume, 200));
            PTTS_U_SetVolume(pTTS, _volume);
        }

        public void SetLanguage(string language)
        {
            switch (language.ToLower())
            {
                case "en_us":
                case "english":
                    this.language = 1;
                    speaker = 4;
                    break;
                case "zh_cn":
                case "chinese":
                    this.language = 2;
                    speaker = 0;
                    break;
                case "ja_jp":
                case "japanese":
                    this.language = 3;
                    speaker = 0;
                    break;
                case "ko_kr":
                case "korean":
                default:
                    this.language = 0;
                    speaker = 0;
                    break;
            }
        }

        public void SetSpeaker(string speaker)
        {
        }

        public void CloseProgram()
        {
        }

        void ttsTimer_Tick(object state)
        {
            ttsTimer.Dispose();

            if (SpeechEnded != null)
            {
                var msg = new StringMessageEventArgs();
                msg.Message = speechData;
                SpeechEnded(this, msg);
            }
        }
    }
}
