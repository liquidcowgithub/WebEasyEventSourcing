using System;
using System.Collections.Generic;
using System.Linq;
using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.Domain.ShoppingCart.ReadModels;

namespace WebEasyEventSourcing.Data.DomainContexts
{
    public class ShoppingCartContext : IShoppingCartContext
    {
        private readonly Dictionary<Guid, ShoppingCartReadModel> carts = new Dictionary<Guid, ShoppingCartReadModel>();

        public ShoppingCartReadModel GetCartById(Guid id)
        {
            return this.carts.ContainsKey(id) ? this.carts[id] : null;
        }

        public bool HasCart(Guid clientId)
        {
            return this.carts.Values.Any(x => x.ClientId == clientId);
        }

        public void SaveCart(ShoppingCartReadModel cart)
        {
            if (!this.carts.ContainsKey(cart.Id))
            {
                this.carts.Add(cart.Id, cart);
            }
            else
            {
                this.carts[cart.Id] = cart;
            }
        }

        public void RemoveCart(Guid cartId)
        {
            if (this.carts.ContainsKey(cartId))
            {
                this.carts.Remove(cartId);
            }
        }
    }
}