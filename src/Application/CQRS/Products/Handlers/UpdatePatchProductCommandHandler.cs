using Application.Common.ResultTypes;
using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class UpdatePatchProductCommandHandler : IRequestHandler<UpdatePatchProductCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdatePatchProductCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdatePatchProductCommand request, CancellationToken cancellationToken)
        {
            //Check product need update
            var product = await _sender.Send(new GetProductByIdQuery(request.Id));
            if (product is null)
            {
                return FResult.NotFound(request.Id, nameof(Product));
            }
            // Constrains foreign key pass database if error throw exception
            // If patch doc create new name product type
            // If name product type is null create new array else add end
            // If patch doc create new product value type
            // If product value type is null create new array else add end

            request.PatchDoc.ApplyTo(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
