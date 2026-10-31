using MediatR;
using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Update
{
    public record UpdateProductCommand(
        int Id,
        int RestaurantId,
        int CategoryId,
        string Name,
        decimal Price,
        bool IsAvailable
    ) : IRequest<Result<bool>>;
}
