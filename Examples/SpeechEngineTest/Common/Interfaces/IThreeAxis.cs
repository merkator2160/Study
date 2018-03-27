using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IThreeAxis
    {
        void SendOneAxisPacket(byte deviceID, short devicePosition, short deviceVelocity);
    }
}
