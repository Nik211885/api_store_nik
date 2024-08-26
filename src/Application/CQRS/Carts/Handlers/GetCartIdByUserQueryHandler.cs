using Application.CQRS.Carts.Commands;
using Application.CQRS.Carts.Queries;
using Application.Interface;
using ApplicationCore.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Carts.Handlers
{
    internal class GetCartIdByUserQueryHandler
        : IRequestHandler<GetCartIdByUserQuery, IEnumerable<string>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetCartIdByUserQueryHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<string>> Handle(GetCartIdByUserQuery request, CancellationToken cancellationToken)
        {
            var query = from c in _dbContext.Carts
                        where c.UserId.Equals(request.UserId) 
                            && c.IsCheckOut == request.IsCheckOut
                        select c.Id;
            var cartId = await query.ToListAsync(cancellationToken);
            if (!request.IsCheckOut &&( cartId is null || cartId.Count == 0))
            {
                throw new ForbiddenException("You can access this source");
            }
            return cartId;
        }
    }
}
