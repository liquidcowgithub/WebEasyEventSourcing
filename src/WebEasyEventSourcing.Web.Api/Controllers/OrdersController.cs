using System;
using System.Web.Http;
using WebEasyEventSourcing.Application;
using WebEasyEventSourcing.Messages.Orders;
using WebEasyEventSourcing.Web.Api.DataTransferObjects;

namespace WebEasyEventSourcing.Web.Api.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IApplication app;

        public OrdersController(IApplication app)
        {
            this.app = app;
        }

        [Route("orders/{orderId}/shippingAddress")]
        public IHttpActionResult Post(Guid orderId, [FromBody] AddressDto address)
        {
            app.Send(new ConfirmShippingAddress(orderId, address.Address));
            return Ok();
        }

        [Route("orders/{orderId}/payment")]
        public IHttpActionResult Post(Guid orderId)
        {
            app.Send(new PayForOrder(orderId));
            return Ok();
        }

    }
}