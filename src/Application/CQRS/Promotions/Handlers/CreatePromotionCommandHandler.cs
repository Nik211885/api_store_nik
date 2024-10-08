﻿using Application.Common.ResultTypes;
using Application.CQRS.Promotions.Commands;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Promotions.Handlers
{
    public class CreatePromotionCommandHandler :
        IRequestHandler<CreatePromotionCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreatePromotionCommandHandler(IStoreNikDbContext dbContext,IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotionDiscount = new PromotionDiscount(request.UserId);
            promotionDiscount = _mapper.Map(request.Promotion,promotionDiscount);
            _dbContext.PromotionDiscounts.Add(promotionDiscount);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
