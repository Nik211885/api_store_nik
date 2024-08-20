using Application.CQRS.Products.Queries;
using Application.CQRS.Promotions.Queries;
using Application.CQRS.Ratings.Queries;
using ApplicationCore.Entities.Products;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class ProductDetailReponse
    {
        //POCO or DTO => POCO
        public string Id { get; private set; } = null!;
        public string NameProduct { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string ImageProduct { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string? KeySearch { get; private set; }
        public IEnumerable<PromotionDiscountReponse>? Promotions { get; private set; }
        public IEnumerable<ProductNameTypeReponse>? NameTypes { get; private set; }
        public IEnumerable<ProductDescriptionReponse>? ProductDescriptions { get; private set; }
        public IEnumerable<StatisticalRatingDTO>? StatisticalRating { get; private set; }
        public IEnumerable<RatingReponse>? Ratings { get; private set; }
        public async Task Join(ISender sender)
        {
            StatisticalRating = await sender.Send(new GetStatisticsRatingForProductQuery(Id));
            Promotions = await sender.Send(new GetPromotionForProductQuery(Id));
            var paginationRating = await sender.Send(new GetRatingForProductWithPaginationQuery(Id,PageSize:10));
            //Just get top 10 ratings
            Ratings = paginationRating.Items;
            ProductDescriptions = await sender.Send(new GetProductDescriptionQuery(Id));
            NameTypes = await sender.Send(new GetProductNameTypeQuery(Id));
        }
    };
}
