using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    /// <summary>
    /// 로봇 운영 기능 인터페이스
    /// 2013.10.31 by Mini
    /// </summary>
    public interface IDriveFunction
    {
        void DoProcess(ExternalInputInfo input, ref ExternalOutputInfo output);
    }
}
