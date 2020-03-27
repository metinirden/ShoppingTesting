using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingTesting.API.Startup;
using ShoppingTesting.Application.ShoppingCarts.Commands;
using ShoppingTesting.Application.ShoppingCarts.Dtos;
using ShoppingTesting.Application.ShoppingCarts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingTesting.API.Controllers
{
    [
        ApiController,
        Route("api/[controller]"),
        Produces("application/json")
    ]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator) => _mediator = mediator;

        [HttpGet("products")]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Products()
        {
            return Ok(await _mediator.Send(new GetProductsQuery()));
        }

        [HttpGet("shopping-cart/{id:guid}")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DomainProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShoppingCart(Guid id)
        {
            return Ok(await _mediator.Send(new GetShoppingCartQuery {ShoppingCartId = id}));
        }

        [HttpGet("shopping-carts")]
        [ProducesResponseType(typeof(List<ShoppingCartDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShoppingCarts([FromQuery] GetShoppingCartsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DomainProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCartItem(AddCartItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}