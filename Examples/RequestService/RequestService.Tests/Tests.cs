using System;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using RequestService.Common.Interfaces;
using RequestService.Common.Models;
using RequestService.Common.Models.Request;
using UnityLog4NetExtension.Log4Net;



namespace RequestService.Tests
{
    [TestFixture]
    public class RequestServiceTest
    {
        private UnityContainer _container;
        private RequestService _requestService;

        
        
        [SetUp]
        public void Initialyze()
        {
            _container = new UnityContainer();

            _container.RegisterType<RequestService>();
            _container.RegisterType<IDataProvider, FakeDataProvider>();
            _container.AddNewExtension<Log4NetExtension>();
        }



        [Test]
        public void AddUser()
        {
            if (_requestService == null)
                _requestService = _container.Resolve<RequestService>();

            Assert.IsTrue(_requestService.ConnectionRequest());
        }
        [Test]
        public void AddRequest()
        {
            if (_requestService == null)
                _requestService = _container.Resolve<RequestService>();
            
            var request = new JuridicalTrackingRequest
            {
                Message = "Def message"
            };
            var parameters = new RequestParameters
            {
                Request = request,
                UserId = Guid.NewGuid()
            };

            Assert.IsTrue(_requestService.AddRequest(RequestType.JuridicalTracking, parameters));
        }
        [Test]
        public void ConnectionRequest()
        {
            if (_requestService == null)
                _requestService = _container.Resolve<RequestService>();

            Assert.IsTrue(_requestService.AddUser("John", "Smit") != Guid.Empty);
        }


        #region Stubs

        private class FakeDataProvider : IDataProvider
        {
            // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
            public void AddUser(string firstName, string lastName, Guid userId)
            {
                Trace.WriteLine("AddUser");
            }
            public void AddRequest(RequestType type, string text, DateTime creationDateTime, Guid userId)
            {
                Trace.WriteLine("AddRequest");
            }
        }

        #endregion
    }
}
