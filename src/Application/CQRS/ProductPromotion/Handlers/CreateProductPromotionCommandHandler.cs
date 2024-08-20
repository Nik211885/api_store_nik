using Application.Common.ResultTypes;
using Application.CQRS.ProductPromotion.Commands;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using MediatR;

namespace Application.CQRS.ProductPromotion.Handlers
{
    public class CreateProductPromotionCommandHandler : IRequestHandler<CreateProductPromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateProductPromotionCommandHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateProductPromotionCommand request, CancellationToken cancellationToken)
        {
            var productPromotion = _mapper.Map<ProductPromotionDiscount>(request);
            _dbContext.ProductPromotionDiscounts.Add(productPromotion);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
