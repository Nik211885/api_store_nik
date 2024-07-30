using Application.Common.ResultTypes;
using Application.Interface;
using Application.Products.Commands;
using Application.Products.Queries;
using MediatR;

namespace Application.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeleteProductCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(new GetProductByIdQuery(request.Id),cancellationToken);
            if(product is not null)
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return FResult.Success();
            }
            return FResult.Failure(FErrors.NotFound(request.Id));
            
        }
    }
}
