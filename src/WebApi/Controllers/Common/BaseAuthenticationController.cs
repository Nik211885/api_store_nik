using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseAuthenticationController : ControllerBase
    {
        protected readonly string userId;
        protected readonly ISender sender;
        protected BaseAuthenticationController(ISender sender, IHttpContextAccessor context)
        {
            Guard.Against.Null(context.HttpContext, nameof(context));
            userId = context.HttpContext.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            this.sender = sender;
        }
    }
}
