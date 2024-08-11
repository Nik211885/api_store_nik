using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    internal class GetOrderDetailNotCheckOutOffUserQueryHandler
        : IRequestHandler<GetOrderDetailNotCheckOutOffUserQuery, OrderDetail?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetOrderDetailNotCheckOutOffUserQueryHandler(
                IStoreNikDbContext dbContext,
                ISender sender
            )
        {
            _sender = sender;
            _dbContext = dbContext;
        }

        public async Task<OrderDetail?> Handle(GetOrderDetailNotCheckOutOffUserQuery request, CancellationToken cancellationToken)
        {
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId), cancellationToken);
            if (cartId is null) return null;
            var query = from cart in _dbContext.Carts
                        where cart.Id.Equals(cartId)
                                && !cart.IsCheckOut
                        join o in _dbContext.OrderDetails on cart.Id equals o.CartId
                        select o;
            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
