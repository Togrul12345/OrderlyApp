using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class OrderItem : FullAuditableEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPriceSnapshot * Quantity;
        public Order Order { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
