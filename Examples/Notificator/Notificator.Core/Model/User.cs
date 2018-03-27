//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Notificator.Core.Model
{
    public partial class User
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Username
        {
            get;
            set;
        }
    
        public virtual string Email
        {
            get;
            set;
        }
    
        public virtual bool IsAnonymous
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastActivityDate
        {
            get;
            set;
        }
    
        public virtual string Password
        {
            get;
            set;
        }
    
        public virtual int PasswordFormat
        {
            get;
            set;
        }
    
        public virtual string PasswordSalt
        {
            get;
            set;
        }
    
        public virtual string PasswordQuestion
        {
            get;
            set;
        }
    
        public virtual string PasswordAnswer
        {
            get;
            set;
        }
    
        public virtual bool IsApproved
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreateOn
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastLoginDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastPasswordChangedDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastLockoutDate
        {
            get;
            set;
        }
    
        public virtual int FailedPasswordAttemptCount
        {
            get;
            set;
        }
    
        public virtual System.DateTime FailedPasswordAttemptWindowStart
        {
            get;
            set;
        }
    
        public virtual int FailedPasswordAnswerAttemptCount
        {
            get;
            set;
        }
    
        public virtual System.DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get;
            set;
        }
    
        public virtual string Comment
        {
            get;
            set;
        }
    
        public virtual string FirstName
        {
            get;
            set;
        }
    
        public virtual string LastName
        {
            get;
            set;
        }
    
        public virtual Nullable<int> TimeZone
        {
            get;
            set;
        }
    
        public virtual byte Status
        {
            get;
            set;
        }
    
        public virtual int ApplicationId
        {
            get { return _applicationId; }
            set
            {
                if (_applicationId != value)
                {
                    if (Application != null && Application.Id != value)
                    {
                        Application = null;
                    }
                    _applicationId = value;
                }
            }
        }
        private int _applicationId;

        #endregion
        #region Navigation Properties
    
        public virtual Application Application
        {
            get { return _application; }
            set
            {
                if (!ReferenceEquals(_application, value))
                {
                    var previousValue = _application;
                    _application = value;
                    FixupApplication(previousValue);
                }
            }
        }
        private Application _application;
    
        public virtual ICollection<UserEventLink> UserEventLinks
        {
            get
            {
                if (_userEventLinks == null)
                {
                    var newCollection = new FixupCollection<UserEventLink>();
                    newCollection.CollectionChanged += FixupUserEventLinks;
                    _userEventLinks = newCollection;
                }
                return _userEventLinks;
            }
            set
            {
                if (!ReferenceEquals(_userEventLinks, value))
                {
                    var previousValue = _userEventLinks as FixupCollection<UserEventLink>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUserEventLinks;
                    }
                    _userEventLinks = value;
                    var newValue = value as FixupCollection<UserEventLink>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUserEventLinks;
                    }
                }
            }
        }
        private ICollection<UserEventLink> _userEventLinks;

        #endregion
        #region Association Fixup
    
        private void FixupApplication(Application previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (Application != null)
            {
                if (!Application.Users.Contains(this))
                {
                    Application.Users.Add(this);
                }
                if (ApplicationId != Application.Id)
                {
                    ApplicationId = Application.Id;
                }
            }
        }
    
        private void FixupUserEventLinks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UserEventLink item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UserEventLink item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }

        #endregion
    }
}
