using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.Messages;

namespace WebEasyEventSourcing.Application
{
    public interface IApplication
    {
        void Send<TCommand>(TCommand cmd) where TCommand : ICommand;
        IShoppingCartContext ShoppingCartContext { get; }
    }
}