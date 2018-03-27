using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IControlBoard
    {
        
        // RX
        BodyPose GetPosition();
        BodyPoseLimit GetPositionLimitRead();
        BodyPoseGain GetPositionGainRead();
        BoardInfo GetBoardInfo();
        void SetData(List<byte> bytes);
        //int GetPositionData();
        void DriveOneAxis(byte partId, UInt16 positionValue, UInt16 angularVelocity);
    }
}
