using Application.CQRS.OrderDetails.Command;
using Application.CQRS.OrderDetails.Queries;
using Application.DTOs.Reponse;
using Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ISender sender, IHttpContextAccessor context) 
        : BaseAuthenticationController(sender,context)
    {
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderViewModel order)
        {
            var result = await sender.Send(new CreateOrderDetailCommand(userId, order));
            if(result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updateOrder")]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderViewModel order)
        {
            var result = await sender.Send(new UpdateOrderDetailCommand(userId, order));
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteOrder")]
        public async Task<IActionResult> DeletedOrderAsync([Required] string orderId)
        {
            var result = await sender.Send(new DeleteOrderDetailCommand(userId, orderId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("orders")]
        public async Task<ActionResult<OrderHasNotCheckOutReponse>> GetOrdersAsync()
        {
            var orders = await sender.Send(new GetOrderForUserHasNotCheckOutQuery(userId));
            return Ok(orders);
        }
    }
}
