using MediatR;

namespace Application.CQRS.Products.Queries
{
    internal record IsProductHasExitsQuery(string ProductId, int Quantity = 0) : IRequest<bool>;
}
