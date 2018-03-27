using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notificator.Core.Model;

namespace Notificator.Core.Security
{
    public static class UserMapper
    {

        public static MembershipUser Map(string pname, User user)
        {

            return new MembershipUser(pname, user.Username, user.Id, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved,
                                      user.Status == 2, user.CreateOn, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate,
                                      user.LastLockoutDate, user.FirstName, user.LastName, user.TimeZone.GetValueOrDefault(0));
        }

    }
}
