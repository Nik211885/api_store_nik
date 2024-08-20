using Application.Common;
using Application.Common.Mappings;
using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductWithPaginationQueryHandler :
        IRequestHandler<GetProductWithPaginationQuery,
            PaginationEntity<ProductDashboardReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public GetProductWithPaginationQueryHandler(IStoreNikDbContext dbContext, IMapper mapper,ISender sender)
        {
            _sender = sender;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<PaginationEntity<ProductDashboardReponse>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = from p in _dbContext.Products
                          select p;
            var products = await query.ProjectTo<ProductDashboardReponse>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            foreach (var item in products.Items)
            {
                await item.Join(_sender);
            }
            return products;
        }
    }
}
