using System;

namespace FileTransferFramework.Client
{
    using System.Runtime.Serialization;


    [DataContract]
    public class FileTransferRequest
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public Byte[] Content { get; set; }
    }
}
