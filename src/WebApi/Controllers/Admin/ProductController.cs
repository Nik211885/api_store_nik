using Application.CQRS.Products.Commands;
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
    public class ProductController(ISender sender, IHttpContextAccessor context) 
        : BaseAuthenticationController(sender,context)
    {
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDetailViewModel product)
        {
            var result = await sender.Send(new CreateProductCommand(userId, product));
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(string productId)
        {
            var result = await sender.Send(new DeleteProductCommand(productId,userId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updatePutProduct")]
        public async Task<IActionResult> UpdatePutAsync(string productId, [FromBody] ProductDetailViewModel productUpdate)
        {
            var result = await sender.Send(new UpdatePutProductCommand(userId,productId,productUpdate));
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPatch("updatePatchProduct")]
        public async Task<IActionResult> UpdatePatchProductAsync(string productId, [FromBody] JsonPatchDocument<Product> productUpdateDoc)
        {
            var result = await sender.Send(new UpdatePatchProductCommand(userId,productId,productUpdateDoc));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
