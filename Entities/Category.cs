﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcoreapi_cake_shop.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

        [Required]
        [StringLength(450)]
        public string Image { get; set; } = string.Empty;

        public DateTime? CreateAt { get; set; }

        public List<Product>? Products { get; set; }
    }
}
