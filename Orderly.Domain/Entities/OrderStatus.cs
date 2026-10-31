using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class OrderStatus : BaseEntity<int> 
    {
        public string Name { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
