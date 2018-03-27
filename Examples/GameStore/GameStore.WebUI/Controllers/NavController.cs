using GameStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository _repository;



        public NavController(IGameRepository repo)
        {
            _repository = repo;
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public PartialViewResult Menu(String category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<String> categories = _repository.Games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}