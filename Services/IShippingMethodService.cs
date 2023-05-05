﻿using dotnetcoreapi_cake_shop.Dtos;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface IShippingMethodService
    {
        // Get all shipping methods response DTO
        Task<List<ShippingMethodResponseDto>> GetAllShippingMethods();

        // Get shipping method response DTO
        Task<ShippingMethodResponseDto> GetShippingMethodById(int shippingMethodId);

        // Create shipping method
        Task<ShippingMethodResponseDto> CreateShippingMethod(ShippingMethodRequestDto shippingMethodRequestDto);

        // Update shipping method
        Task<ShippingMethodResponseDto> UpdateShippingMethod(int id, ShippingMethodRequestDto shippingMethodRequestDto);

        // Delete shipping method
        Task<ShippingMethodResponseDto> DeleteShippingMethod(int shippingMethodId);
    }
}
