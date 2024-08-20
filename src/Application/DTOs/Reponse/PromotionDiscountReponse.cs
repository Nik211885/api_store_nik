namespace Application.DTOs.Reponse
{
    public record PromotionDiscountReponse(string Id, string Name, string? Description, decimal Promotion, DateTime StartDay, DateTime EndDate);
}
