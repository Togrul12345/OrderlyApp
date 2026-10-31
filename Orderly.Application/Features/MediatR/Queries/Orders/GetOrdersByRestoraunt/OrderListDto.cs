using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Queries.Orders.GetOrdersByRestoraunt
{
    public class OrderListDto
    {
        public Guid Id { get; set; }
        public int RestaurantId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } 
        public List<OrderItemListDto> Items { get; set; } = new();
    }
    public class OrderItemListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!; 
        public int Quantity { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public decimal TotalPrice { get; set; } 
    }

   
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
