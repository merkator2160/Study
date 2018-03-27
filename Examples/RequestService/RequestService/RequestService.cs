using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using log4net;
using RequestService.Common.Interfaces;
using RequestService.Common.Models;
using RequestService.Common.Models.Request;


namespace RequestService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, AddressFilterMode = AddressFilterMode.Any)]
    public class RequestService : IRequestService
    {
        private delegate Boolean AddRequestDelegate(RequestParameters requestData);
        private readonly Dictionary<RequestType, AddRequestDelegate> requestDictionary;
        private readonly IDataProvider _dbDataProvider;
        private readonly ILog _logger;


        public RequestService(IDataProvider dbDataProvider, ILog logger)
        {
            requestDictionary = new Dictionary<RequestType, AddRequestDelegate>
            {
                {RequestType.Seminar, AddSeminarRequest},
                {RequestType.Meeting, AddMeetingRequest},
                {RequestType.JuridicalTracking, AddJuridicalTrackingRequest}
            };
        }

        

        // SERVICE ////////////////////////////////////////////////////////////////////////////////
        public Guid AddUser(String firstName, String lastName)
        {
            Trace.WriteLine("AddUser");

            if (firstName.Trim() == String.Empty || lastName.Trim() == String.Empty)
                return Guid.Empty;

            try
            {
                var userId = Guid.NewGuid();
                _dbDataProvider.AddUser(firstName, lastName, userId);
                _logger.Info(String.Format("Add user: {0} {1}", firstName, lastName));
                return userId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Guid.Empty;
            }
        }
        public Boolean AddRequest(RequestType requestType, Object parameters)
        {
            Trace.WriteLine("AddRequest");

            try
            {
                if (parameters != null)
                {
                    var requestData = (RequestParameters)parameters;

                    if (requestData.UserId == Guid.Empty)
                        return false;

                    AddRequestDelegate method;
                    if (requestDictionary.TryGetValue(requestType, out method))
                        return method.Invoke(requestData);
                }
                return false;
            }
            catch (UserNotFoundException ex)
            {
                _logger.Debug(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }
        public Boolean ConnectionRequest()
        {
            Trace.WriteLine("ConnectionRequest");
            _logger.Debug("ConnectionRequest");
            return true;
        }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public static Type GetType(String typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        private Boolean AddJuridicalTrackingRequest(RequestParameters requestData)
        {
            if (requestData.Request != null)
            {
                var request = (JuridicalTrackingRequest)requestData.Request;

                if (request.Message.Trim() == String.Empty)
                    return false;

                _dbDataProvider.AddRequest(RequestType.JuridicalTracking, request.Message, DateTime.Now, requestData.UserId);
                return true;
            }
            return false;
        }
        private Boolean AddMeetingRequest(RequestParameters requestData)
        {
            if (requestData.Request != null)
            {
                var request = (MeetingRequest)requestData.Request;

                if (request.Message.Trim() == String.Empty)
                    return false;

                _dbDataProvider.AddRequest(RequestType.Meeting, request.Message, DateTime.Now, requestData.UserId);
                return true;
            }
            return false;
        }
        private Boolean AddSeminarRequest(RequestParameters requestData)
        {
            if (requestData.Request != null)
            {
                var request = (SeminarRequest)requestData.Request;

                if (request.Message.Trim() == String.Empty)
                    return false;

                _dbDataProvider.AddRequest(RequestType.Seminar, request.Message, DateTime.Now, requestData.UserId);
                return true;
            }
            return false;
        }
    }
}
