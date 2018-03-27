using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IStartGazer
    {
        StarGazerPosition GetSensorValue();
        void SetData(List<byte> bytes);
    }
}
