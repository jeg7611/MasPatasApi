using MasPatas.Application.DTOs;

namespace MasPatas.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
