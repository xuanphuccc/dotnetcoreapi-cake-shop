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
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public ProductService(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        // Get all products response DTO
        public async Task<List<ProductResponseDto>> GetAllProducts(int? category = null)
        {
            var allProductsQuery = _productRepository.GetAllProducts();

            // Get product by category
            if (category.HasValue)
            {
                allProductsQuery = allProductsQuery.Where(p => p.CategoryId == category.Value);
            }

            var allProducts = await allProductsQuery.ToListAsync();
            var allProductResponseDtos = _mapper.Map<List<ProductResponseDto>>(allProducts);

            // Check products has orders or not
            foreach (var productResponse in allProductResponseDtos)
            {
                await CheckHasOrderProduct(productResponse);
            }

            return allProductResponseDtos;
        }

        // Get product response DTO
        public async Task<ProductResponseDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            var productResponseDto = _mapper.Map<ProductResponseDto>(product);
            await CheckHasOrderProduct(productResponseDto);

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

            // Check product has orders or not
            var hasOrders = await _orderRepository.HasOrders(productId);
            if(hasOrders > 0)
            {
                throw new Exception("this product already  on order");
            }


            var deletedProduct = await _productRepository.DeleteProduct(existProduct);

            var deletedProductResponseDto = _mapper.Map<ProductResponseDto>(deletedProduct);
            return deletedProductResponseDto;
        }

        // Check product has orders or not
        private async Task CheckHasOrderProduct(ProductResponseDto productResponseDto)
        {
            productResponseDto.HasOrders = await _orderRepository.HasOrders(productResponseDto.ProductId);

        }
    }
}
