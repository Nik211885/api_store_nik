using Application.CQRS.Products.Commands;
using Application.DTOs.Request;
using ApplicationCore.Entities.Products;
using ApplicationCore.ValueObject;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Role.Admin))]
    public class ProductController : ControllerBase
    {
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProductAsync(ISender sender, [FromBody] ProductDetailViewModel product)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var command = new CreateProductCommand(userId, product);
            var result = await sender.Send(command);
            if (result.Success)
            {
                return Created();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(ISender sender, string productId)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new DeleteProductCommand(productId,userId));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("updatePutProduct")]
        public async Task<IActionResult> UpdatePutAsync(ISender sender, string productId, [FromBody] ProductDetailViewModel productUpdate)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new UpdatePutProductCommand(userId,productId,productUpdate));
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPatch("updatePatchProduct")]
        public async Task<IActionResult> UpdatePatchProductAsync(ISender sender,string productId, [FromBody] JsonPatchDocument<Product> productUpdateDoc)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await sender.Send(new UpdatePatchProductCommand(userId,productId,productUpdateDoc));
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
