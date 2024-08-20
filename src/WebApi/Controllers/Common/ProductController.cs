﻿using Application.Common;
using Application.CQRS.Products.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("products")]
        public async Task<ActionResult<PaginationEntity<ProductDashboardReponse>>> GetProductsAsync(ISender sender, int pageNumber)
        {
            var products = await sender.Send(new GetProductWithPaginationQuery(pageNumber));
            return Ok(products);
        }
        [HttpGet("products/search")]
        public async Task<ActionResult<PaginationEntity<ProductDashboardReponse>>> GetProductByNameAsync(ISender sender, string? name, int pageNumber)
        {
            var products = await sender.Send(new GetProductByNameWIthPaginationQuery(name, pageNumber));
            return Ok(products);
        }
        [HttpGet("product/statisticalRating")]
        public async Task<ActionResult<StatisticalRatingDTO>> GetStatisticalRatingForProductAsync(ISender sender,[Required] string id)
        {
            var statisticalRatingForProduct = await sender.Send(new GetStatisticsRatingForProductQuery(id));
            return Ok(statisticalRatingForProduct);
        }
        [HttpGet("product")]
        public async Task<ActionResult<ProductDetailReponse>> GetProductByIdAsync(ISender sender, [Required] string id)
        {
            var product = await sender.Send(new GetProductDetailByIdQuery(id));
            return Ok(product);
        }
    }
}
