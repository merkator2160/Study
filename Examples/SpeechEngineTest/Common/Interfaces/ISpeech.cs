using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISpeechCallBack))]
    //[ServiceContract]
    public interface ISpeech
    {
        [OperationContract(IsOneWay = true)]
        void PlaySpeech(string speechData);
        [OperationContract(IsOneWay = true)]
        void PlayLipSync(string speechData);
        [OperationContract(IsOneWay = true)]
        void StopSpeech();
        [OperationContract(IsOneWay = true)]
        void SetPitch(int pitch);
        [OperationContract(IsOneWay = true)]
        void SetSpeed(int speed);
        [OperationContract(IsOneWay = true)]
        void SetVolume(int volume);
        [OperationContract(IsOneWay = true)]
        void SetLanguage(string language);
        [OperationContract(IsOneWay = true)]
        void SetSpeaker(string speaker);
        [OperationContract(IsOneWay = true)]
        void CloseProgram();
    }

    public interface ISpeechCallBack
    {
        [OperationContract(IsOneWay = true)]
        void OnSpeechStarted(object sender, StringMessageEventArgs e);
        [OperationContract(IsOneWay = true)]
        void OnSpeechEnded(object sender, StringMessageEventArgs e);
        [OperationContract(IsOneWay = true)]
        void OnVisemeUpdated(object sender, StringMessageEventArgs e);
    }
}
