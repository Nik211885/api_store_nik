using MediatR;

namespace Application.CQRS.Products.Queries
{
    internal record IsProductValueTypeQuery(string ProductId, IEnumerable<string> ProductValueTypeIds) : IRequest<bool>;
}
