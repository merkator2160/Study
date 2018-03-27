using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IAxis
    {
        void SendOneAxisPacket(byte deviceID, short devicePosition, short deviceVelocity);
        void SendTwoAxisPacket(byte device1ID, short device1Position, short device1Velocity,
            byte device2ID, short device2Position, short device2Velocity);
        void SendThreeAxisPacket(byte device1ID, short device1Position, short device1Velocity,
            byte device2ID, short device2Position, short device2Velocity,
            byte device3ID, short device3Position, short device3Velocity);
    }
}
