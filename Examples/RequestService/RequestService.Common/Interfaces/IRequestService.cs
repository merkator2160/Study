using System;
using System.ServiceModel;
using RequestService.Common.Models;
using RequestService.Common.Models.Request;



namespace RequestService.Common.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    [ServiceKnownType(typeof(RequestParameters))]
    [ServiceKnownType(typeof(RequestType))]
    [ServiceKnownType(typeof(RequestBase))]
    [ServiceKnownType(typeof(JuridicalTrackingRequest))]
    [ServiceKnownType(typeof(MeetingRequest))]
    [ServiceKnownType(typeof(SeminarRequest))]
    public interface IRequestService
    {
        [OperationContract]
        Guid AddUser(String firstName, String lastName);

        [OperationContract]
        Boolean AddRequest(RequestType requestType, Object parameters);

        [OperationContract]
        Boolean ConnectionRequest();
    }
}
