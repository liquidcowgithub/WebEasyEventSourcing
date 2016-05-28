﻿namespace WebEasyEventSourcing.Messages.Orders

open System
open WebEasyEventSourcing.Messages

type OrderItem = {
    ProductId: Guid;
    Price: Decimal;
}

type OrderCreated = {
    OrderId: Guid;
    ClientId: Guid;
    Items: OrderItem[];
} with interface IEvent

type PayForOrder = {
    OrderId: Guid;
} with interface ICommand

type PaymentReceived = {
    OrderId: Guid;
} with interface IEvent

type ConfirmShippingAddress = {
    OrderId: Guid;
    Address: string; //Because I am Lazy
} with interface ICommand

type ShippingAddressConfirmed = {
    OrderId: Guid;
    Address: string; 
} with interface IEvent

type CompleteOrder = {
    OrderId: Guid;
} with interface ICommand

type OrderCompleted = {
    OrderId: Guid;
} with interface IEvent