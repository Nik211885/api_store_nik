using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Command;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.OrderDetails.Handler
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public CreateOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            // check cart for user has exits
            var cartId = await _sender.Send(new GetCartIdByUserQuery(request.UserId),cancellationToken);
            //Check this cart had this product
            var quantity = await _sender.Send(new GetQuantityCartNotCheckOutHasProductQuery(cartId.First(), request.Order.ProductId),cancellationToken);
            //If has update quantity return error don't can't add just update in your cart
            if(quantity > 0) throw new ArgumentException("This product had in cart");
            //Create new order
            //Check product
            var isProduct = await _sender.Send(new IsProductHasExitsQuery(request.Order.ProductId, request.Order.Quantity),cancellationToken);  
            if(!isProduct)
            {
                return FResult.Failure("Product has not exits or your quantity bigger quantity product provide");
            }
            //Check product Name Type has product and check value type has in name type and check quantity in value type has enough in value type
            var isProductValueType = await _sender.Send(new IsProductValueTypeQuery(request.Order.ProductId,request.Order.ProductValueTypeIds),cancellationToken);
            if (!isProductValueType)
            {
                return FResult.Failure("Choose product value type after create order detail");
            }

            //Create OrderDetail
            var orderDetail = new OrderDetail(cartId.First(),request.Order.ProductId,request.Order.Quantity);
            //Transaction
            _dbContext.OrderDetails.Add(orderDetail);
            foreach(var valueTypeId in request.Order.ProductValueTypeIds is null ?[]: request.Order.ProductValueTypeIds)
            {
                var orderDetailProductValueType = new OrderDetailProductValueType(orderDetail.Id, valueTypeId);
                _dbContext.OrderDetailProductValueType.Add(orderDetailProductValueType);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
