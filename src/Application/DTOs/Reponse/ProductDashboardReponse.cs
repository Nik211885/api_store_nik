using Application.CQRS.OrderDetails.Queries;
using Application.CQRS.Ratings.Queries;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class ProductDashboardReponse
    {
        //POCO or DTOs
        public string Id { get; set; } = null!;
        public string NameProduct { get; set; } = null!;
        public string ImageProduct { get; set; } = null!;
        public decimal Price { get; set; }
        public string?  KeySearch { get; set; }
        // Count to table rating and orderDetail
        public double Rating { get; set; } 
        public long RatingCount {  get; set; }
        public long Buyer { get; set; }
        public async Task Join(ISender sender)
        {
            Buyer = await sender.Send(new GetCountOrderHasCheckOutForProductQuery(Id));
            var RatingStat = await sender.Send(new GetStatisticsRatingForProductQuery(Id));
            if(!RatingStat.Any()) { Rating = 0; return; }
            double totalStar = 0;
            foreach(var r in RatingStat)
            {
                var (star, count) = r;
                totalStar += star * count;
                RatingCount = RatingCount + count;
            }
            Rating = Math.Round(totalStar  / RatingCount,2);
        }
    }
}
