using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IMobility
    {
        WheelEncoder GetOdometry();
        Pose GetPosition();
        BoardInfo GetBoardInfo();
        void SetPosition(Pose pose);
        void DriveWheel(double linearVelocity, double angularVelocity);
        void DriveWheel(double linearVelocity, double angularVelocity, double keepSeconds);
        void DriveDifferential(int leftVelocity, int rightVelocity);
        void DriveDifferential(int leftVelocity, int leftTime, int rightVelocity, int rightTime);
        void MoveWheel(double distance, double linearVelocity);
        void RotateWheel(double angle, double angularVelocity);
        void StopWheel();
        bool IsWheelRunning();
        void SetData(List<byte> bytes);
    }
}
