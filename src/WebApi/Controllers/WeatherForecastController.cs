using Application.Common;
using Application.DTOs;
using Application.Products.Commands;
using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using Infrastructure.Data;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly StoreNikDbConText _dbConText;
    private readonly UserManager<ApplicationUser> _userManager;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
        StoreNikDbConText dbConText,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _dbConText = dbConText;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet("GetProduct")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetProduct(ISender sender){
        var products = await _dbConText.Products.ToListAsync();
        return Ok(products);
    }
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var result = await sender.Send(command);
        if(result.IsSuccess)
        {
            return Created();
        }
        return BadRequest(result.Errors);
    }
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(UserLoginViewModel userRegrist)
    {
        var user = new ApplicationUser
        {
            UserName = userRegrist.UserName,
            Email = userRegrist.Email,
        };
        var result = await _userManager.CreateAsync(user, userRegrist.Password);
        if (result.Succeeded)
        {
            return Ok(user.Id);
        }
        return BadRequest();
    }
    [HttpGet("product")]
    public async Task<ActionResult<Product>> GetProduct([Required] string id, ISender sender)
    {
        var product = await sender.Send(new GetProductByIdQuery(id));
        if(product is null)
        {
            return BadRequest();
        }
        return Ok(product);
    }
    [HttpDelete("product")]
    public async Task<IActionResult> DeleteProduct([Required] string id,ISender sender)
    {
        var result = await sender.Send(new DeleteProductCommand(id));
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
    [HttpPut("product")]
    public async Task<IActionResult> UpdateProductAsync(ISender sender, [Required] string Id, ProductUpdateViewModel product)
    {
        var result = await sender.Send(new UpdatePutProductCommand(Id, product));
        if (result.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber, int pageSize, ISender sender)
    {
        if (pageNumber < 0 || pageSize < 0)
        {
            return BadRequest();
        }
        PaginationEntity<Product> paginationProduct;
        if (pageNumber == 0)
        {
            if (pageSize == 0)
            {
                paginationProduct = await sender.Send(new GetProductWithPaginationQuery());
            }
            else
            {
                paginationProduct = await sender.Send(new GetProductWithPaginationQuery(PageSize: pageSize));
            }
        }
        else
        {
            if (pageSize == 0)
            {
                paginationProduct = await sender.Send(new GetProductWithPaginationQuery(PageNumber: pageNumber));
            }
            else
            {
                paginationProduct = await sender.Send(new GetProductWithPaginationQuery(pageNumber, pageSize));
            }
        }
        var products = paginationProduct.Items;
        return Ok(products);
    }
    // Error with add end array with path is /productNameType/-
    [HttpPatch("product")]
    public async Task<IActionResult> UpdatePatchProduct([Required] string id,[FromBody] JsonPatchDocument<Product> patchDoc, ISender sender)
    {
        var result = await sender.Send(new UpdatePatchProductCommand(id, patchDoc));
        if (result.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
}
