using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using ApplicationCore.ValueObject;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductDetailByIdQueryHandler
        : IRequestHandler<GetProductDetailByIdQuery, ProductDetailReponse>
    {
        private readonly ISender _sender;
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenClaims _tokenClaims;
        public GetProductDetailByIdQueryHandler(ISender sender, IStoreNikDbContext dbContext, IMapper mapper, ITokenClaims tokenClaims)
        {
            _mapper = mapper;
            _sender = sender;
            _dbContext = dbContext;
            _tokenClaims = tokenClaims;
        }
        public async Task<ProductDetailReponse> Handle(GetProductDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var productNJ = await _sender.Send(new GetProductByIdQuery(request.Id), cancellationToken);
            if (productNJ is not null)
            {
                string? userId = null;
                if (request.accessToken is not null)
                {
                    userId = await _tokenClaims.ValidAccessTokenHasExpriseAsync(request.accessToken);
                }
                var product = _mapper.Map<ProductDetailReponse>(productNJ);
                await product.Join(_sender, userId);
                return product;
            }
            throw new ArgumentException(VariableException.NotFound);
        }
    }
}
