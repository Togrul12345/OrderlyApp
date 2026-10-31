using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Queries.Products.GetTopSellingProducts
{
    public class TopSellingProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int TotalSoldQuantity { get; set; } 
        public decimal TotalRevenue { get; set; }  
    }
}
