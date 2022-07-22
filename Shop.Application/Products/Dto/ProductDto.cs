using AutoMapper;
using Shop.Core.Dto;
using Shop.Domain.Products;

namespace Shop.Application.Products.Dto
{
    public class ProductDto: CodeNamedEntityDto<Product>
    {
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
