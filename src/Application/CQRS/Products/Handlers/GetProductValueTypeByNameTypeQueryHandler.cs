using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductValueTypeByNameTypeQueryHandler
        : IRequestHandler<GetProductValueTypeByNameTypeQuery, IEnumerable<ProductValueTypeReponse>>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductValueTypeByNameTypeQueryHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductValueTypeReponse>> Handle(GetProductValueTypeByNameTypeQuery request, CancellationToken cancellationToken)
        {
            var query = from v in _dbContext.ProductValueTypes
                        where v.ProductNameTypeId.Equals(request.NameTypeId)
                        select v;
            var productValueType = await query.ProjectTo<ProductValueTypeReponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return productValueType;
        }
    }
}
