using Application.Common;
using Application.CQRS.Ratings.Commands;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(ISender sender, IHttpContextAccessor context) 
        : BaseAuthenticationController(sender,context)
    {
        [HttpPost("newRating")]
        public async Task<IActionResult> CreateRatingAsync([FromBody] CreateRatingViewModel rating)
        {
            var result = await sender.Send(new CreateRatingCommand(userId,rating));
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("ratings")]
        public async Task<ActionResult<PaginationEntity<RatingWithProductIdReponse>>> GetRatingsAsync(int PageNumber)
        {
            var result = await sender.Send(new GetRatingWithPaginationByUseQuery(userId,PageNumber));
            return Ok(result);
        }
        [HttpGet("rating")]
        public async Task<ActionResult<RatingWithProductIdReponse?>> GetRatingAsync(string orderDetail)
        {
            var result = await sender.Send(new GetRatingByOrderDetailIdForUserQuery(userId, orderDetail));
            return Ok(result);
        }
    }
}
