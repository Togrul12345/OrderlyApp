using Orderly.Domain.Common;
using Orderly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Entities
{
    public class Order : FullAuditableEntity<Guid>
    {
        public int RestaurantId { get; set; }
        public decimal TotalPrice { get;private set; }
        public void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(item => item.TotalPrice);
        }
        public int OrderStatusId { get; set; } = (int)OrderStatusEnum.Pending;
        public Result ChangeStatus(OrderStatusEnum newStatus)
        {
          
            bool isValidTransition = (OrderStatusEnum)OrderStatusId switch
            {
                OrderStatusEnum.Pending => newStatus == OrderStatusEnum.Preparing,
                OrderStatusEnum.Preparing => newStatus == OrderStatusEnum.Ready,
                OrderStatusEnum.Ready => newStatus == OrderStatusEnum.Completed,
                OrderStatusEnum.Completed => false, 
                _ => false
            };

            if (!isValidTransition)
            {
                return Result.Failure($"Sifariş statusu '{(OrderStatusEnum)OrderStatusId}' durumundan '{newStatus}' durumuna keçə bilməz.");
            }

            OrderStatusId = (int)newStatus;
            return Result.Success();
        }
        public OrderStatus OrderStatus { get; set; }
        public Restaurant Restaurant { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
