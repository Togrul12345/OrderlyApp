using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Create
{
    public class ProductResult
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
