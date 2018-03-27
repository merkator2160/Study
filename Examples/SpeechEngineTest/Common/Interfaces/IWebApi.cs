using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    public interface IWebApi
    {
        byte[] GetWebCamData(string robotID);
        void SetWebCamData(string robotID, byte[] imageArray);
        void DeleteWebCamData(string robotID);
        List<string> GetRobotCommands(string robotID);
        void SetRobotCommands(string robotID, List<string> cmd);
        void DeleteRobotCommands(string robotID);
        List<string> GetRobotEvents(string robotID);
        void SetRobotEvents(string robotID, List<string> evt);
        void DeleteRobotEvents(string robotID);
        string SetPosterData(string robotID, string posterName, byte[] imageArray);
        void DeletePoster(string posterCode);
        string SetGuestBookData(string robotID, string noteID, string noteType, string fileType, byte[] imageArray); // 2013.10.04 by Mini
    }
}
