using MediatR;
using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Queries.Products.GetTopSellingProducts
{
    public record GetTopSellingProductsQuery(int RestaurantId) : IRequest<Result<List<TopSellingProductDto>>>;
}

