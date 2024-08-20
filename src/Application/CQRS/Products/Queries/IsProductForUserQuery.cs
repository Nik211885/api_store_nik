using MediatR;

namespace Application.CQRS.Products.Queries
{
    internal record IsProductForUserQuery(string UserId, string ProductId) : IRequest;
}
