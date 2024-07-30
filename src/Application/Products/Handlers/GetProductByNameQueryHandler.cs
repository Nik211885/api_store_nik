using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Product>
    {
        public Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
