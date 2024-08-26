using Application.CQRS.OrderValueType.Queries;
using Application.CQRS.Products.Queries;
using Application.CQRS.Promotions.Queries;
using AutoMapper;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class OrderHasNotCheckOutReponse
    {
        public string ProductId { get; init; } = null!;
        public string OrderId { get; init; } = null!;
        public string NameProduct { get; init; } = null!;
        public string ImageProduct { get; init; } = null!;
        public int QuantityForProduct { get; init; }
        public decimal PriceForProduct { get; init; }
        public int Quantity { get; init; }
        //Join make performant transmission
        public decimal PriceDefault { get; private set; }
        public decimal PriceAfterApplyPromotion { get; private set; }
        public IEnumerable<ProductNameTypeReponse>? NameTypes { get; private set; }
        public async Task Join(ISender sender)
        {
            NameTypes = await sender.Send(new GetProductNameTypeQuery(ProductId));
            foreach (var n in NameTypes is null ? [] : NameTypes)
            {
                await n.Join(sender, new OptionOrderWithCheckValueType(OrderId));
            }
            //Calculate PriceDefault you can calculate in client
            PriceDefault = PriceForProduct;
            var priceInValueTypeQuery = from n in NameTypes
                                        select
                                            from v in (IEnumerable<ProductValueType>)n.ValueTypes
                                            where v.IsChecked
                                            select v.Price;
            foreach(var valueType in priceInValueTypeQuery)
            {
                foreach (var p in valueType)
                {
                    PriceDefault += p;
                }
            }
            PriceDefault = Quantity * PriceDefault;
            //Get Promotion
            PriceAfterApplyPromotion = PriceDefault;
            var promotion = await sender.Send(new GetJustPromotionForProductQuery(ProductId));
            foreach(var p in promotion)
            {
                PriceAfterApplyPromotion -= PriceDefault * p / 100;
            }

        }
    }
    public class ProductValueType : ProductValueTypeReponse
    {
        public bool IsChecked { get; private set; }
        public override async Task Join(ISender sender, Option? option)
        {
            var orderId = option!.Id;
            IsChecked = await sender.Send(new GetOrderCheckValueTypeQuery(orderId, Id));
        }
    }
    public class MappingProductValueType : Profile
    {
        public MappingProductValueType()
        {
            CreateMap<ApplicationCore.Entities.Products.ProductValueType, ProductValueType>();
        }
    }
    public class OptionOrderWithCheckValueType : Option
    {
        public OptionOrderWithCheckValueType(string Id) : base(Id)
        {
            
        }
    } 
}
