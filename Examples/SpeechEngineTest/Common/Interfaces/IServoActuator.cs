﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IServoActuator
    {
        int GetPosition();
        void SetPosition(double degree);
        int GetVelocity();
        void SetVelocity(double degreePerSec);
    }
}
