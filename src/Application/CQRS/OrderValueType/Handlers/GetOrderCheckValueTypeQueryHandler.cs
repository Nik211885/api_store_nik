using Application.CQRS.OrderValueType.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderValueType.Handlers
{
    public class GetOrderCheckValueTypeQueryHandler
        : IRequestHandler<GetOrderCheckValueTypeQuery, bool>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetOrderCheckValueTypeQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(GetOrderCheckValueTypeQuery request, CancellationToken cancellationToken)
        {
            var query = from odpvt in _dbContext.OrderDetailProductValueType
                        where odpvt.ProductValueTypeId.Equals(request.ValueTypeId)
                              && odpvt.OrderDetailId.Equals(request.OrderId)
                        select odpvt;
            return await query.AnyAsync(cancellationToken);
        }
    }
}
