using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
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
                         where p.Id.Equals(request.ProductId)
                         select p.UserId;
            var userId = await query1.FirstOrDefaultAsync(cancellationToken);
            Guard.Against.Null(userId,nameof(Product),VariableException.NotFound);
            if(userId != request.UserId)
            {
                throw new UnauthorizedAccessException("You don't can edit this product");
            }
        }
    }
}
