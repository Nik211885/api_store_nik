using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    public class GetOrderForUserHasNotCheckOutQueryHandler
        : IRequestHandler<GetOrderForUserHasNotCheckOutQuery, IEnumerable<OrderHasNotCheckOutReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetOrderForUserHasNotCheckOutQueryHandler(IStoreNikDbContext dbContext,
                ISender sender
            )
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<OrderHasNotCheckOutReponse>?> Handle(GetOrderForUserHasNotCheckOutQuery request,
            CancellationToken cancellationToken)
        {
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId),cancellationToken);
            Guard.Against.Null(cartId, nameof(cartId));
            var query = from o in _dbContext.OrderDetails
                        where o.CartId.Equals(cartId.First())
                        join p in _dbContext.Products on o.ProductId equals p.Id
                        select new OrderHasNotCheckOutReponse
                        {
                            ProductId = p.Id,
                            OrderId = o.Id,
                            NameProduct = p.NameProduct,
                            ImageProduct = p.ImageProduct,
                            QuantityForProduct = p.Quantity,
                            PriceForProduct = p.Price,
                            Quantity = o.Quantity,
                        };
            var orders = await query.ToListAsync(cancellationToken);
            foreach(var o in orders)
            {
                await o.Join(_sender);
            }
            return orders;
        }
    }
}
