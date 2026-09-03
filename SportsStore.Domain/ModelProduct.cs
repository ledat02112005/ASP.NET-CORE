using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain
{
    public class ModelProduct
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public required string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị dương")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập danh mục")]
        public required string Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
