using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
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
            var query = _dbContext.Products.Where(p => p.Id.Equals(request.Id))
                            .Include(p => p.ProductNameTypes);
            if (await query.Select(p => p.ProductNameTypes).AnyAsync(cancellationToken))
            {
                query!.ThenInclude(pnd => pnd.ProductValueTypes);
            }
            return await query.Select(p => p).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
