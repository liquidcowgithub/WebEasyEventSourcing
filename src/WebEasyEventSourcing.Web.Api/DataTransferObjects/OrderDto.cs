using System;

namespace WebEasyEventSourcing.Web.Api.DataTransferObjects
{
    public class OrderDto
    {
        public Guid CartId { get; set; }
        public Guid OrderId { get; set; }
    }
}