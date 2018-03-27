using GameStore.Domain.Abstract;
using GameStore.WebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        // GET: /Game/
        private IGameRepository _repository;



        public GameController(IGameRepository repository)
        {
            _repository = repository;
        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Int32 PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        private Int32 _pageSize = 3;


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public ViewResult List(String category, Int32 page = 1)
        {
            var model = new GamesListViewModel()
            {
                Games = _repository.Games
                .Where(p => category == null || p.Category == category)
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Games.Count() : _repository.Games.Where(game => game.Category == category).Count()
                }
            };
            return View(model);
        }
    }
}