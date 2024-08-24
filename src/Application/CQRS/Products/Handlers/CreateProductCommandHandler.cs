using Application.Common.ResultTypes;
using Application.CQRS.Products.Commands;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IStoreNikDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {   
            if (request is null)
            {
                return FResult.Failure("Request is not null");
            }
            var product = new Product(request.UserId);
            product = _mapper.Map(request.Product,product);
            _dbContext.Products.Add(product);
            var productTypes = request.Product.NameTypes is null? [] : request.Product.NameTypes.ToList();
            foreach (var productType in productTypes)
            {
                var (nameType, valueTypes) = productType;
                var nameProductType = new ProductNameType(nameType, product.Id);
                _dbContext.ProductNameTypes.Add(nameProductType);
                foreach (var valueType in valueTypes)
                {
                    _dbContext.ProductValueTypes.Add(new ProductValueType(
                        valueType.ValueType,
                        valueType.Price, nameProductType.Id));
                }
            }
            var productDescription = request.Product.ProductDescription is null? [] : request.Product.ProductDescription ;
            foreach(var pd in productDescription)
            {
                var (nameDescription, valueDescription) = pd;
                _dbContext.ProductDescriptions.Add(new ProductDescription(product.Id, nameDescription, valueDescription));
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
