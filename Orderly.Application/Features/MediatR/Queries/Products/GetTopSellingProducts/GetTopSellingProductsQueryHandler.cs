using MediatR;
using Microsoft.EntityFrameworkCore;
using Orderly.Application.Repositories;
using Orderly.Domain.Common;
using Orderly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Queries.Products.GetTopSellingProducts
{
    public class GetTopSellingProductsQueryHandler : IRequestHandler<GetTopSellingProductsQuery, Result<List<TopSellingProductDto>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetTopSellingProductsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<TopSellingProductDto>>> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _orderRepository.GetQueryable()
                .AsNoTracking()
                .Where(o => o.RestaurantId == request.RestaurantId
                         && o.OrderStatusId != (int)OrderStatusEnum.Cancelled);


            var topProducts = await query
                .SelectMany(o => o.OrderItems)
                .Where(oi => oi.Product != null)
                .GroupBy(oi => new { oi.ProductId, oi.Product.Name })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    TotalSoldQuantity = g.Sum(oi => oi.Quantity), 
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPriceSnapshot) 
                })
                .OrderByDescending(x => x.TotalSoldQuantity) 
                .Take(5) 
                .ToListAsync(cancellationToken);

            return Result<List<TopSellingProductDto>>.Success(topProducts);
        }
    }
}
