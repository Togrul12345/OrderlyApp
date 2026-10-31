using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class Product : FullAuditableEntity<int>
    {
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true; 
        public Restaurant Restaurant { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
