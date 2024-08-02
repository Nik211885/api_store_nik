using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Handlers
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IEnumerable<Product>?>
    {
        public async Task<IEnumerable<Product>?> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            // query db return product have same prototype user key search
            return null;
        }
    }
}
