using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    internal class GetOrderDetailCheckOutOffUserQueryHandler
        : IRequestHandler<GetOrderDetailCheckOutOffUserQuery, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetOrderDetailCheckOutOffUserQueryHandler(
                IStoreNikDbContext dbContext,
                ISender sender
            )
        {
            _sender = sender;
            _dbContext = dbContext;
        }

        public async Task<IResult> Handle(GetOrderDetailCheckOutOffUserQuery request, CancellationToken cancellationToken)
        {
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId, request.IsCheckOut), cancellationToken);
            if(!cartId.Any())
            {
                throw new ArgumentException("Don't find your cart");
            }
            var query = from cart in _dbContext.Carts
                        where cartId.Contains(cart.Id)
                                && cart.IsCheckOut == request.IsCheckOut
                        join o in _dbContext.OrderDetails on cart.Id equals o.CartId
                        where o.Id.Equals(request.OrderId)
                        select o;
            if (request.IsOption)
            {
                var result = await query.ToListAsync(cancellationToken);
                return FResult.Success(result);
            }
            var resultCount = await query.CountAsync(cancellationToken);
            return FResult.Success(resultCount);
        }
    }
}
