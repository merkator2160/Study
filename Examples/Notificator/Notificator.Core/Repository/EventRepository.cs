using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notificator.Core.Model;

namespace Notificator.Core.Repository
{
    public class EventRepository : RepositoryBase
    {
        public EventRepository(NotificatorEntities context) : base(context)
        {
        }

        public Event GetEventById(int id)
        {
            return context.GetEventById(id).FirstOrDefault();
        }
    }
}
