using Application.Common.ResultType;
using Application.Common.ResultTypes;
using Application.Interface;
using Application.Mappings;
using Application.Products.Commands;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateProductCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                return FResult.Failure(new ResultError("Error", "Request is not null"));
            }
            var product = Mapping<CreateProductCommand, Product>
                .CreateMap().Map<Product>(request);
            _dbContext.Products.Add(product);
            var productTypes = request.NameTypes.ToList();
            foreach (var productType in productTypes)
            {
                var (nameType,valueTypes) =  productType;
                var nameProductType = new ProductNameType(nameType, product.Id);
                _dbContext.ProductNameTypes.Add(nameProductType);
                foreach(var valueType in valueTypes)
                {
                    _dbContext.ProductValueTypes.Add(new ProductValueType(
                        valueType.ValueType, valueType.Quantity,
                        valueType.Price, nameProductType.Id));
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
