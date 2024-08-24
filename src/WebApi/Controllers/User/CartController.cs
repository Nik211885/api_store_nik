using Application.CQRS.OrderDetails.Command;
using Application.CQRS.OrderDetails.Queries;
using Application.DTOs.Reponse;
using Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrderAsync(ISender sender, [FromBody] CreateOrderViewModel order)
        {
            var userId = User.Claims.First(x=>x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new CreateOrderDetailCommand(userId, order));
            if(result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updateOrder")]
        public async Task<IActionResult> UpdateOrderAsync(ISender sender, [FromBody] UpdateOrderViewModel order)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new UpdateOrderDetailCommand(userId, order));
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("deleteOrder")]
        public async Task<IActionResult> DeletedOrderAsync(ISender sender, [Required] string orderId)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new DeleteOrderDetailCommand(userId, orderId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("orders")]
        public async Task<ActionResult<OrderHasNotCheckOutReponse>> GetOrdersAsync(ISender sender)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var orders = await sender.Send(new GetOrderForUserHasNotCheckOutQuery(userId));
            return Ok(orders);
        }
    }
}
