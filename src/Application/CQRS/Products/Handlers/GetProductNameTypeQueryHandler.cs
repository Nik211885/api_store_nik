using Application.CQRS.Products.Queries;
using Application.DTOs;
using Application.DTOs.Reponse;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    public class GetProductNameTypeQueryHandler
        : IRequestHandler<GetProductNameTypeQuery, IEnumerable<ProductNameTypeReponse>?>
    {
        private readonly IStoreNikDbContext _dbContext;
        public GetProductNameTypeQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductNameTypeReponse>?> Handle(GetProductNameTypeQuery request, CancellationToken cancellationToken)
        {
            var query = from pn in _dbContext.ProductNameTypes
                        where pn.ProductId.Equals(request.ProductId)
                        select new ProductNameTypeReponse
                        {
                            Id = pn.Id,
                            NameType = pn.NameType,
                        };
            var productNameType = await query.ToListAsync(cancellationToken);
            return productNameType;
        }
    }
}
