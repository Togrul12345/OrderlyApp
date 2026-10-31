using MediatR;
using Orderly.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Orders.Create
{
    public record CreateOrderCommand(
        int RestaurantId,
        List<CreateOrderItemDto> Items
    ) : IRequest<Result<OrderResponseDto>>;
}
