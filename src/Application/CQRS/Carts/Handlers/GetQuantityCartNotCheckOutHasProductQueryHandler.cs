using Application.CQRS.Carts.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Carts.Handlers
{
    internal class GetQuantityCartNotCheckOutHasProductQueryHandler
        : IRequestHandler<GetQuantityCartNotCheckOutHasProductQuery, int>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetQuantityCartNotCheckOutHasProductQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(GetQuantityCartNotCheckOutHasProductQuery request, CancellationToken cancellationToken)
        {
           var query = from o in _dbContext.OrderDetails
                       where o.CartId.Equals(request.CartId)
                                && o.ProductId.Equals(request.ProductId)
                        select o.Quantity;
            return await query.FirstOrDefaultAsync(cancellationToken);
                        
        }
    }
}
