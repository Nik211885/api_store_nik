﻿using Application.CQRS.Carts.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Carts.Handlers
{
    public class GetCartByUserQueryHandler : IRequestHandler<GetCartByUserQuery, Cart?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetCartByUserQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart?> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            var cartQuery = from c in _dbContext.Carts
                            where c.UserId.Equals(request.UserId)
                                    && c.IsCheckOut == request.IsCheckOut
                            select c;
            //Include is full join
            if (!request.IsIncludeProductInCart || !await cartQuery.Include(c => c.OrderDetails).AnyAsync(cancellationToken))
            {
                var cartNoInclude = await cartQuery.FirstOrDefaultAsync(cancellationToken);
                return cartNoInclude;
            }
            var cartInclude = await cartQuery.Include(c => c.OrderDetails)!.
                ThenInclude(o => o.Product).Select(c => c).
                FirstOrDefaultAsync(cancellationToken);
            return cartInclude;
        }
    }
}
