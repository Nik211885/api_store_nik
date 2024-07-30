using Application.Interface;
using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetProductByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var query = from product in _dbContext.Products
                        where product.Id.Equals(request.Id)
                        select product;
            return await query.FirstOrDefaultAsync();
        }
    }
}
