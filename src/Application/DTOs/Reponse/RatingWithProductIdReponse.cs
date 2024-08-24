namespace Application.DTOs.Reponse
{
    public class RatingWithProductIdReponse : RatingReponse
    {
        public string ProductId { get; init; } = null!;
    }
}
