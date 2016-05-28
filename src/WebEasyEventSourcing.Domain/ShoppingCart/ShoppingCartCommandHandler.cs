using System;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages.Store;

namespace WebEasyEventSourcing.Domain.ShoppingCart
{
    public class ShoppingCartCommandHandler
        : ICommandHandler<CreateNewCart>
            , ICommandHandler<AddProductToCart>
            , ICommandHandler<RemoveProductFromCart>
            , ICommandHandler<EmptyCart>
            , ICommandHandler<Checkout>
    {
        private readonly IRepository repo;

        public ShoppingCartCommandHandler(IRepository repo)
        {
            this.repo = repo;
        }

        public void Handle(AddProductToCart cmd)
        {
            Execute(cmd.CartId, cart => cart.AddProduct(cmd.ProductId, cmd.Price));
        }

        public void Handle(Checkout cmd)
        {
            var cart = repo.GetById<ShoppingCart>(cmd.CartId);
            var order = cart.Checkout();
            repo.Save(cart, order);
        }

        public void Handle(CreateNewCart cmd)
        {
            repo.Save(ShoppingCart.Create(cmd.CartId, cmd.ClientId));
        }

        public void Handle(EmptyCart cmd)
        {
            Execute(cmd.CartId, cart => cart.Empty());
        }

        public void Handle(RemoveProductFromCart cmd)
        {
            Execute(cmd.CartId, cart => cart.RemoveProduct(cmd.ProductId));
        }

        private void Execute(Guid id, Action<ShoppingCart> action)
        {
            var cart = repo.GetById<ShoppingCart>(id);
            action(cart);
            repo.Save(cart);
        }
    }
}