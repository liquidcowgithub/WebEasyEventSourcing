using System;
using WebEasyEventSourcing.EventSourcing.Domain;

namespace WebEasyEventSourcing.Domain.Store
{
    [Serializable]
    public class CannotCheckoutEmptyCartException : DomainException
    {
    }
}