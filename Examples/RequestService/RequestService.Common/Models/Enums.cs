using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;



namespace RequestService.Common.Models
{
    [DataContract]
    public enum RequestType
    {
        [EnumMember]
        Meeting = 0,
        [EnumMember]
        Seminar = 1,
        [EnumMember]
        JuridicalTracking = 2
    }
}
