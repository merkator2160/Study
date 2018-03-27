using System.Web.Mvc;

namespace CinemaSchedule.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ScheduleEditor()
        {
            return View();
        }
    }
}