using System;
using System.Diagnostics;
using RequestService.Common.Interfaces;
using RequestService.Common.Models;



namespace RequestService.FakeDataLayer
{
    public class FakeDataProvider : IDataProvider
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
}
