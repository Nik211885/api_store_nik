using Application.Common.ResultTypes;
using Application.CQRS.OrderDetails.Command;
using Application.CQRS.OrderDetails.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Handler
{
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private ISender _sender;
        public DeleteOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _sender.Send(new GetOrderDetailByIdQuery(request.Id),cancellationToken);
            if(orderDetail is null)
            {
                return FResult.NotFound(request.Id,nameof(OrderDetail));
            }
            _dbContext.OrderDetails.Remove(orderDetail);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();

        }
    }
}
