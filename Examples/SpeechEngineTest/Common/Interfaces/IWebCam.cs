using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IWebCam
    {
        Image GetImage();
    }
}
