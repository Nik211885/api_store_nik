using ApplicationCore.Entities.Products;

namespace Application.DTOs.Reponse
{
    //Get 10 rating in head
    public record ProductDTO(Product Product, IEnumerable<StatisticalRatingDTO> StatisticRating);
}
