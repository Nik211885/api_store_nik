using Application.Common.ResultTypes;
using Application.CQRS.Products.Commands;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        public CreateProductCommandHandler(IStoreNikDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {   
            if (request is null)
            {
                return FResult.Failure("Request is not null");
            }
            var product = Mapping<CreateProductCommand, Product>
                .CreateMap().Map<Product>(request);
            _dbContext.Products.Add(product);
            var productTypes = request.NameTypes.ToList();
            foreach (var productType in productTypes)
            {
                var (nameType, valueTypes) = productType;
                var nameProductType = new ProductNameType(nameType, product.Id);
                _dbContext.ProductNameTypes.Add(nameProductType);
                foreach (var valueType in valueTypes)
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
