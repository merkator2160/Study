using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using System.ServiceModel;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    [ServiceContract]
    public interface INui
    {
        [OperationContract]
        int[] GetUsers();
        [OperationContract]
        int GetNearestUser();
        [OperationContract]
        Position GetNearestUserHeadPosition(int user);
        [OperationContract]
        Position GetNearestUserTorsoPosition(int user);
        [OperationContract]
        Position GetNearestHeadPosition();
        [OperationContract]
        Position GetNearestHumanPosition();
        [OperationContract]
        Image GetColorImage();
        [OperationContract]
        byte[] GetColorImageStream();
        [OperationContract]
        Image GetDepthImage();
        [OperationContract]
        byte[] GetDepthImageStream();
        [OperationContract]
        List<ushort> GetHorizontalDepth(int index);
        [OperationContract]
        Stream GetAudioStream();
        [OperationContract(IsOneWay = true)]
        void CloseProgram();
    }
}
