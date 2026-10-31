using MediatR;
using Orderly.Application.Repositories;
using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Orderly.Application.Features.MediatR.Queries.Orders.GetOrdersByRestoraunt
{
    public class GetOrdersByRestaurantQueryHandler : IRequestHandler<GetOrdersByRestaurantQuery, Result<PaginatedResult<OrderListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
    
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByRestaurantQueryHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<Result<PaginatedResult<OrderListDto>>> Handle(GetOrdersByRestaurantQuery request, CancellationToken cancellationToken)
        {
            
            var query = _orderRepository.GetQueryable() 
                .AsNoTracking()
                .Where(o => o.RestaurantId == request.RestaurantId);

        
            if (request.StatusId.HasValue)
            {
                query = query.Where(o => o.OrderStatusId == request.StatusId.Value);
            }

          
            var totalCount = await query.CountAsync(cancellationToken);

  
            var orders = await query
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) 
                .Include(o => o.OrderStatus) 
                .OrderByDescending(o => o.CreatedAt) 
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(o => new OrderListDto
                {
                    Id = o.Id,
                    RestaurantId = o.RestaurantId,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus.Name, 
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt,
                    Items = o.OrderItems.Select(oi => new OrderItemListDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        UnitPriceSnapshot = oi.UnitPriceSnapshot,
                        TotalPrice = oi.Quantity * oi.UnitPriceSnapshot
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var result = new PaginatedResult<OrderListDto>
            {
                Items = orders,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Result<PaginatedResult<OrderListDto>>.Success(result);
        }
    }
}
