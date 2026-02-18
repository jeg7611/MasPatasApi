using AutoMapper;
using MasPatas.Application.DTOs;
using MasPatas.Application.Interfaces;
using MasPatas.Domain.Entities;
using MasPatas.Domain.Repositories;

namespace MasPatas.Application.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
    // Use case: CreateProduct
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Product>(dto);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        var created = await productRepository.CreateAsync(entity, cancellationToken);
        return mapper.Map<ProductDto>(created);
    }

    // Use case: GetAllProducts
    public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyList<ProductDto>>(products);
    }

    // Use case: GetProductById
    public async Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken);
        return product is null ? null : mapper.Map<ProductDto>(product);
    }
}
