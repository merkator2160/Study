using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IUltrasonic
    {
        List<double> GetSensorValue();
        void SetData(List<byte> bytes);
    }
}
