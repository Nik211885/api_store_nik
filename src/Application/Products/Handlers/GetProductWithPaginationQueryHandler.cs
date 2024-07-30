using Application.Common;
using Application.Interface;
using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Handlers
{
    public class GetProductWithPaginationQueryHandler :
        IRequestHandler<GetProductWithPaginationQuery,
            PaginationEntity<Product>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetProductWithPaginationQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<Product>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = from product in _dbContext.Products
                        select product;
            var pagination = await PaginationEntity<Product>.
                CreatePaginationEntityAsync(query.AsNoTracking(), request.PageNumber, request.PageSize);
            return pagination;
        }
    }
}
