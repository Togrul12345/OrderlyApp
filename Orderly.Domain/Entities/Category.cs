using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class Category : FullAuditableEntity<int>
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
