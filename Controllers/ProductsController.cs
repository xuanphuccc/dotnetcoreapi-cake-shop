﻿using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(
            IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProductResponseDtos = await _productService.GetAllProducts();

            return Ok(new ResponseDto()
            {
                Data = allProductResponseDtos
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            var productResponseDto = await _productService.GetProductById(id.Value);
            if (productResponseDto == null)
            {
                return NotFound(new ResponseDto() { Status = 404, Title = "product not found" });
            }

            return Ok(new ResponseDto()
            {
                Data = productResponseDto
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto productRequestDto)
        {
            if (productRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "product is required" });
            }

            // Check exist category
            if (productRequestDto.CategoryId != null)
            {
                var existCategory = await _categoryService.GetCategoryById(productRequestDto.CategoryId.Value);
                if (existCategory == null)
                {
                    return BadRequest(new ResponseDto() { Status = 400, Title = "category not found" });
                }
            }

            try
            {
                // Create product
                var createdProductResponseDto = await _productService.CreateProduct(productRequestDto);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProductResponseDto.ProductId },
                    new ResponseDto()
                    {
                        Data = createdProductResponseDto,
                        Status = 201,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int? id, [FromBody] ProductRequestDto productRequestDto)
        {
            if (!id.HasValue || productRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            // Check exist category
            if (productRequestDto.CategoryId != null)
            {
                var existCategory = await _categoryService.GetCategoryById(productRequestDto.CategoryId.Value);
                if (existCategory == null)
                {
                    return BadRequest(new ResponseDto() { Status = 400, Title = "category not found" });
                }
            }

            try
            {
                // Update product
                var updatedProductResponseDto = await _productService.UpdateProduct(id.Value, productRequestDto);

                return Ok(new ResponseDto()
                {
                    Data = updatedProductResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "productId is required" });
            }

            try
            {
                // Delete product
                var deletedProductResponseDto = await _productService.DeleteProduct(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = deletedProductResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }
    }
}
