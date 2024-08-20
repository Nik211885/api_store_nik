using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    internal class GetCountOrderHasCheckOutForProductQueryHandler
        : IRequestHandler<GetCountOrderHasCheckOutForProductQuery, int>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetCountOrderHasCheckOutForProductQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(GetCountOrderHasCheckOutForProductQuery request, CancellationToken cancellationToken)
        {
            var query = from o in _dbContext.OrderDetails
                        where o.ProductId.Equals(request.ProductId)
                        join cart in _dbContext.Carts on o.CartId equals cart.Id
                        where cart.IsCheckOut
                        select o.Id;
            var count = await query.CountAsync(cancellationToken);
            return count;
        }
    }
}
