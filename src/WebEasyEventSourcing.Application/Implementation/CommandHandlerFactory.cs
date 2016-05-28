﻿using System;
using System.Collections.Generic;
using WebEasyEventSourcing.Domain.Orders;
using WebEasyEventSourcing.Domain.ShoppingCart;
using WebEasyEventSourcing.EventSourcing.Exceptions;
using WebEasyEventSourcing.EventSourcing.Handlers;
using WebEasyEventSourcing.EventSourcing.Persistence;
using WebEasyEventSourcing.Messages;
using WebEasyEventSourcing.Messages.Orders;
using WebEasyEventSourcing.Messages.Store;

namespace WebEasyEventSourcing.Application.Implementation
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly Dictionary<Type, Func<IHandler>> handlerFactories = new Dictionary<Type, Func<IHandler>>();

        public CommandHandlerFactory(IEventStore eventStore)
        {
            Func<IRepository> newTransientRepo = () => new Repository(eventStore);

            this.RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartCommandHandler(newTransientRepo()),
                typeof(CreateNewCart), typeof(AddProductToCart), typeof(RemoveProductFromCart), typeof(EmptyCart), typeof(Checkout));

            this.RegisterHandlerFactoryWithTypes(
                () => new OrderCommandHandler(newTransientRepo()),
                typeof(PayForOrder), typeof(ConfirmShippingAddress), typeof(CompleteOrder));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach(var type in types)
            {
                this.handlerFactories.Add(type, handler);
            }
        }

        public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
        {
            if (this.handlerFactories.ContainsKey(typeof(TCommand)))
            {
                var handler = this.handlerFactories[typeof(TCommand)]() as ICommandHandler<TCommand>;
                if (handler != null)
                {
                    return handler;
                }
            }
            throw new NoCommandHandlerRegisteredException(typeof (TCommand));
        }
    }
}