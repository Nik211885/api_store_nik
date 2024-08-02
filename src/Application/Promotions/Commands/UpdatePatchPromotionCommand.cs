using Application.Common.ResultTypes;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Promotions.Commands
{
    public record UpdatePatchPromotionCommand(string Id, JsonPatchDocument<PromotionDiscount> PatchDoc) : IRequest<Result>;
}
