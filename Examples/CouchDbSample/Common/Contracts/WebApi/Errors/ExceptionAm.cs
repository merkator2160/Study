using Newtonsoft.Json;

namespace Common.Contracts.WebApi.Errors
{
    public class ExceptionAm
    {
        public String ActualValue { get; set; }
        public String ClassName { get; set; }
        public Dictionary<String, String> Data { get; set; }
        public String ExceptionMethod { get; set; }

        [JsonProperty("HelpURL")]
        public String HelpUrl { get; set; }

        public Int32 HResult { get; set; }
        public ExceptionAm InnerException { get; set; }
        public String Message { get; set; }
        public String ParamName { get; set; }
        public Int32 RemoteStackIndex { get; set; }
        public String RemoteStackTraceString { get; set; }
        public String Source { get; set; }
        public String StackTraceString { get; set; }
        public String WatsonBuckets { get; set; }
    }
}