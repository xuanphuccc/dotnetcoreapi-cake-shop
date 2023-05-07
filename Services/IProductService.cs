using dotnetcoreapi_cake_shop.Dtos;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface IProductService
    {
        // Get all products response DTO
        Task<List<ProductResponseDto>> GetAllProducts(int? category = null);

        // Get product response DTO
        Task<ProductResponseDto> GetProductById(int productId);

        // Create product
        Task<ProductResponseDto> CreateProduct(ProductRequestDto productRequestDto);

        // Update product
        Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequestDto);

        // Delete product
        Task<ProductResponseDto> DeleteProduct(int productId);
    }
}
