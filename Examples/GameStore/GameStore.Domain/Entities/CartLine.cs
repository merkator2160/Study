using System;


namespace GameStore.Domain.Entities
{
    public class CartLine
    {
        public Game Game { get; set; }
        public Int32 Quantity { get; set; }
    }
}