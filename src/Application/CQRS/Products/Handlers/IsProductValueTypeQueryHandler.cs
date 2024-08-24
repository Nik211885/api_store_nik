using Application.CQRS.Products.Queries;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Products.Handlers
{
    internal class IsProductValueTypeQueryHandler
        : IRequestHandler<IsProductValueTypeQuery, bool>
    {
        private readonly IStoreNikDbContext _dbContext;
        public IsProductValueTypeQueryHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(IsProductValueTypeQuery request, CancellationToken cancellationToken)
        {
            // Check in product has product name type has product value type 
            // and with one name type just one value type
            
            //Get name type for product
            var nameTypeIdQuery = from n in _dbContext.ProductNameTypes
                           where n.ProductId.Equals(request.ProductId)
                           select n.Id;
            var nameTypeId = await nameTypeIdQuery.ToListAsync(cancellationToken);

            if(nameTypeId.Count != request.ProductValueTypeIds?.Count())
            {
                return false;
            }

            //check value type for name type
            foreach(var value in request.ProductValueTypeIds)
            {
                var nameTypeIdQueryByValueType = from v in _dbContext.ProductValueTypes
                                               where v.Id.Equals(value)
                                               join n in _dbContext.ProductNameTypes on v.ProductNameTypeId equals n.Id
                                               select n.Id;
                var nameTypeIdByValue = await nameTypeIdQueryByValueType.FirstOrDefaultAsync(cancellationToken);
                if(nameTypeIdByValue is null)
                {
                    return false;
                }
                int index = 0;
                while(index < nameTypeId.Count)
                {
                    if (nameTypeId[index].Equals(nameTypeIdByValue))
                    {
                        nameTypeId.RemoveAt(index);
                        break;
                    }
                    if(index == nameTypeId.Count - 1)
                    {
                        return false;
                    }
                    index++;
                }
            }
            return true;
        }
    }
}
