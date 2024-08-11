using Application.CQRS.Carts.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Carts.Handlers
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Cart?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetCartByIdQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cartQuery = from c in _dbContext.Carts
                            where c.Id.Equals(request.Id)
                            select c;
            var cart = await cartQuery.FirstOrDefaultAsync(cancellationToken);
            return cart;
        }
    }
}
