using Application.CQRS.Reactions.Commands;
using Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReactionController : ControllerBase
    {
        [HttpPost("reaction")]
        public async Task<IActionResult> CreateReactionAsync(ISender sender, [FromBody] ReactionViewModel reaction)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new CreateReactionCommand(userId,reaction));
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updateReaction")]
        public async Task<IActionResult> UpdateReactionAsync(ISender sender, string ratingId)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new UpdateReactionCommand(userId,ratingId));
            if (result.Success)
            {
                return NoContent(); 
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteReaction")]
        public async Task<IActionResult> DeleteReactionAsync(ISender sender, string ratingId)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new DeleteReactionCommand(userId, ratingId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
