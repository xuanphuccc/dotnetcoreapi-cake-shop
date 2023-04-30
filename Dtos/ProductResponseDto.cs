using dotnetcoreapi_cake_shop.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnetcoreapi_cake_shop.Dtos
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Ingredients { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(256)]
        public string? Taste { get; set; }

        public string? Texture { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;

        public string? Accessories { get; set; }

        public string? Instructions { get; set; }

        [Required]
        public bool IsDisplay { get; set; }

        public DateTime? CreateAt { get; set; }

        public CategoryResponseDto? Category { get; set; }

        public List<ProductImageResponseDto>? Images { get; set; }
    }
}
