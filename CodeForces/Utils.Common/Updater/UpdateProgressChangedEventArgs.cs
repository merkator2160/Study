using System;

namespace Utils.Common.Updater
{
    public class UpdateProgressChangedEventArgs : EventArgs
    {
        public Int64 BytesReceived { get; set; }
        public Int64 TotalBytesToReceive { get; set; }
        public Int32 ProgressPercentage { get; set; }
        public String Message { get; set; }
    }
}