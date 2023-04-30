﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
