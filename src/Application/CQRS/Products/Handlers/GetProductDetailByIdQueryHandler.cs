using Application.CQRS.Products.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using ApplicationCore.Entities.Products;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductDetailByIdQueryHandler
        : IRequestHandler<GetProductDetailByIdQuery, ProductDetailReponse?>
    {
        private readonly ISender _sender;
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductDetailByIdQueryHandler(ISender sender, IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<ProductDetailReponse?> Handle(GetProductDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var productNJ = await _sender.Send(new GetProductByIdQuery(request.Id), cancellationToken);
            Guard.Against.Null<Product>(productNJ, nameof(Product), VariableException.NotFound);
            var product = _mapper.Map<ProductDetailReponse>(productNJ);
            await product.Join(_sender);
            return product;
        }
    }
}
