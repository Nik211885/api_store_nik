using Application.Common;
using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.DTOs;
using Application.Interface;
using ApplicationCore.Entities.Products;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly StoreNikDbConText _dbConText;
    private readonly IEmail _emailServices;
    private readonly MailSettings _mailSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
        StoreNikDbConText dbConText,
        IEmail emailServices,
        UserManager<ApplicationUser> userManager,
        IOptions<MailSettings> mailSettings)
    {
        _emailServices = emailServices;
        _userManager = userManager;
        _dbConText = dbConText;
        _logger = logger;
        _mailSettings = mailSettings.Value;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [HttpGet("test2")]
    public ActionResult Test()
    {
        return Ok(_mailSettings);
    }
    [HttpPost("test")]
    public async Task<ActionResult> Check(string to, string body, string subject)
    {
        var result = await _emailServices.SendEmailAsync(to, body, subject);
        if (result.Success)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
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
        if(result.Success)
        {
            return Created();
        }
        return BadRequest(result.Errors);
    }
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(UserLoginViewModel userRegrist)
    {
        var user = new ApplicationUser()
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
        var result = await sender.Send(new DeleteProductCommand("",id));
        if (result.Success)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
    [HttpPut("product")]
    public async Task<IActionResult> UpdateProductAsync(ISender sender, [Required] UpdatePutProductCommand updateProduct)
    {
        var result = await sender.Send(updateProduct);
        if (result.Success)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
    // Error with add end array with path is /productNameType/-
    [HttpPatch("product")]
    public async Task<IActionResult> UpdatePatchProduct([FromBody] UpdatePatchProductCommand productUpdate, ISender sender)
    {
        var result = await sender.Send(productUpdate);
        if (result.Success)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
}
