﻿using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class CategoryRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(450)]
        public string? Image { get; set; }
    }
}
