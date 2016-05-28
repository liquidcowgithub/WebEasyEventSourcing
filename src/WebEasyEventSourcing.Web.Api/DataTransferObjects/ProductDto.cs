using System;

namespace WebEasyEventSourcing.Web.Api.DataTransferObjects
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}