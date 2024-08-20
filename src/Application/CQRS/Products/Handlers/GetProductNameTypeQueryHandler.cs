using Application.CQRS.Products.Queries;
using Application.DTOs.Reponse;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductNameTypeQueryHandler
        : IRequestHandler<GetProductNameTypeQuery, IEnumerable<ProductNameTypeReponse>?>
    {
        private readonly ISender _sender;
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductNameTypeQueryHandler(ISender sender, IStoreNikDbContext dbContext, IMapper mapper)
        {
            _sender = sender;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductNameTypeReponse>?> Handle(GetProductNameTypeQuery request, CancellationToken cancellationToken)
        {
            var query = from pn in _dbContext.ProductNameTypes
                        where pn.ProductId.Equals(request.ProductId)
                        select pn;
            var productNameType = await query.ProjectTo<ProductNameTypeReponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            foreach(var pn in productNameType)
            {
                await pn.Join(_sender);
            }
            return productNameType;
        }
    }
}
