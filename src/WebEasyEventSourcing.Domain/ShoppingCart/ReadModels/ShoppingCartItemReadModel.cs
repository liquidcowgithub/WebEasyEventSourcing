using System;

namespace WebEasyEventSourcing.Domain.ShoppingCart.ReadModels
{
    public class ShoppingCartItemReadModel
    {
        public Guid ProductId { get; set; }
        public Decimal Price { get; set; }
    }
}