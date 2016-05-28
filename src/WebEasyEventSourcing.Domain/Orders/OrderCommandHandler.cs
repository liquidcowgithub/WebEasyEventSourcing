using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages.Orders;
using WebEasyEventSourcing.Messages.Shipping;

namespace WebEasyEventSourcing.Domain.Orders
{
    public class OrderCommandHandler
        : ICommandHandler<PayForOrder>
          , ICommandHandler<ConfirmShippingAddress>
          , ICommandHandler<CompleteOrder>
    {
        private readonly IRepository repository;

        public OrderCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(PayForOrder cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.Pay();
            this.repository.Save(order);
        }

        public void Handle(ConfirmShippingAddress cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.ProvideShippingAddress(cmd.Address);
            this.repository.Save(order);
        }

        public void Handle(CompleteOrder cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.CompleteOrder();
            this.repository.Save(order);
        }
    }
}