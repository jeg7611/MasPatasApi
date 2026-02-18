using AutoMapper;
using MasPatas.Application.DTOs;
using MasPatas.Domain.Entities;

namespace MasPatas.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}
