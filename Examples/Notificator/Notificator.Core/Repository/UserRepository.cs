using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Notificator.Core.Model;
using Notificator.Core.Security;

namespace Notificator.Core.Repository
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository(NotificatorEntities context) : base(context)
        {
        }

        public int GetCurrentUserId()
        {
            var user = GetCurrentUser();
            return user == null ? -1 : user.Id;
        }

        public User GetCurrentUser()
        {
            //TODO: remove Provider to EFMembershipProvider cast
            var user = Membership.GetUser();
            var provider = Membership.Provider as EFMembershipProvider;
            if (provider == null || user == null)
                return null;
            
            return provider.GetDBUser(context, user.UserName);
        }
    }
}
