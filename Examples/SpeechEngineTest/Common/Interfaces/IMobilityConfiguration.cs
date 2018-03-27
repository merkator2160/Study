using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IMobilityConfiguration
    {
        // Odometry Type Set
        void SetEncoderType(byte mode);
        // 6th waist command
        //TX
        void DriveOneAxis(byte partId, UInt16 positionValue, UInt16 angularVelocity);
        void QueryVersion();
        void QueryMotorState();
        void SetBoardMode(byte mode);
        // 6th waist 
        void SetLimit(byte partId, UInt16 minimumPosition, UInt16 initPosition, UInt16 maximumPosition);
        void SetGain(byte partId, UInt32 pGain, UInt32 iGain, UInt32 dGain);
        void RequestLimit(byte id);
        void RequestGain(byte id);
        BodyPose GetWaistPosition();
        BodyPoseLimit GetPositionLimitRead();
        BodyPoseGain GetPositionGainRead();
        // 6th raw protocol
        void ServoPower(byte id, byte mode);
        void DriveMotorVelocityMode(byte id, Int32 velocity);
        void DriveMotorPwmMode(byte id, Int32 velocity);
        // Accelation Setting
        void SetAccelation(UInt16 firstAxis, UInt16 secondAxis, UInt16 thirdAxis, UInt16 fourthAxis);
        //2013.07.02
        void DriveDifferentialVelocityAcceleration(int leftVelocity, int leftAcceleration, int rightVelocity, int rightAcceleration);
    }
}
