//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Notificator.Core.Model
{
    public partial class NotificatorEntities : ObjectContext
    {
        public const string ConnectionString = "name=NotificatorEntities";
        public const string ContainerName = "NotificatorEntities";
    
        #region Constructors
    
        public NotificatorEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public NotificatorEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public NotificatorEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Application> Applications
        {
            get { return _applications  ?? (_applications = CreateObjectSet<Application>("Applications")); }
        }
        private ObjectSet<Application> _applications;
    
        public ObjectSet<Event> Events
        {
            get { return _events  ?? (_events = CreateObjectSet<Event>("Events")); }
        }
        private ObjectSet<Event> _events;
    
        public ObjectSet<UserEventLink> UserEventLinks
        {
            get { return _userEventLinks  ?? (_userEventLinks = CreateObjectSet<UserEventLink>("UserEventLinks")); }
        }
        private ObjectSet<UserEventLink> _userEventLinks;
    
        public ObjectSet<User> Users
        {
            get { return _users  ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private ObjectSet<User> _users;

        #endregion
        #region Function Imports
        public ObjectResult<EventInfo> GetAllEvents(Nullable<int> userId)
        {
    
            ObjectParameter userIdParameter;
    
            if (userId.HasValue)
            {
                userIdParameter = new ObjectParameter("UserId", userId);
            }
            else
            {
                userIdParameter = new ObjectParameter("UserId", typeof(int));
            }
            return base.ExecuteFunction<EventInfo>("GetAllEvents", userIdParameter);
        }
        public ObjectResult<User> GetUserByName(string name, Nullable<int> applicationId)
        {
    
            ObjectParameter nameParameter;
    
            if (name != null)
            {
                nameParameter = new ObjectParameter("Name", name);
            }
            else
            {
                nameParameter = new ObjectParameter("Name", typeof(string));
            }
    
            ObjectParameter applicationIdParameter;
    
            if (applicationId.HasValue)
            {
                applicationIdParameter = new ObjectParameter("ApplicationId", applicationId);
            }
            else
            {
                applicationIdParameter = new ObjectParameter("ApplicationId", typeof(int));
            }
            return base.ExecuteFunction<User>("GetUserByName", nameParameter, applicationIdParameter);
        }
        public ObjectResult<Event> GetEventById(Nullable<int> id)
        {
    
            ObjectParameter idParameter;
    
            if (id.HasValue)
            {
                idParameter = new ObjectParameter("Id", id);
            }
            else
            {
                idParameter = new ObjectParameter("Id", typeof(int));
            }
            return base.ExecuteFunction<Event>("GetEventById", idParameter);
        }
        public ObjectResult<EventInfo> GetUserEvents(Nullable<int> userId)
        {
    
            ObjectParameter userIdParameter;
    
            if (userId.HasValue)
            {
                userIdParameter = new ObjectParameter("UserId", userId);
            }
            else
            {
                userIdParameter = new ObjectParameter("UserId", typeof(int));
            }
            return base.ExecuteFunction<EventInfo>("GetUserEvents", userIdParameter);
        }
        public ObjectResult<NotificationLogItem> GetNotificationLog(Nullable<int> userId, Nullable<int> lastLinkId, Nullable<int> topCount)
        {
    
            ObjectParameter userIdParameter;
    
            if (userId.HasValue)
            {
                userIdParameter = new ObjectParameter("UserId", userId);
            }
            else
            {
                userIdParameter = new ObjectParameter("UserId", typeof(int));
            }
    
            ObjectParameter lastLinkIdParameter;
    
            if (lastLinkId.HasValue)
            {
                lastLinkIdParameter = new ObjectParameter("LastLinkId", lastLinkId);
            }
            else
            {
                lastLinkIdParameter = new ObjectParameter("LastLinkId", typeof(int));
            }
    
            ObjectParameter topCountParameter;
    
            if (topCount.HasValue)
            {
                topCountParameter = new ObjectParameter("TopCount", topCount);
            }
            else
            {
                topCountParameter = new ObjectParameter("TopCount", typeof(int));
            }
            return base.ExecuteFunction<NotificationLogItem>("GetNotificationLog", userIdParameter, lastLinkIdParameter, topCountParameter);
        }
        public ObjectResult<Application> GetApplicationByName(string name)
        {
    
            ObjectParameter nameParameter;
    
            if (name != null)
            {
                nameParameter = new ObjectParameter("Name", name);
            }
            else
            {
                nameParameter = new ObjectParameter("Name", typeof(string));
            }
            return base.ExecuteFunction<Application>("GetApplicationByName", nameParameter);
        }
        public ObjectResult<Application> GetApplicationById(Nullable<int> id)
        {
    
            ObjectParameter idParameter;
    
            if (id.HasValue)
            {
                idParameter = new ObjectParameter("Id", id);
            }
            else
            {
                idParameter = new ObjectParameter("Id", typeof(int));
            }
            return base.ExecuteFunction<Application>("GetApplicationById", idParameter);
        }
        public ObjectResult<User> GetUserById(Nullable<int> id, Nullable<int> applicationId)
        {
    
            ObjectParameter idParameter;
    
            if (id.HasValue)
            {
                idParameter = new ObjectParameter("Id", id);
            }
            else
            {
                idParameter = new ObjectParameter("Id", typeof(int));
            }
    
            ObjectParameter applicationIdParameter;
    
            if (applicationId.HasValue)
            {
                applicationIdParameter = new ObjectParameter("ApplicationId", applicationId);
            }
            else
            {
                applicationIdParameter = new ObjectParameter("ApplicationId", typeof(int));
            }
            return base.ExecuteFunction<User>("GetUserById", idParameter, applicationIdParameter);
        }

        #endregion
    }
}
