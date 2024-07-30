using Application.Interface;
using Application.Mappings;
using Application.Promotions.Commands;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Promotions.Handlers
{
    public class CreatePromotionCommandHandler :
        IRequestHandler<CreatePromotionCommand, string>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreatePromotionCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            return request.UserId;

        }
    }
}
