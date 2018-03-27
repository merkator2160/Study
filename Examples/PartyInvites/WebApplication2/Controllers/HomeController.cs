using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var firstVal = 10;
            var secondVal = 0;
            var result = firstVal / 2;

            //ViewBag.Message = "Отладка приложения ASP.NET MVC!";

            return View(result);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}