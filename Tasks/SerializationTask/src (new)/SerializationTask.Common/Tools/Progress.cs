using System.Diagnostics;
using System.Globalization;

namespace SerializationTask.Common.Tools
{
    public class Progress
    {
        private readonly String _taskName;
        private readonly UInt32 _logFrequencyDivider;


        
        public Progress(Int64 total) : this(total, "Progress")
        {

        }
        public Progress(Int64 total, String taskName) : this(total, taskName, 1)
        {

        }
        public Progress(Int64 total, UInt32 logFrequencyDivider) : this(total, "Progress", logFrequencyDivider)
        {

        }
        public Progress(Int64 total, String taskName, UInt32 logFrequencyDivider)
        {
            Total = total;
            _taskName = taskName;
            _logFrequencyDivider = logFrequencyDivider;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Int64 Total { get; }
        public Int64 HandledCount { get; private set; }
        public Single ProgressPercentage
        {
            get
            {
                if (HandledCount == 0)
                    return 0;

                return (Single)HandledCount / Total;
            }
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public override String ToString()
        {
            return $"{_taskName}: {ProgressPercentage.ToString("P2", CultureInfo.InvariantCulture)}, {HandledCount} of {Total}";
        }
        public void Next()
        {
            lock (this)
            {
                HandledCount++;

                if(HandledCount % _logFrequencyDivider == 0)
                    Trace.TraceInformation(ToString());
            }
        }
    }
}