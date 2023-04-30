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
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        public ProductService(
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
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

            var createdProduct = await _productRepository.CreateProduct(newProduct);

            List<ProductImage> productImages = _mapper.Map<List<ProductImage>>(productRequestDto.Images);
            foreach (var item in productImages)
            {
                item.ProductId = createdProduct.ProductId;
                await _productImageRepository.CreateProductImage(item);
            }

            var createdProductResponseDto = _mapper.Map<ProductResponseDto>(createdProduct);
            return createdProductResponseDto;
        }

        // Update product
        public async Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequestDto)
        {
            return null!;
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
