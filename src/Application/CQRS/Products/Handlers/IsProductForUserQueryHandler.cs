using Application.CQRS.Products.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    internal class IsProductForUserQueryHandler : IRequestHandler<IsProductForUserQuery>
    {
        private readonly IStoreNikDbContext _dbContext;
        public IsProductForUserQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(IsProductForUserQuery request, CancellationToken cancellationToken)
        {
            var query1 = from p in _dbContext.Products
                         where p.UserId.Equals(request.UserId)
                         select p;
            if(! await query1.AnyAsync(cancellationToken))
            {
                throw new UnauthorizedAccessException("You don't can update this product");
            }
        }
    }
}
