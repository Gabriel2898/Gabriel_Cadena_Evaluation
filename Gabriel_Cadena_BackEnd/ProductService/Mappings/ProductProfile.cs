// Mappings/ProductProfile.cs
using AutoMapper;
using ProductService.DTOs;
using ProductService.Models;

namespace ProductService.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}