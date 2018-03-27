using System;
using System.Runtime.Serialization;
using RequestService.Common.Interfaces;



namespace RequestService.Common.Models.Request
{
    [DataContract]
    //[KnownType(typeof(JuridicalTrackingRequest))]
    //[KnownType(typeof(MeetingRequest))]
    //[KnownType(typeof(SeminarRequest))]
    public class RequestBase
    {
        [DataMember]
        public String Message { get; set; }
    }
}
