using System;
using System.Web.Http;
using WebEasyEventSourcing.Application;
using WebEasyEventSourcing.Messages.Store;
using WebEasyEventSourcing.Web.Api.DataTransferObjects;

namespace WebEasyEventSourcing.Web.Api.Controllers
{
    public class ShoppingCartsController : ApiController
    {
        private readonly IApplication app;

        public ShoppingCartsController(IApplication app)
        {
            this.app = app;
        }

        [Route("shoppingCarts/{cartId}")]
        public IHttpActionResult Get(Guid cartId)
        {
            var cart = app.ShoppingCartContext.GetCartById(cartId);

            return Ok(cart);
        }

        [Route("shoppingCart/")]
        public IHttpActionResult Post()
        {
            var mockClientId = Guid.NewGuid();
            var newCartId = Guid.NewGuid();

            app.Send(new CreateNewCart(newCartId, mockClientId));

            var cart = app.ShoppingCartContext.GetCartById(newCartId);

            return Ok(cart);
        }

        [Route("shoppingCarts/{cartId}/products")]
        public IHttpActionResult Post(Guid cartId, [FromBody] ProductDto product)
        {
            app.Send(new AddProductToCart(cartId, product.ProductId, product.Price));

            var cart = app.ShoppingCartContext.GetCartById(cartId);

            return Ok(cart);
        }


        [Route("shoppingCarts/{cartId}/checkout")]
        public IHttpActionResult Post(Guid cartId)
        {
            app.Send(new Checkout(cartId));

            return Ok();
        }

        [Route("shoppingCarts/{cartId}/order")]
        public IHttpActionResult GetOrder(Guid cartId)
        {
            var order = new OrderDto()
            {
                CartId = cartId,
                OrderId = cartId
            };

            return Ok(order);
        }

    }
}