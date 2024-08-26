using Application.CQRS.Promotions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //[HttpGet("test")]
        //public async Task<ActionResult<IEnumerable<decimal>>> Test(ISender sender, string productId)
        //{
        //    var promotions = await sender.Send(new GetJustPromotionForProductQuery(productId));
        //    return Ok(promotions);
        //}
    }
}
