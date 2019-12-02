using ProtoBuf;
using System;

namespace CodeForces.Units.Protobuf.Models
{
    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)]
        public String Line1 { get; set; }
        [ProtoMember(2)]
        public String Line2 { get; set; }
        public String Line3 { get; set; }
    }
}