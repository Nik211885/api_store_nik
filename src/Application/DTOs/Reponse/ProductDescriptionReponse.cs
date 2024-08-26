using ApplicationCore.Entities.Products;
using AutoMapper;

namespace Application.DTOs.Reponse
{
    public record ProductDescriptionReponse(string NameDescription, string ValueDescription);
    public class MappingProductDescription : Profile
    {
        public MappingProductDescription()
        {
            CreateMap<ProductDescription, ProductDescriptionReponse>();
        }
    }
}
