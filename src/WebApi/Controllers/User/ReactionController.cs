using Application.CQRS.Reactions.Commands;
using Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController(ISender sender, IHttpContextAccessor context) 
        : BaseAuthenticationController(sender, context)
    {
        [HttpPost("reaction")]
        public async Task<IActionResult> CreateReactionAsync([FromBody] ReactionViewModel reaction)
        {
            var result = await sender.Send(new CreateReactionCommand(userId,reaction));
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updateReaction")]
        public async Task<IActionResult> UpdateReactionAsync(string ratingId)
        {
            var result = await sender.Send(new UpdateReactionCommand(userId,ratingId));
            if (result.Success)
            {
                return NoContent(); 
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteReaction")]
        public async Task<IActionResult> DeleteReactionAsync(string ratingId)
        {
            var result = await sender.Send(new DeleteReactionCommand(userId, ratingId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
