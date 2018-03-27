using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

using FutureRobot.FuroWare.Common.Utils;
using FutureRobot.FuroWare.Common.Interfaces;

namespace FutureRobot.FuroWare.Applications
{
    public class SvoxSpeech : ISpeech
    {
        string speechData, speechStatus = string.Empty;

        public void PlaySpeech(string speechData)
        {
        }  //NOT IMPLEMENT
        public void PlayLipSync(string speechData)
        {
        }  //NOT IMPLEMENT
        public void StopSpeech()
        {
        }  //NOT IMPLEMENT
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
        }  //NOT IMPLEMENT
        public void SetSpeaker(string speaker)
        {
        }  //NOT IMPLEMENT
        public string GetSpeechStatus()
        {
            return speechStatus;
        }
        public void CloseProgram()
        {
        }  //NOT IMPLEMENT
    }
}
