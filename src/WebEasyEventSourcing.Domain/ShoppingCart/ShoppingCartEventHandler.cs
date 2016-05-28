using System.Linq;
using WebEasyEventSourcing.Domain.ShoppingCart.ReadModels;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.Messages.Store;

namespace WebEasyEventSourcing.Domain.ShoppingCart
{
    public class ShoppingCartEventHandler 
        : IEventHandler<CartCreated>
        , IEventHandler<ProductAddedToCart>
        , IEventHandler<ProductRemovedFromCart>
        , IEventHandler<CartEmptied>
        , IEventHandler<CartCheckedOut>
    {
        private readonly IShoppingCartContext context;

        public ShoppingCartEventHandler(IShoppingCartContext context)
        {
            this.context = context;
        }

        public void Handle(CartCreated evt)
        {
            var newCart = new ShoppingCartReadModel
                              {
                                  ClientId = evt.ClientId,
                                  Id = evt.CartId
                              };
            context.SaveCart(newCart);
        }

        public void Handle(ProductAddedToCart evt)
        {
            var cart = context.GetCartById(evt.CartId);
            var product = cart.Items.FirstOrDefault(x => x.ProductId == evt.ProductId);
            if (product != null)
            {
                product.Price = evt.Price;
            }
            else
            {
                cart.Items.Add(new ShoppingCartItemReadModel
                                   {
                                       Price = evt.Price,
                                       ProductId = evt.ProductId
                                   });
            }
            context.SaveCart(cart);
        }

        public void Handle(ProductRemovedFromCart evt)
        {
            var cart = context.GetCartById(evt.CartId);
            cart.Items.RemoveAll(x => x.ProductId == evt.ProductId);
            context.SaveCart(cart);
        }

        public void Handle(CartEmptied evt)
        {
            var cart = context.GetCartById(evt.CartId);
            cart.Items.Clear();
            context.SaveCart(cart);
        }

        public void Handle(CartCheckedOut evt)
        {
            context.RemoveCart(evt.CartId);
        }
    }
}