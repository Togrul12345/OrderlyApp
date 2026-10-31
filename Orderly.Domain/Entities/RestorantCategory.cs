using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class RestaurantCategory
    {
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
