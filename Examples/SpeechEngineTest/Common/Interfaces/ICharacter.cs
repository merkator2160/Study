using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    [ServiceContract]
    public interface ICharacter
    {
        [OperationContract(IsOneWay = true)]
        void StopSpeak();
        [OperationContract(IsOneWay = true)]
        void ClearExpression();
        [OperationContract(IsOneWay = true)]
        void SetExpression(uint nExpression, float fIntensity);
        [OperationContract]
        bool GetWindowRect(ref RECT lpRect);
        [OperationContract]
        bool SetWindowPos(IntPtr pWndInsertAfter, int x, int y, int cx, int cy, uint nFlags);
        [OperationContract]
        long ModifyStyle(ulong dwRemove, ulong dwAdd, uint nFlags = 0);
        [OperationContract]
        bool ShowWindow(int nCmdShow);
        [OperationContract(IsOneWay = true)]
        void SetFace(string faceName);
        [OperationContract(IsOneWay = true)]
        void SetHair(string hairName);
        [OperationContract(IsOneWay = true)]
        void UpdatePose(int poseIndex, float influence);
        [OperationContract(IsOneWay = true)]
        void SetLanguage(string language);
        [OperationContract(IsOneWay = true)]
        void PlaySpeech(List<int> visemes, List<int> durations);
        [OperationContract(IsOneWay = true)]
        void SetMouth(int viseme, int duration);
        [OperationContract(IsOneWay = true)]
        void CloseProgram();
    }
}
