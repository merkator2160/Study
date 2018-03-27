using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace FutureRobot.FuroWare
{
    public class ConfigUtil
    {
        public static string VoiceDB { get { return Properties.Settings.Default.VoiceDB; } }
    }
}
