﻿using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class ProductImageRequestDto
    {
        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;
    }
}
