﻿using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CustomerPhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [StringLength(100)]
        public string DeliveryTime { get; set; } = string.Empty;

        [Required]
        [StringLength(450)]
        public string Address { get; set; } = string.Empty;

        [StringLength(200)]
        public string? DeliveryNotes { get; set; }

        [Required]
        public bool IsGift { get; set; }

        [StringLength(50)]
        public string? RecipientName { get; set; }

        [StringLength(20)]
        public string? RecipientPhoneNumber { get; set; }

        [Required]
        public decimal ShippingFee { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        public DateTime CreateAt { get; set; }

        public ShippingMethodResponseDto? ShippingMethod { get; set; }

        public OrderStatusResponseDto? OrderStatus { get; set; }

        public List<OrderItemResponseDto>? Items { get; set; }
    }
}
