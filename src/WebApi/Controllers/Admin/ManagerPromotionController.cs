using Application.Common;
using Application.CQRS.ProductPromotion.Commands;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Promotions.Queries;
using Application.DTOs.Reponse;
using Application.DTOs.Request;
using ApplicationCore.Entities.Products;
using ApplicationCore.ValueObject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.Admin))]
    public class ManagerPromotionController(ISender sender, IHttpContextAccessor context) 
        : BaseAuthenticationController(sender,context)
    {
        [HttpGet("promotions")]
        public async Task<ActionResult<PaginationEntity<PromotionDiscountReponse>>> GetPromotionsAsync()
        {
            var promotions = await sender.Send(new GetListPromotionManagerByUserQuery(userId));
            return Ok(promotions);
        }
        [HttpGet("promotion")]
        public async Task<ActionResult<dynamic>> GetPromotionByIdAsync(string PromotionId)
        {
            var promotion = await sender.Send(new GetPromotionByIdManagerByUserQuery(PromotionId, userId));
            return Ok(promotion);
        }
        [HttpPost("createPromotion")]
        public async Task<IActionResult> CreatePromotionAsync([FromBody] PromotionViewModel promotion)
        {
            var result = await sender.Send(new CreatePromotionCommand(userId,promotion));
            if(result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updatePutPromotion")]
        public async Task<IActionResult> UpdatePutPromotionAsync(string promotionId, [FromBody] PromotionViewModel promotion)
        {
            var result = await sender.Send(new UpdatePutPromotionCommand(userId, promotionId, promotion));
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deletePromotion")]
        public async Task<IActionResult> DeletePromotionAsync(string promotionId)
        {
            var result = await sender.Send(new DeletePromotionCommand(userId,promotionId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPatch("updatePatchPromotion")]
        public async Task<IActionResult> UpdatePatchPromotionAsync(string promotionId, JsonPatchDocument<PromotionDiscount> PatchDoc)
        {
            var result = await sender.Send(new UpdatePatchPromotionCommand(userId, promotionId, PatchDoc));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("addPromotionForProduct")]
        public async Task<IActionResult> AddPromotionForProductAsync(string promotionId, string productId)
        {
            var result = await sender.Send(new CreateProductPromotionCommand(userId, productId, promotionId));
            if(result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("removePromotionForProduct")]
        public async Task<IActionResult> RemovePromotionForProductAsync(string promotionId, string productId)
        {
            var result = await sender.Send(new DeleteProductPromotionCommand(userId, productId, promotionId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
