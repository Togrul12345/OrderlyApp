using MediatR;
using Orderly.Domain.Common;
using Orderly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Orders.ChangeOrderStatus
{
    public record ChangeOrderStatusCommand(Guid OrderId, OrderStatusEnum NewStatus) : IRequest<Result>;
}
