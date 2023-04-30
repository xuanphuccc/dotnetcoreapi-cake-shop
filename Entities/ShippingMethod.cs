﻿using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class ShippingMethod
    {
        [Key]
        public int ShippingMethodId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal PricePerKm { get; set; }

        [StringLength(450)]
        public string? Logo { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public DateTime? CreateAt { get; set; }


        public List<Order>? Orders { get; set; }
    }
}
