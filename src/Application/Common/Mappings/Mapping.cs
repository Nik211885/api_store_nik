using Application.CQRS.Carts.Commands;
using Application.CQRS.ProductPromotion.Commands;
using Application.CQRS.Promotions.Commands;
using Application.CQRS.Ratings.Commands;
using Application.CQRS.Reactions.Commands;
using Application.DTOs;
using Application.DTOs.Reponse;
using Application.DTOs.Request;
using Application.Interface;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateCartCommand, Cart>();
            CreateMap<CreateProductPromotionCommand, ProductPromotionDiscount>();
            CreateMap<ProductDetailViewModel, Product>();
            CreateMap<CreatePromotionCommand, PromotionDiscount>();
            CreateMap<UpdatePromotionCommand,PromotionDiscount>();
            //CreateMap<CreateReactionCommand,Reaction>();
            //CreateMap<CreateRatingCommand,Rating>();
            CreateMap<IUser, UserDetailReponse>();
            CreateMap<Product, ProductDashboardReponse>();
            CreateMap<Product, ProductDetailReponse>();
            CreateMap<PromotionDiscount, PromotionDiscountReponse>();
            CreateMap<Rating, RatingReponse>();
            CreateMap<ProductDescription, ProductDescriptionReponse>();
            CreateMap<ProductNameType, ProductNameTypeReponse>();
            CreateMap<ProductValueType, ProductValueTypeReponse>();
        }
    }
}
