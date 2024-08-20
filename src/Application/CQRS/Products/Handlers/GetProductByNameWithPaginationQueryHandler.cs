using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductByNameWithPaginationQueryHandler
        : IRequestHandler<GetProductByNameWIthPaginationQuery, PaginationEntity<ProductDashboardReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public GetProductByNameWithPaginationQueryHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper;
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<ProductDashboardReponse>> Handle(GetProductByNameWIthPaginationQuery request, CancellationToken cancellationToken)
        {
            var name = request.NameProduct is null ? string.Empty : request.NameProduct;
            var productsQuery = from product in _dbContext.Products
                                where product.NameProduct.Contains(name)
                                select product;
            var products = await productsQuery
                            .ProjectTo<ProductDashboardReponse>(_mapper.ConfigurationProvider)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);
            foreach (var item in products.Items)
            {
                await item.Join(_sender);
            }
            return products;
        }
    }
}
