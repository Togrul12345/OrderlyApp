using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orderly.Application.Features.MediatR.Commands.Orders.ChangeOrderStatus;
using Orderly.Application.Features.MediatR.Commands.Orders.Create;
using Orderly.Application.Features.MediatR.Queries.Orders.GetOrdersByRestoraunt;

namespace Orderly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var result =await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeOrderStatusCommand command)
        {
            var result =await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetByRestoraunt")]
        public async Task<IActionResult> Get([FromQuery]GetOrdersByRestaurantQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
