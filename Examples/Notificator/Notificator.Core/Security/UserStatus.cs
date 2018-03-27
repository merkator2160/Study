using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notificator.Core.Security
{
    public enum UserStatus : byte
    {
        Approved = 1,
        Locked = 2,
        Expire = 3
    }

}
