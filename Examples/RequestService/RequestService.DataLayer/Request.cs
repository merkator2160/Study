//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RequestService.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int Id { get; set; }
        public RequestService.Common.Models.RequestType Type { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public System.Guid SystemRequestID { get; set; }
    
        public virtual User User { get; set; }
    }
}
