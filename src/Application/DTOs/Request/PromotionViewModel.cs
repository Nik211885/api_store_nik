using ApplicationCore.Entities.Products;
using AutoMapper;

namespace Application.DTOs.Request
{
    public record PromotionViewModel(string Name,
                                           decimal Promotion,
                                           DateTime StartDay,
                                           DateTime EndDate,
                                           bool ApplyAll,
                                           string? Description);
    public class MappingPromotion : Profile
    {
        public MappingPromotion()
        {
            CreateMap<PromotionViewModel, PromotionDiscount>();
        }
    }
}
