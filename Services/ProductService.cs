using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;
using dotnetcoreapi_cake_shop.Repositories;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi_cake_shop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        // Get all products response DTO
        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            var allProducts = await _productRepository.GetAllProducts().ToListAsync();

            var allProductResponseDtos = _mapper.Map<List<ProductResponseDto>>(allProducts);
            return allProductResponseDtos;
        }

        // Get product response DTO
        public async Task<ProductResponseDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            var productResponseDto = _mapper.Map<ProductResponseDto>(product);
            return productResponseDto;
        }

        // Create product
        public async Task<ProductResponseDto> CreateProduct(ProductRequestDto productRequestDto)
        {
            var newProduct = _mapper.Map<Product>(productRequestDto);
            newProduct.CreateAt = DateTime.UtcNow;

            var createdProduct = await _productRepository.CreateProduct(newProduct);

            var createdProductResponseDto = _mapper.Map<ProductResponseDto>(createdProduct);
            return createdProductResponseDto;
        }

        // Update product
        public async Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequestDto)
        {
            var existProduct = await _productRepository.GetProductById(id);

            if (existProduct == null)
            {
                throw new Exception("product not found");
            }

            _mapper.Map(productRequestDto, existProduct);
            var updatedProduct = await _productRepository.UpdateProduct(existProduct);

            var updatedProductResponseDto = _mapper.Map<ProductResponseDto>(updatedProduct);
            return updatedProductResponseDto;
        }

        // Delete product
        public async Task<ProductResponseDto> DeleteProduct(int productId)
        {
            var existProduct = await _productRepository.GetProductById(productId);

            if (existProduct == null)
            {
                throw new Exception("product not found");
            }

            var deletedProduct = await _productRepository.DeleteProduct(existProduct);

            var deletedProductResponseDto = _mapper.Map<ProductResponseDto>(deletedProduct);
            return deletedProductResponseDto;
        }
    }
}
