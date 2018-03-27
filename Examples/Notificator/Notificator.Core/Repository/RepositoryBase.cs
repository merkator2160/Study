using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notificator.Core.Model;

namespace Notificator.Core.Repository
{
    public abstract class RepositoryBase
    {
        protected NotificatorEntities context;

        protected RepositoryBase(NotificatorEntities context)
        {
            this.context = context;
        }
    }
}
