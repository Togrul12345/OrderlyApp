using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Orders.Create
{
    public record CreateOrderItemDto(
        int ProductId,
        int Quantity
    );
}
