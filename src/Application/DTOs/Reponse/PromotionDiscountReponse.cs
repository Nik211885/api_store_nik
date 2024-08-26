using Application.DTOs.Request;
using ApplicationCore.Entities.Products;
using AutoMapper;

namespace Application.DTOs.Reponse
{
    public record PromotionDiscountReponse(string Id, string Name, string? Description, bool ApplyAll, decimal Promotion, DateTime StartDay, DateTime EndDate)
        : PromotionViewModel(Name,Promotion,StartDay,EndDate,ApplyAll,Description);
    public class MappingPromotionDiscount : Profile
    {
        public MappingPromotionDiscount()
        {
            CreateMap<PromotionDiscount, PromotionDiscountReponse>();
        }
    }
}
