using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Orders.Create
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public int RestaurantId { get; set; }
        public int OrderStatusId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
