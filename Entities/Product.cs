﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Ingredients { get; set; } = string.Empty;

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

        [StringLength(256)]
        public string? Taste { get; set; }

        [Column(TypeName = "ntext")]
        public string? Texture { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;

        [Column(TypeName = "ntext")]
        public string? Accessories { get; set; }

        [Column(TypeName = "ntext")]
        public string? Instructions { get; set; }

        [Required]
        public bool IsDisplay { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public List<ProductImage>? Images { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
