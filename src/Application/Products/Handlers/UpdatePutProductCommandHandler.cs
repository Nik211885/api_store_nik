using Application.Common.ResultTypes;
using Application.DTOs;
using Application.Interface;
using Application.Mappings;
using Application.Products.Commands;
using Application.Products.Queries;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.Products.Handlers
{
    public class UpdatePutProductCommandHandler : IRequestHandler<UpdatePutProductCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdatePutProductCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(UpdatePutProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(new GetProductByIdQuery(request.Id));
            if(product is not null)
            {
                product = Mapping<ProductUpdateViewModel,Product>.CreateMap().Map(request.Product, product);
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return FResult.Success();
            }
            return FResult.Failure(FErrors.NotFound(request.Id));
        }
    }
}
