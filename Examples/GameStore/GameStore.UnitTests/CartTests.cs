using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace GameStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация - создание нескольких тестовых игр
            var game1 = new Game { GameId = 1, Name = "Игра1" };
            var game2 = new Game { GameId = 2, Name = "Игра2" };

            // Организация - создание корзины
            var cart = new Cart();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            var results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Game, game1);
            Assert.AreEqual(results[1].Game, game2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Организация - создание нескольких тестовых игр
            var game1 = new Game { GameId = 1, Name = "Игра1" };
            var game2 = new Game { GameId = 2, Name = "Игра2" };

            // Организация - создание корзины
            var cart = new Cart();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            var results = cart.Lines.OrderBy(c => c.Game.GameId).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых игр
            var game1 = new Game { GameId = 1, Name = "Игра1" };
            var game2 = new Game { GameId = 2, Name = "Игра2" };
            var game3 = new Game { GameId = 3, Name = "Игра3" };

            // Организация - создание корзины
            var cart = new Cart();

            // Организация - добавление нескольких игр в корзину
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            // Действие
            cart.RemoveLine(game2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Game == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация - создание нескольких тестовых игр
            var game1 = new Game { GameId = 1, Name = "Игра1", Price = 100 };
            var game2 = new Game { GameId = 2, Name = "Игра2", Price = 55 };

            // Организация - создание корзины
            var cart = new Cart();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            var result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация - создание нескольких тестовых игр
            var game1 = new Game { GameId = 1, Name = "Игра1", Price = 100 };
            var game2 = new Game { GameId = 2, Name = "Игра2", Price = 55 };

            // Организация - создание корзины
            var cart = new Cart();

            // Действие
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }

}
