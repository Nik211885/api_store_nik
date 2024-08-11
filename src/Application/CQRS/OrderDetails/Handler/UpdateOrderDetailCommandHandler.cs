using Application.Common.ResultTypes;
using Application.CQRS.OrderDetails.Command;
using Application.CQRS.OrderDetails.Queries;
using Application.CQRS.Products.Queries;
using Application.Interface;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Application.CQRS.OrderDetails.Handler
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public UpdateOrderDetailCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            // Check user has exits 

            // Get order detail is for user not check out
            var OrderDetailNotCheckOut = await _sender.Send(
                new GetOrderDetailNotCheckOutOffUserQuery
                (request.UserId, request.OrderId), cancellationToken);
            if (OrderDetailNotCheckOut is null)
            {
                return FResult.Failure("Don't update order detail");
            }
            // Check product Name Type has product and check value type has in name type
            var productId = OrderDetailNotCheckOut.ProductId;

            var isProductValueType = await _sender.Send(new IsProductValueTypeQuery(productId, request.ProductValueTypeId), cancellationToken);
            if (!isProductValueType)
            {
                return FResult.Failure("Product don't have product value type");
            }
            // Update order detail
            //Problem with ro ri ma || aggregate
            //Update quantity
            OrderDetailNotCheckOut.Quantity = request.Quantity;
            //Update product value type
            //Get All Product Value type make update
            var orderDetailProductValueTypeQuery = from odpv in _dbContext.OrderDetailProductValueType
                                                   where odpv.OrderDetailId.Equals(request.OrderId)
                                                   select odpv;
            var orderDetailProductValueType = await orderDetailProductValueTypeQuery.ToListAsync(cancellationToken);
            // nameTypeId and value type id
            var dictValueType = new Dictionary<string, string>();
            foreach (var o in request.ProductValueTypeId)
            {
                var nameTypeQuery = GetProductNameTypeId(o);
                var nameType = await nameTypeQuery.FirstOrDefaultAsync(cancellationToken);
                if(nameType is null)
                {
                    return FResult.Failure($"Don't find name type for product has value type id {o}");
                }
                dictValueType.Add(nameType, o);
            }
            foreach(var o in orderDetailProductValueType)
            {
                var nameTypeQuery = GetProductNameTypeId(o.ProductValueTypeId);
                var nameType = await nameTypeQuery.FirstOrDefaultAsync();
                if(nameType is null)
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
            // Save update for database
            _dbContext.OrderDetails.Update(OrderDetailNotCheckOut);
            _dbContext.OrderDetailProductValueType.UpdateRange(orderDetailProductValueType);
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
