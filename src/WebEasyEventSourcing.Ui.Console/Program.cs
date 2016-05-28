using WebEasyEventSourcing.Application;
using System;
using WebEasyEventSourcing.Messages.Store;
using WebEasyEventSourcing.Messages.Orders;

namespace WebEasyEventSourcing.Ui.Console
{
    class Program
    {
        private static readonly Guid clientId = Guid.NewGuid();
        private static readonly Guid cartId = Guid.NewGuid();

        static void Main(string[] args)
        {
            try
            {
                var app = Bootstrapper.Bootstrap();

                if(!app.ShoppingCartContext.HasCart(clientId))
                {
                    app.Send(new CreateNewCart(cartId, clientId));
                }

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 50));

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 10));

                var cartModel = app.ShoppingCartContext.GetCartById(cartId);

                app.Send(new Checkout(cartId));
                var hasCartBeenRemovedAfterCheckout = app.ShoppingCartContext.HasCart(clientId);

                var orderId = cartId;
                app.Send(new ConfirmShippingAddress(orderId, "My Home"));
                app.Send(new PayForOrder(orderId));

                
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
