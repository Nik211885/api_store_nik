using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Command;
using Application.Interface;
using ApplicationCore.ValueObject;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeleteOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var carId = await _sender.Send(new GetCartIdByUserQuery(request.UserId),cancellationToken);
            var query = from o in _dbContext.OrderDetails
                        where o.Id.Equals(request.OrderId)
                            && o.CartId.Equals(carId.First())
                        select o;
            var od = await query.FirstOrDefaultAsync(cancellationToken);
            if(od is null)
            {
                return FResult.Failure(VariableException.NotFound);
            }
            _dbContext.OrderDetails.Remove(od);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();

        }
    }
}
