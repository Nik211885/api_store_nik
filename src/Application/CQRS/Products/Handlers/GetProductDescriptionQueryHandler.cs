using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductDescriptionQueryHandler
        : IRequestHandler<GetProductDescriptionQuery, IEnumerable<ProductDescriptionReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductDescriptionQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDescriptionReponse>?> Handle(GetProductDescriptionQuery request, CancellationToken cancellationToken)
        {
            var query = from d in _dbContext.ProductDescriptions
                        where d.ProductId.Equals(request.ProductId)
                        select d;
            var productDescription = await query.ProjectTo<ProductDescriptionReponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return productDescription;
        }
    }
}
