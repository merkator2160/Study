using System;
using System.Diagnostics;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using RequestService.Common.Interfaces;
using RequestService.Common.Models;



namespace RequestService.DataLayer
{
    public class DbDataProvider : IDataProvider
    {
        private readonly RequestModelContainer _db;



        [InjectionConstructor]
        public DbDataProvider()
        {
            _db = new RequestModelContainer();
        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        [Dependency]
        public ILog Logger { get; set; }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void AddUser(String firstName, String lastName, Guid userId)
        {
            Trace.WriteLine("AddUser");

            try
            {
                _db.UsersSet.Add(new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    SystemUserID = userId
                });
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        public void AddRequest(RequestType type, String text, DateTime creationDateTime, Guid userId)
        {
            Trace.WriteLine("AddRequest");

            try
            {
                var user = _db.UsersSet.Single(p => p.SystemUserID.Equals(userId));
                _db.RequestSet.Add(new Request
                {
                    CreationDate = creationDateTime,
                    Message = text,
                    Type = type,
                    SystemRequestID = Guid.NewGuid(),
                    User = user
                });
                _db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw new UserNotFoundException("User is not exist.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
