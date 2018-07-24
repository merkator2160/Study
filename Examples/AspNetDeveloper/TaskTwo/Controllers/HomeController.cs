using System.Web.Mvc;
using TaskTwo.Models;
using TaskTwo.Services;

namespace TaskTwo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ViewModel());
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            var service = new PaymentService(new InputParams
            {
                CountPeople = model.PlayersNumber,
                InitAmount = model.StartAmount
            });
            var result = service.Start().Result;
            ViewBag.Result = result;
            return View(new ViewModel());
        }
    }
}