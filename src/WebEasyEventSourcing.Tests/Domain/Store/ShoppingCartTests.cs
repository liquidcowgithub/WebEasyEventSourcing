using System;
using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.Domain.Store;
using WebEasyEventSourcing.Messages.Orders;
using WebEasyEventSourcing.Messages.Store;
using WebEasyEventSourcing.Tests.Domain.Helpers;
using NUnit.Framework;

namespace WebEasyEventSourcing.Tests.Domain.Store
{
    internal class ShoppingCartTests : Spesification
    {
        private const decimal ProductPrice = 10;
        private readonly Guid cartId = Guid.NewGuid();
        private readonly Guid clientId = Guid.NewGuid();
        private readonly Guid productId = Guid.NewGuid();

        [Test]
        public void CartCreation()
        {
            When(new CreateNewCart(cartId, clientId));
            Then(new CartCreated(cartId, clientId));
        }

        [Test]
        public void AddingAProduct()
        {
            GivenNewCart();
            When(new AddProductToCart(cartId, productId, ProductPrice));
            Then(new ProductAddedToCart(cartId, productId, ProductPrice));
        }

        [Test]
        public void RemovingAProduct()
        {
            GivenNewCart();
            AndAddedProduct();
            When(new RemoveProductFromCart(cartId, productId));
            Then(new ProductRemovedFromCart(cartId, productId));
        }

        [Test]
        public void EmptyCart()
        {
            GivenNewCart();
            AndAddedProduct();
            And<ShoppingCart, ProductAddedToCart>(cartId, new ProductAddedToCart(cartId, Guid.NewGuid(), ProductPrice));
            When(new EmptyCart(cartId));
            Then(new CartEmptied(cartId));
        }

        [Test]
        public void CheckingOut()
        {
            GivenNewCart();
            AndAddedProduct();
            When(new Checkout(cartId));
            Then(
                new CartCheckedOut(cartId),
                new OrderCreated(cartId, clientId, new[]
                {
                    new OrderItem(productId, ProductPrice)
                })
                );
        }

        [Test]
        public void CannotCheckoutEmptyCart()
        {
            GivenNewCart();
            ThrowsWhen<CannotCheckoutEmptyCartException, Checkout>(new Checkout(cartId));
        }

        [Test]
        public void CannotAddToCheckedoutCart()
        {
            GivenCheckedOutCart();
            ThrowsWhen<CartAlreadyCheckedOutException, AddProductToCart>(new AddProductToCart(cartId, productId,
                ProductPrice));
        }

        [Test]
        public void CannotRemoveFromCheckedoutCart()
        {
            GivenCheckedOutCart();
            ThrowsWhen<CartAlreadyCheckedOutException, RemoveProductFromCart>(new RemoveProductFromCart(cartId,
                productId));
        }

        [Test]
        public void CannotEmptyCheckedoutCart()
        {
            GivenCheckedOutCart();
            ThrowsWhen<CartAlreadyCheckedOutException, EmptyCart>(new EmptyCart(cartId));
        }

        private void GivenNewCart()
        {
            Given<ShoppingCart, CartCreated>(cartId, new CartCreated(cartId, clientId));
        }

        private void AndAddedProduct()
        {
            And<ShoppingCart, ProductAddedToCart>(cartId, new ProductAddedToCart(cartId, productId, ProductPrice));
        }

        private void GivenCheckedOutCart()
        {
            GivenNewCart();
            AndAddedProduct();
            And<ShoppingCart, CartCheckedOut>(cartId, new CartCheckedOut(cartId));
        }
    }
}