namespace FileTransferFramework.Client
{
    using System;
    using System.Runtime.Serialization;

    public class FileTransferResponse
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DateTime CreateAt { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ResponseStatus { get; set; }        
    }
}
