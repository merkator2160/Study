using System;
using System.Linq;
using System.Web.Mvc;
using Notificator.Core.Model;
using Notificator.Site.Models;
using Notificator.Core.Repository;

namespace Notificator.Site.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private NotificatorEntities context = new NotificatorEntities();
        private UserRepository userRepository;
        private EventRepository eventRepository;

        public EventController()
        {
            userRepository = new UserRepository(context);
            eventRepository = new EventRepository(context);
        }

        public ActionResult Index()
        {
            var userId =  userRepository.GetCurrentUserId();
            var events = context.GetAllEvents(userId).ToList();
            return View(events);
        }

        public ActionResult Create()
        {
            return View(new EventViewModel());
        }

        [HttpPost]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event entity = new Event();
                entity.Name = eventViewModel.Name;
                context.Events.AddObject(entity);
                context.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        public ActionResult Join(int id)
        {
            var @event = eventRepository.GetEventById(id);
            return View(new EventViewModel(@event));
        }

        [HttpPost]
        public ActionResult Join(int id, FormCollection collection)
        {
            var @event = eventRepository.GetEventById(id);
            UserEventLink link = new UserEventLink();
            link.UserId = userRepository.GetCurrentUserId();
            link.EventId = @event.Id;
            link.CreatedDate = DateTime.UtcNow;
            context.UserEventLinks.AddObject(link);
            context.SaveChanges();
            return RedirectToAction("Index");
        } 

        public ActionResult MyEvents()
        {
            var userId = userRepository.GetCurrentUserId();
            var events = context.GetUserEvents(userId).ToList();
            return View(events);
        }

        public ActionResult Notifications()
        {
            var userId = userRepository.GetCurrentUserId();
            var notifications = context.GetNotificationLog(userId, -1, 100).ToList();
            var topNotification = notifications.FirstOrDefault();
            ViewBag.MaxLinkId = topNotification != null ? topNotification.LinkId : -1;
            return View(notifications);
        }

        public ActionResult NotificationsUpdate(int id)
        {
            var userId = userRepository.GetCurrentUserId();
            var notifications = context.GetNotificationLog(userId, id, 100).ToList();
            var topNotification = notifications.FirstOrDefault();
            var data = new {
                lastId = id,
                count = notifications.Count,
                maxId = topNotification == null ? id : topNotification.LinkId,
                items = notifications
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
