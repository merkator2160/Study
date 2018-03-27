using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IControlBoardConfiguration
    {
        //TX
        void SetGain(byte partId, UInt32 pGain, UInt32 iGain, UInt32 dGain);
        void SetLimit(byte partId, UInt16 minimumPosition, UInt16 initPosition, UInt16 maximumPosition);
        void RequestLimit(byte id);
        void RequestGain(byte id);
        void SetPositionType(byte mode);
        void SetBoardMode(byte mode);
        void QueryVersion();
        void QueryMotorState();
        void AutoLimit();
        // 6th raw protocol
        void ServoPower(byte id, byte mode);
        void DriveMotorPwmMode(byte id, Int32 velocity);
        void SetAccelation(UInt16 firstAxis, UInt16 secondAxis, UInt16 thirdAxis, UInt16 fourthAxis);
    }
}
