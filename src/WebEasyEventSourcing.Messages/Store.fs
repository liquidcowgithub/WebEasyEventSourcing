﻿namespace WebEasyEventSourcing.Messages.Store

open System
open WebEasyEventSourcing.Messages

type CreateNewCart = {
    CartId: Guid;
    ClientId: Guid;
} with interface ICommand

type CartCreated = {
    CartId: Guid;
    ClientId: Guid;
} with interface IEvent

type AddProductToCart = {
    CartId: Guid;
    ProductId: Guid;
    Price: Decimal;
} with interface ICommand

type ProductAddedToCart = {
    CartId: Guid;
    ProductId: Guid;
    Price: Decimal;
} with interface IEvent

type RemoveProductFromCart = {
    CartId: Guid;
    ProductId: Guid;
} with interface ICommand

type ProductRemovedFromCart = {
    CartId: Guid;
    ProductId: Guid;
} with interface IEvent

type EmptyCart = {
    CartId: Guid;
} with interface ICommand

type CartEmptied = {
     CartId: Guid;
} with interface IEvent

type Checkout = {
    CartId: Guid;
} with interface ICommand

type CartCheckedOut = {
     CartId: Guid;
} with interface IEvent