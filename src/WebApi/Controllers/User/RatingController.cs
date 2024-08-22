using Application.Common;
using Application.CQRS.Ratings.Commands;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
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
    public class RatingController : ControllerBase
    {
        [HttpPost("newRating")]
        public async Task<IActionResult> CreateRatingAsync(ISender sender, [FromBody] CreateRatingViewModel rating)
        {
            var userId = User.Claims.First(x=>x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new CreateRatingCommand(userId,rating));
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("ratings")]
        public async Task<ActionResult<PaginationEntity<RatingReponse>>> GetRatingsAsync(ISender sender, int PageNumber)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new GetRatingWithPaginationByUseQuery(userId,PageNumber));
            return Ok(result);
        }
    }
}
