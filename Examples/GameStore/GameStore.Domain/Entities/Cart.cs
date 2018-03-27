using System;
using System.Collections.Generic;
using System.Linq;


namespace GameStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new List<CartLine>();



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public IEnumerable<CartLine> Lines
        {
            get { return _lineCollection; }
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void AddItem(Game game, Int32 quantity)
        {
            CartLine line = _lineCollection
                .Where(g => g.Game.GameId == game.GameId)
                .FirstOrDefault();

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Game game)
        {
            _lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }
        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Game.Price * e.Quantity);
        }
        public void Clear()
        {
            _lineCollection.Clear();
        }
    }
}
