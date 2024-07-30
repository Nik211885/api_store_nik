using Application.Interface;
using Application.Promotions.Commands;
using MediatR;

namespace Application.Promotions.Handlers
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand>
    {
        private readonly IStoreNikDbContext _dbContext;
        public DeletePromotionCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            if(request is not null)
            {
                _dbContext.PromotionDiscounts.Remove(request.Promotion);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
