using Autofac;
using WebEasyEventSourcing.Data.DomainContexts;
using WebEasyEventSourcing.Domain.ShoppingCart;

namespace WebEasyEventSourcing.Application.AutoFacModules
{
    public class ContextsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShoppingCartContext>().As<IShoppingCartContext>().SingleInstance();
        }
    }
}