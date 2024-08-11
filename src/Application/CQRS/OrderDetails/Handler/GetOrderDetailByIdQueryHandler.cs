using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    internal class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, OrderDetail?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetOrderDetailByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OrderDetail?> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetailQuery = from od in _dbContext.OrderDetails
                                   where od.Id.Equals(request.Id)
                                   select od;
            var cartId = await orderDetailQuery.Select(x => x.CartId).FirstOrDefaultAsync(cancellationToken);
            if(cartId is null)
            {
                return null;
            }
            var isCheckOut = from c in _dbContext.Carts
                             where c.IsCheckOut == request.IsCheckOut
                             select c;
            if(!await isCheckOut.AnyAsync(cancellationToken))
            {
                return null;
            }
            return await orderDetailQuery.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
