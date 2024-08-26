using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        [HttpGet("promotions")]
        public async Task<ActionResult<IEnumerable<PromotionDiscountReponse>>> GetPromotionsAsync(ISender sender)
        {
            var promotions = await sender.Send(new GetListPromotionQuery());
            return Ok(promotions);
        }
    }
}
