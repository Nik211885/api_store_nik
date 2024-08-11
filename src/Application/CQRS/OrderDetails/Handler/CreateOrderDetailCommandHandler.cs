using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Command;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Handler
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public CreateOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            // check cart for user
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId),cancellationToken);
            //Check product
            var isProduct = await _sender.Send(new IsProductHasExitsQuery(request.ProductId, request.Quantity),cancellationToken);  
            if(!isProduct)
            {
                return FResult.Failure("Product has not exits or your quantity bigger quantity product provide");
            }
            //Check product Name Type has product and check value type has in name type
            var isProductValueType = await _sender.Send(new IsProductValueTypeQuery(request.ProductId,request.ProductValueTypeIds),cancellationToken);
            if (!isProductValueType)
            {
                return FResult.Failure("Product don't have product value type");
            }
            //Create OrderDetail
            var orderDetail = new OrderDetail(cartId,request.ProductId,request.Quantity);
            _dbContext.OrderDetails.Add(orderDetail);
            foreach(var valueTypeId in request.ProductValueTypeIds)
            {
                var orderDetailProductValueType = new OrderDetailProductValueType(orderDetail.Id, valueTypeId);
                _dbContext.OrderDetailProductValueType.Add(orderDetailProductValueType);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
