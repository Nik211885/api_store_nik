using Application.Common.ResultTypes;
using Application.CQRS.Carts.Queries;
using Application.CQRS.OrderDetails.Command;
using Application.CQRS.OrderDetails.Queries;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.OrderDetails.Handler
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdateOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            // Check user has exits 

            // Get order detail is for user not check out
            var result = await _sender.Send(
                new GetOrderDetailByIdCheckOutOffUserQuery
                (request.UserId, request.Order.OrderId), cancellationToken);
            if (result.AttachedIsSuccess is null) throw new ArgumentException("Bad Request don't find your order detail");
            OrderDetail OrderDetailNotCheckOut = result.AttachedIsSuccess;
            // Check product Name Type has product and check value type has in name type
            var productId = OrderDetailNotCheckOut.ProductId;
            // Update order detail
            //Problem with ro ri ma || aggregate
            //Update quantity
            if (request.Order.Quantity is not null)
            {
                var isProduct = await _sender.Send(new IsProductHasExitsQuery(productId, (int)request.Order.Quantity),cancellationToken);
                if (!isProduct)
                {
                    return FResult.Failure("Product has not exits or quantity in your order bigger quantity publish provide");
                }
                OrderDetailNotCheckOut.Quantity = (int)request.Order.Quantity;
                _dbContext.OrderDetails.Update(OrderDetailNotCheckOut);
            }
            //Update product value type
            //Get All Product Value type make update
            if (request.Order.ProductValueTypeId is not null)
            {
                var isProductValueType = await _sender.Send(new IsProductValueTypeQuery(productId, request.Order.ProductValueTypeId), cancellationToken);
                if (!isProductValueType)
                {
                    return FResult.Failure("Bad Request in your option product value type");
                }
                var orderDetailProductValueTypeQuery = from odpv in _dbContext.OrderDetailProductValueType
                                                       where odpv.OrderDetailId.Equals(request.Order.OrderId)
                                                       select odpv;
                var orderDetailProductValueType = await orderDetailProductValueTypeQuery.ToListAsync(cancellationToken);
                // nameTypeId and value type id
                var dictValueType = new Dictionary<string, string>();
                foreach (var o in request.Order.ProductValueTypeId)
                {
                    var nameTypeQuery = GetProductNameTypeId(o);
                    var nameType = await nameTypeQuery.FirstOrDefaultAsync(cancellationToken);
                    if (nameType is null)
                    {
                        return FResult.Failure($"Don't find name type for product has value type id {o}");
                    }
                    dictValueType.Add(nameType, o);
                }
                _dbContext.OrderDetailProductValueType.RemoveRange(orderDetailProductValueType);
                foreach (var o in orderDetailProductValueType)
                {
                    var nameTypeQuery = GetProductNameTypeId(o.ProductValueTypeId);
                    var nameType = await nameTypeQuery.FirstOrDefaultAsync(cancellationToken);
                    if (nameType is null)
                    {
                        return FResult.Failure();
                    }
                    string value;
                    if (!dictValueType.TryGetValue(nameType, out value))
                    {
                        return FResult.Failure();
                    }
                    o.ProductValueTypeId = value;
                }
                //Transaction deleted after update we can modify primary and foreign key
                _dbContext.OrderDetailProductValueType.AddRange(orderDetailProductValueType);
            }
            // Save update for database
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
        private IQueryable<string> GetProductNameTypeId(string productValueType)
        {
            var nameTypeQuery = from v in _dbContext.ProductValueTypes
                                where v.Id.Equals(productValueType)
                                select v.ProductNameTypeId;
            return nameTypeQuery;
        }
    }
}
