using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Exceptions;
using ApplicationCore.ValueObject;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    internal class GetOrderDetailByIdCheckOutOffUserQueryHandler
        : IRequestHandler<GetOrderDetailByIdCheckOutOffUserQuery, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetOrderDetailByIdCheckOutOffUserQueryHandler(
                IStoreNikDbContext dbContext,
                ISender sender
            )
        {
            _sender = sender;
            _dbContext = dbContext;
        }

        public async Task<IResult> Handle(GetOrderDetailByIdCheckOutOffUserQuery request, CancellationToken cancellationToken)
        {
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId, request.IsCheckOut), cancellationToken);
            var query = from cart in _dbContext.Carts
                        where cartId.Contains(cart.Id)
                                && cart.IsCheckOut == request.IsCheckOut
                        join o in _dbContext.OrderDetails on cart.Id equals o.CartId
                        where o.Id.Equals(request.OrderId)
                        select o;
            var resultCount = await query.CountAsync(cancellationToken);
            if(resultCount == 0)
            {
                return FResult.Failure(VariableException.NotFound);
            }
            if (request.IsOption)
            {
                var result = await query.FirstOrDefaultAsync(cancellationToken);
                return FResult.Success(result);
            }
            return FResult.Success();
        }
    }
}
