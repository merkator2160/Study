using System.Web.Mvc;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {
        // ACTIONS ////////////////////////////////////////////////////////////////////////////////
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
