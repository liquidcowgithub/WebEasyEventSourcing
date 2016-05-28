using System;
using WebEasyEventSourcing.Domain.ShoppingCart.ReadModels;

namespace WebEasyEventSourcing.Domain.ShoppingCart
{
    public interface IShoppingCartContext
    {
        ShoppingCartReadModel GetCartById(Guid id);
        bool HasCart(Guid clientId);
        void RemoveCart(Guid cartId);
        void SaveCart(ShoppingCartReadModel cart);
    }
}