using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace FutureRobot.FuroWare.Common.Utils
{
    public delegate void StarGazerMessageSendingDelegate(object sender, ListDoubleMessageEventArgs e);
    public delegate void UltrasonicMessageSendingDelegate(object sender, ListDoubleMessageEventArgs e);
    public delegate void LaserRangeFinderDataMessageSendingDelegate(object sender, ListDoubleMessageEventArgs e);
    public delegate void LaserRangeFinderDataCommandSendingDelegate(object sender, ListByteMessageEventArgs e);
    public delegate void MobilityMessageSendingDelegate(object sender, ListByteMessageEventArgs e);
    public delegate void BoardStatusMessageSendingDelegate(object sender, BoardStatusMessageEventArgs e);
    public delegate void OdometryMessageSendingDelegate(object sender, OdometryMessageEventArgs e);
    public delegate void WheelEncoderMessageSendingDelegate(object sender, WheelEncoderMessageEventArgs e);
    public delegate void BodyMessageSendingDelegate(object sender, ListByteMessageEventArgs e);
    public delegate void RobotEventSendingDelegate(object sender, StringMessageEventArgs e);
    public delegate void ImageSendingDelegate(object sender, ImageDataEventArgs e);
    public delegate void ProgramCloseDelegate();
    public delegate void InvokeScriptDelegate(string scriptName, object[] paramObjects);

    public delegate void RobotContentEventDelegate(object sender, RobotContentEventArgs e); // 2013.10.31 by Mini
    public delegate void RobotStatusNotifyEventDelegate(object sender, RobotNotifyEventArgs e);    // 2014.02.06 by Mini
    public delegate void NavigationTargetSettedEventDelegate(object sender, NavigationTargetSettedEventArgs e); // 2014.01.08 by Mini

    public class ListDoubleMessageEventArgs : EventArgs
    {
        public List<double> Message { get; set; }
    }

    public class ListByteMessageEventArgs : EventArgs
    {
        public List<byte> Message { get; set; }
    }

    public class BoardStatusMessageEventArgs : EventArgs
    {
        public BoardStatus Message { get; set; }
    }

    public class OdometryMessageEventArgs : EventArgs
    {
        public Pose Message { get; set; }
    }

    public class WheelEncoderMessageEventArgs : EventArgs
    {
        public WheelEncoder Message { get; set; }
    }

    public class StringMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class ImageDataEventArgs : EventArgs
    {
        public Image Message { get; set; }
    }

    public class RobotNotifyEventArgs : EventArgs
    {
        public string Status { set; get; }
        public string Message { set; get; }

        public RobotNotifyEventArgs(string status, string message)
            : base()
        {
            this.Status = status;
            this.Message = message;
        }
    }

    /// <summary>
    /// 로봇 컨텐트 이벤트 아귀먼트
    /// 2013.10.31 by Mini
    /// </summary>
    public abstract class RobotContentEventArgs : EventArgs
    {
        protected string eventString;

        public string EventString { get { return eventString; } }
        public object[] Parameters { get { return GetParameters(); } }

        public RobotContentEventArgs()
        {
            eventString = "";
        }

        public virtual object[] GetParameters()
        {
            return null;
        }
    }

    public class DriveEventArgs : EventArgs
    {
        public DriveInfo DriveInfo { get; set; }

        private DateTime eventTime;
        public DateTime EventTime { get { return eventTime; } }

        public DriveEventArgs()
        {
            eventTime = DateTime.Now;
            DriveInfo = new DriveInfo();
        }

        public DriveEventArgs(double linear, double angular, bool stop = false)
        {
            eventTime = DateTime.Now;
            DriveInfo = new DriveInfo(linear, angular, stop);
        }
    }

    public class NavigationTargetSettedEventArgs : EventArgs
    {
        public string Target { set; get; }

        public NavigationTargetSettedEventArgs()
        {
            this.Target = "";
        }

        public NavigationTargetSettedEventArgs(string target)
            : this()
        {
            this.Target = target;
        }
    }
}
