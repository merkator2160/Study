using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;


namespace GameStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private IGameRepository _repository;



        public CartController(IGameRepository repo)
        {
            _repository = repo;
        }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public RedirectToRouteResult AddToCart(int gameId, string returnUrl)
        {
            Game game = _repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        {
            Game game = _repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
    }
}