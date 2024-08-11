using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    public class GetOrderDetailByCartIdQueryHandler : IRequestHandler<GetOrderDetailByCartIdQuery, IEnumerable<OrderDetail>>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetOrderDetailByCartIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<OrderDetail>> Handle(GetOrderDetailByCartIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetailsQuery = from od in _dbContext.OrderDetails
                               where od.CartId.Equals(request.CartId)
                               select od;
            return await orderDetailsQuery.ToListAsync(cancellationToken);
        }
    }
}
