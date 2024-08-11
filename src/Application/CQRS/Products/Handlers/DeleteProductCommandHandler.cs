using Application.Common.ResultTypes;
using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Handlers
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
            var product = await _sender.Send(new GetProductByIdQuery(request.Id), cancellationToken);
            if (product is not null)
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return FResult.Success();
            }
            return FResult.NotFound(request.Id,nameof(Product));

        }
    }
}
