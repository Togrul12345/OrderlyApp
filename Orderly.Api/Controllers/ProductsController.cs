using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orderly.Application.Features.MediatR.Commands.Products.Create;
using Orderly.Application.Features.MediatR.Commands.Products.Update;
using Orderly.Application.Features.MediatR.Queries.Products.GetTopSellingProducts;

namespace Orderly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var result =await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetTopSellingProductsByRestourantId")]
        public async Task<IActionResult> Get([FromQuery]GetTopSellingProductsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
