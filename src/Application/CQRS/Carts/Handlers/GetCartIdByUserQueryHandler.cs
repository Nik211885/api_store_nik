using Application.CQRS.Carts.Commands;
using Application.CQRS.Carts.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Carts.Handlers
{
    internal class GetCartIdByUserQueryHandler
        : IRequestHandler<GetCartIdByUserQuery, string>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public GetCartIdByUserQueryHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<string> Handle(GetCartIdByUserQuery request, CancellationToken cancellationToken)
        {
            var query = from c in _dbContext.Carts
                        where c.UserId.Equals(request.UserId) && !c.IsCheckOut
                        select c.Id;
            var cartId = await query.FirstOrDefaultAsync(cancellationToken);
            if(cartId is null)
            {
                await _sender.Send(new CreateCartCommand(request.UserId),cancellationToken);
                return await this.Handle(request,cancellationToken);
            }
            return cartId;
        }
    }
}
