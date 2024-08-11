using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    public class GetOrderDetailByProductIdQueryHandler : IRequestHandler<GetOrderDetailByProductIdQuery, IEnumerable<OrderDetail>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetOrderDetailByProductIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<OrderDetail>> Handle(GetOrderDetailByProductIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetailsQuery = from od in _dbContext.OrderDetails
                                    where od.ProductId.Equals(request.ProductId)
                                    select od;
            return await orderDetailsQuery.ToListAsync(cancellationToken);
        }
    }
}
