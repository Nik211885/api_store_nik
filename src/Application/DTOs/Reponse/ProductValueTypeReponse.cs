using Application.CQRS.Products.Queries;
using AutoMapper;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class ProductValueTypeReponse
    {
        public string Id { get; set; } = null!;
        public string ValueType { get; set; } = null!;
        public decimal Price { get; set; }
        public virtual async Task Join(ISender sender, Option? option)
        {
            await Task.CompletedTask;
        }
    }
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<ApplicationCore.Entities.Products.ProductValueType, ProductValueTypeReponse>();
        }
    }
}
