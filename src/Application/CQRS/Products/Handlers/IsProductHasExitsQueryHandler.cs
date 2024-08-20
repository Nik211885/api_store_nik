    using Application.CQRS.Products.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    internal class IsProductHasExitsQueryHandler
        : IRequestHandler<IsProductHasExitsQuery, bool>
    {
        private readonly IStoreNikDbContext _dbContext;
        public IsProductHasExitsQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(IsProductHasExitsQuery request, CancellationToken cancellationToken)
        {
            //Check product has exits
            var query = from p in _dbContext.Products
                        where p.Id.Equals(request.ProductId)
                        && p.Quantity >= request.Quantity
                        select p;
            var result = await query.AnyAsync(cancellationToken);
            return result;
        }
    }
}
