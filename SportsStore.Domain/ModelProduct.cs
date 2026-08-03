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
        public required string Name { get; set; } // required đảm bảo thuộc tính này phải được khởi tạo
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Category { get; set; }
        // Thêm thuộc tính ImageUrl để lưu đường dẫn ảnh
        public string? ImageUrl { get; set; }
    }
}
