using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RequestService.Common.Interfaces;



namespace RequestService.Common.Models.Request
{
    [DataContract]
    public class RequestParameters
    {
        [DataMember]
        public RequestBase Request { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }
}
