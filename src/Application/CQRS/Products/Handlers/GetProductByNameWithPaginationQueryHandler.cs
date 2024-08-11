using Application.Common;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductByNameWithPaginationQueryHandler
        : IRequestHandler<GetProductByNameWIthPaginationQuery, PaginationEntity<Product>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetProductByNameWithPaginationQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<Product>> Handle(GetProductByNameWIthPaginationQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = from product in _dbContext.Products
                                where product.NameProduct.Contains(request.NameProduct)
                                select product;
            var pagination = await PaginationEntity<Product>.CreatePaginationEntityAsync(productsQuery,request.PageNumber, request.PageSize);
            return pagination;
        }
    }
}
